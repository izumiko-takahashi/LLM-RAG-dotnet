using LlmRag.Application.Interfaces;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Embeddings;
using UglyToad.PdfPig;

namespace LlmRag.Infrastructure.Repositories;

public class DocumentRepository : IDocumentSearch
{
    private readonly Kernel _kernel;

    public DocumentRepository(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<string> SearchAsync(string question, string fileName)
    {
        var fragments = ExtractFragments(fileName);
        var bestFragment = await FindMostRelevantAsync(question, fragments);
        return bestFragment;
    }

    private List<string> ExtractFragments(string fileName)
    {
        var filePath = Path.Combine("Documents", fileName);

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Documento no encontrado: {fileName}");

        var fragments = new List<string>();
        using var pdf = PdfDocument.Open(filePath);

        foreach (var page in pdf.GetPages())
        {
            var text = page.Text;
            if (!string.IsNullOrWhiteSpace(text))
                fragments.Add(text);
        }

        return fragments;
    }

    private async Task<string> FindMostRelevantAsync(string question, List<string> fragments)
    {
        var embeddingService = _kernel.GetRequiredService<ITextEmbeddingGenerationService>();

        var questionEmbedding = await embeddingService.GenerateEmbeddingAsync(question);

        var bestScore = double.MinValue;
        var bestFragment = string.Empty;

        foreach (var fragment in fragments)
        {
            var fragmentEmbedding = await embeddingService.GenerateEmbeddingAsync(fragment);
            var score = CosineSimilarity(questionEmbedding.Span, fragmentEmbedding.Span);

            if (score > bestScore)
            {
                bestScore = score;
                bestFragment = fragment;
            }
        }

        return bestFragment;
    }

    private static double CosineSimilarity(ReadOnlySpan<float> a, ReadOnlySpan<float> b)
    {
        double dot = 0, magA = 0, magB = 0;
        for (int i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            magA += a[i] * a[i];
            magB += b[i] * b[i];
        }
        return dot / (Math.Sqrt(magA) * Math.Sqrt(magB));
    }
}
using LlmRag.Application.Interfaces;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Memory;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace LlmRag.Infrastructure.Repositories;

public class DocumentRepository : IDocumentSearch
{
    private readonly ISemanticTextMemory _memory;
    private const string CollectionName = "documents";

    public DocumentRepository(ISemanticTextMemory memory)
    {
        _memory = memory;
    }

    public async Task<string> SearchAsync(string question, string fileName)
    {
        await IngestDocumentAsync(fileName);

        var results = _memory.SearchAsync(
            collection: CollectionName,
            query: question,
            limit: 3,
            minRelevanceScore: 0.7
        );

        var extracts = new List<string>();
        await foreach (var result in results)
        {
            extracts.Add(result.Metadata.Text);
        }

        return string.Join("\n\n", extracts);
    }

    private async Task IngestDocumentAsync(string fileName)
    {
        var filePath = Path.Combine("Documents", fileName);

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Documento no encontrado: {fileName}");

        using var pdf = PdfDocument.Open(filePath);
        foreach (var page in pdf.GetPages())
        {
            var text = page.Text;
            await _memory.SaveInformationAsync(
                collection: CollectionName,
                text: text,
                id: $"{fileName}-page-{page.Number}"
            );
        }
    }
}
using LlmRag.Application.Interfaces;
using LlmRag.Infrastructure.LLM;
using LlmRag.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace LlmRag.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string openAiApiKey)
    {
        var memory = new MemoryBuilder()
            .WithOpenAITextEmbeddingGeneration("text-embedding-ada-002", openAiApiKey)
            .WithVolatileMemoryStore()
            .Build();

        services.AddSingleton<ISemanticTextMemory>(memory);
        services.AddScoped<IDocumentSearch, DocumentRepository>();
        services.AddScoped<IResponseGenerator, LlmConection>();

        return services;
    }
}
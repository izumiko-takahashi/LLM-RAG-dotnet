using LlmRag.Application.Interfaces;
using LlmRag.Infrastructure.LLM;
using LlmRag.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace LlmRag.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string openAiApiKey)
    {
        services.AddSingleton<Kernel>(_ =>
    Kernel.CreateBuilder()
        .AddOpenAIChatCompletion("gpt-4o-mini", openAiApiKey)
        .AddOpenAITextEmbeddingGeneration("text-embedding-ada-002", openAiApiKey)
        .Build());

        services.AddScoped<IDocumentSearch, DocumentRepository>();
        services.AddScoped<IResponseGenerator, LlmConection>();

        return services;
    }
}
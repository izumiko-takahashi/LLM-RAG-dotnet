using LlmRag.Application.Interfaces;
using LlmRag.Infrastructure.LLM;
using LlmRag.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LlmRag.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IDocumentSearch, DocumentRepository>();
        services.AddScoped<IResponseGenerator, LlmConection>();
        return services;
    }
}
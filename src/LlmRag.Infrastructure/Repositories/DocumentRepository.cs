using LlmRag.Application.Interfaces;

namespace LlmRag.Infrastructure.Repositories;

public class DocumentRepository : IDocumentSearch
{
    public async Task<string> SearchAsync(string question, string fileName)
    {
        // Aquí irá la lógica real de búsqueda semántica
        throw new NotImplementedException();
    }
}
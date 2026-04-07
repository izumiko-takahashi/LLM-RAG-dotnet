using LlmRag.Application.Interfaces;

namespace LlmRag.Infrastructure.LLM;

public class LlmConection : IResponseGenerator
{
    public async Task<string> GenerateAsync(string extract)
    {
        // Aquí irá la conexión real al LLM
        throw new NotImplementedException();
    }
}

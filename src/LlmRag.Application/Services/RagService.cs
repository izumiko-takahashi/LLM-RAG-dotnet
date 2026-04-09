using LlmRag.Application.Interfaces;

namespace LlmRag.Application.Services;

public class RagService
{
    private readonly IDocumentSearch _documentSearch;    //Mejor recibimos desde fuera los métodos, aplicando la inyección de dependencias.
    private readonly IResponseGenerator _responseGenerator;  //Podrían quedar dentro del constructor como new, pero perdería flexibilidad, casando el servicio a lo definido en el constructor.

    public RagService(IDocumentSearch documentSearch, IResponseGenerator responseGenerator)
    {
        _documentSearch = documentSearch;
        _responseGenerator = responseGenerator;
    }

    public async Task<string> AskAsync(string question, string fileName)
    {
        var extract = await _documentSearch.SearchAsync(question, fileName);
        var response = await _responseGenerator.GenerateAsync(extract, question);
        return response;
    }
}
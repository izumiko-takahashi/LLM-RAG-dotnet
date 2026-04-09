using LlmRag.Application.Interfaces;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace LlmRag.Infrastructure.LLM;

public class LlmConection : IResponseGenerator
{
    private readonly Kernel _kernel;
    private const string SystemPrompt = """
        Eres un asistente que responde preguntas basándose ÚNICAMENTE en el contexto proporcionado.
        No uses conocimiento externo ni inventes información.
        Si la respuesta no está en el contexto, responde: "No encontré información sobre eso en el documento."
        Responde de forma clara y accesible para cualquier persona.
        """;

    public LlmConection(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<string> GenerateAsync(string extract, string question)
    {
        var chatService = _kernel.GetRequiredService<IChatCompletionService>();

        var history = new ChatHistory(SystemPrompt);
        history.AddUserMessage($"Contexto del documento:\n{extract}");

        var response = await chatService.GetChatMessageContentAsync(history);
        return response.Content ?? "No se pudo generar una respuesta.";
    }
}
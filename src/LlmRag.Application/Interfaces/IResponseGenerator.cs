namespace LlmRag.Application.Interfaces;

public interface IResponseGenerator
{
    Task<string> GenerateAsync(string extract, string question);
}
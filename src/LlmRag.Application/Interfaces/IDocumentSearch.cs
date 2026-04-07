using LlmRag.Domain.Entities;

namespace LlmRag.Application.Interfaces;

public interface IDocumentSearch
{
    Task<string> SearchAsync(string question, string fileName);
}
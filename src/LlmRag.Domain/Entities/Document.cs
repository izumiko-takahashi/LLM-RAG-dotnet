
namespace LlmRag.Domain.Entities;

public class Document
{
    public Guid Id { get; set; }
    public string Format { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
}
namespace LlmRag.Domain.Entities;

public class Query
{
    public Guid Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
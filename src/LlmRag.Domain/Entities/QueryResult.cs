namespace LlmRag.Domain.Entities;

public class QueryResult
{
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public Guid QueryId { get; set; }
    public string Extract { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
}
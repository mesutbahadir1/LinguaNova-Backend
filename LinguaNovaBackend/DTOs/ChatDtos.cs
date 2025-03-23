public record ChatRequest
{
    public string Message { get; init; }
    public string TargetLanguage { get; init; }
}

public record ChatResponse
{
    public string Message { get; init; }
    public DateTime Timestamp { get; init; }
} 
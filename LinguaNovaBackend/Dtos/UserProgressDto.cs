namespace LinguaNovaBackend.Dtos;

public class UserProgressDto
{
    public ProgressItemDto ArticleProgress { get; set; }
    public ProgressItemDto AudioProgress { get; set; }
    public ProgressItemDto VideoProgress { get; set; }
}
public class ProgressItemDto
{
    public string Title { get; set; }
    public int CompletedCount { get; set; }
    public int TotalCount { get; set; }
}
namespace LinguaNovaBackend.Dtos;

public class ArticleProgressDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public bool IsCompleted { get; set; }
}
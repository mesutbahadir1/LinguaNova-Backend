namespace LinguaNovaBackend.Dtos;

public class UserArticleProgressDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ArticleId { get; set; }
    public int Level { get; set; }
    public bool IsCompleted { get; set; }
}
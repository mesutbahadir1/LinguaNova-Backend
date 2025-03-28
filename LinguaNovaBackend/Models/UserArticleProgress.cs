namespace LinguaNovaBackend.Models;

public class UserArticleProgress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int ArticleId { get; set; }
    public Article Article { get; set; }
    public int Level { get; set; }
    public bool IsCompleted { get; set; }
}
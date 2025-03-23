public class UserArticleProgress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ArticleId { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime LastAccessTime { get; set; }
    
    // Navigation properties
    public User User { get; set; }
    public Article Article { get; set; }
} 
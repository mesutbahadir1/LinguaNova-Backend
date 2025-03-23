public class ArticleTest
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public int Score { get; set; }
    public int ArticleId { get; set; }
    
    // Navigation property
    public Article Article { get; set; }
} 
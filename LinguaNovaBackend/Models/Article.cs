public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Level { get; set; }

    // Navigation properties
    public ICollection<ArticleTest> Tests { get; set; }
    public ICollection<UserArticleProgress> UserProgresses { get; set; }
} 
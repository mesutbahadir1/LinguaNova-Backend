public class TestResult
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string TestType { get; set; } // "Article", "Video", "Audio"
    public int TestId { get; set; }
    public int Score { get; set; }
    public DateTime CompletionDate { get; set; }
    
    // Navigation property
    public User User { get; set; }
} 
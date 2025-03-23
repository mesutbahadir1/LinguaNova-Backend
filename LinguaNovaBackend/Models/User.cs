public class User
{
    public int Id { get; set; }
    public string FirebaseUid { get; set; } // Firebase User ID
    public string Username { get; set; }
    public string Email { get; set; }
    public int Level { get; set; }
    public int TotalScore { get; set; }

    // Navigation properties
    public ICollection<UserArticleProgress> ArticleProgresses { get; set; }
    public ICollection<UserVideoProgress> VideoProgresses { get; set; }
    public ICollection<UserAudioProgress> AudioProgresses { get; set; }
    public ICollection<TestResult> TestResults { get; set; }
} 
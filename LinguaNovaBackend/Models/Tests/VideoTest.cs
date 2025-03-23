public class VideoTest
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public int Score { get; set; }
    public int VideoId { get; set; }
    
    // Navigation property
    public Video Video { get; set; }
} 
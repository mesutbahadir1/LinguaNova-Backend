public class UserVideoProgress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int VideoId { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime LastAccessTime { get; set; }
    public double WatchedDuration { get; set; }
    
    // Navigation properties
    public User User { get; set; }
    public Video Video { get; set; }
} 
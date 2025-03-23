public class UserAudioProgress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AudioId { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime LastAccessTime { get; set; }
    public double ListenedDuration { get; set; }
    
    // Navigation properties
    public User User { get; set; }
    public Audio Audio { get; set; }
} 
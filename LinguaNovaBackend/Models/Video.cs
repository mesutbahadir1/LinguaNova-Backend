public class Video
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public int Level { get; set; }

    // Navigation properties
    public ICollection<VideoTest> Tests { get; set; }
    public ICollection<UserVideoProgress> UserProgresses { get; set; }
} 
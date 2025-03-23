public class Audio
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public int Level { get; set; }

    // Navigation properties
    public ICollection<AudioTest> Tests { get; set; }
    public ICollection<UserAudioProgress> UserProgresses { get; set; }
} 
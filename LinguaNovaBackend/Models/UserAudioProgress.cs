namespace LinguaNovaBackend.Models;

public class UserAudioProgress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int AudioId { get; set; }
    public Audio Audio { get; set; }
    public int Level { get; set; }
    public bool IsCompleted { get; set; }
}
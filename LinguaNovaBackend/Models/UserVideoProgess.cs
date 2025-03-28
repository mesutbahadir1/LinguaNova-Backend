namespace LinguaNovaBackend.Models;

public class UserVideoProgress
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int VideoId { get; set; }
    public Video Video { get; set; }
    public int Level { get; set; }
    public bool IsCompleted { get; set; }
}

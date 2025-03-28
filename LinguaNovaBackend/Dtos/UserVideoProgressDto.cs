namespace LinguaNovaBackend.Dtos;

public class UserVideoProgressDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int VideoId { get; set; }
    public int Level { get; set; }
    public bool IsCompleted { get; set; }
}
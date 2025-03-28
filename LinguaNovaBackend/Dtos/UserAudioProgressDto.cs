namespace LinguaNovaBackend.Dtos;

public class UserAudioProgressDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AudioId { get; set; }
    public int Level { get; set; }
    public bool IsCompleted { get; set; }
}
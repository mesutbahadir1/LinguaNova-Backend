public record UserDto
{
    public int Id { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
    public int Level { get; init; }
    public int TotalScore { get; init; }
} 
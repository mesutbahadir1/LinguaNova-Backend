namespace LinguaNovaBackend.Dtos;

public class VideoTestDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer1 { get; set; }
    public string Answer2 { get; set; }
    public string Answer3 { get; set; }
    public string Answer4 { get; set; }
    public int CorrectAnswerIndex { get; set; }
    public int VideoId { get; set; }
}
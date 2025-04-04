namespace LinguaNovaBackend.Dtos;

public class UserAudioTestDto
{
    public int Id { get; set; }  // Testin ID'si
    public string Question { get; set; }  // Test sorusu
    public string Answer1 { get; set; }  // 1. cevap seçeneği
    public string Answer2 { get; set; }  // 2. cevap seçeneği
    public string Answer3 { get; set; }  // 3. cevap seçeneği
    public string Answer4 { get; set; }  // 4. cevap seçeneği
    public int CorrectAnswerIndex { get; set; }  // Doğru cevabın indexi (1, 2, 3, 4)
    public int? TestProgressId { get; set; }  // Testin ilerleme kaydının ID'si (nullable)
    public bool IsCorrect { get; set; }  // Kullanıcının doğru cevabı verip vermediği durumu
}

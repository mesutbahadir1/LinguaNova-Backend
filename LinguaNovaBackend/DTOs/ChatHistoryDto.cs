namespace LinguaNova.DTOs
{
    public class ChatHistoryDto
    {
        public string Message { get; set; }
        public string Response { get; set; }
        public string TargetLanguage { get; set; }
        public DateTime Timestamp { get; set; }
    }
} 
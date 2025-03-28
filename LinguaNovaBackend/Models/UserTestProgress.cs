namespace LinguaNovaBackend.Models
{
    public class UserTestProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ArticleId { get; set; }
        public int? VideoId { get; set; }
        
        public int? AudioId { get; set; }
        public int? ArticleTestId { get; set; }
        public int? VideoTestId { get; set; }
        
        public int? AudioTestId { get; set; }
        public bool IsCorrect { get; set; }
       
    }
} 
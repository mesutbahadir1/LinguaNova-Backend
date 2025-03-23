namespace LinguaNovaBackend.Models
{
    public class UserTestProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        public int? ArticleTestId { get; set; }
        public virtual ArticleTest ArticleTest { get; set; }
        
        public int? VideoTestId { get; set; }
        public virtual VideoTest VideoTest { get; set; }
        
        public int? AudioTestId { get; set; }
        public virtual AudioTest AudioTest { get; set; }
        
        public bool IsCorrect { get; set; }
        public int Score { get; set; }
    }
} 
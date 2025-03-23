using System.Collections.Generic;

namespace LinguaNovaBackend.Models
{
    public class ArticleTest
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        public int Score { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public virtual ICollection<UserTestProgress> UserTestProgresses { get; set; }
    }
} 
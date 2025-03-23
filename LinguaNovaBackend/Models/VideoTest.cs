using System.Collections.Generic;

namespace LinguaNovaBackend.Models
{
    public class VideoTest
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        public int Score { get; set; }
        public int VideoId { get; set; }
        public virtual Video Video { get; set; }
        public virtual ICollection<UserTestProgress> UserTestProgresses { get; set; }
    }
} 
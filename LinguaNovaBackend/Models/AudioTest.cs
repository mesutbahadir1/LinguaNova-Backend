using System.Collections.Generic;

namespace LinguaNovaBackend.Models
{
    public class AudioTest
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
        public int Score { get; set; }
        public int AudioId { get; set; }
        public virtual Audio Audio { get; set; }
        public virtual ICollection<UserTestProgress> UserTestProgresses { get; set; }
    }
} 
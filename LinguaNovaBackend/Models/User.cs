using System.Collections.Generic;

namespace LinguaNovaBackend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Level { get; set; }
        public int TotalScore { get; set; }
        public virtual ICollection<UserTestProgress> TestProgress { get; set; }
    }
} 
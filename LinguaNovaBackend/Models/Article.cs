using System.Collections.Generic;

namespace LinguaNovaBackend.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Level { get; set; }
    }
} 
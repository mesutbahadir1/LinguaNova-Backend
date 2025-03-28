using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinguaNovaBackend.Models
{
    public class VideoTest
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public int VideoId { get; set; }
        public virtual Video Video { get; set; }
    }
} 
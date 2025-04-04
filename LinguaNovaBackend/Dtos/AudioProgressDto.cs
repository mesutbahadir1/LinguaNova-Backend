namespace LinguaNovaBackend.Dtos
{
    public class AudioProgressDto
    {
        public int Id { get; set; } // Audio Id
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsCompleted { get; set; }
    }
}
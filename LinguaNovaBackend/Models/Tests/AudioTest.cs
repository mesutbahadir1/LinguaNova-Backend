public class AudioTest
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public int Score { get; set; }
    public int AudioId { get; set; }
    
    // Navigation property
    public Audio Audio { get; set; }
} 
namespace rpglms.DTOs
{
    public class PerformanceDto
    {
        public int PerformanceId { get; set; }
        public int ChronicleId { get; set; }
        public int ChapterId { get; set; }
        public required string Title { get; set; }
        public string? Instructions { get; set; }
        public int? Attempts { get; set; }
        public int? Timer { get; set; }
        public int? PassingScore { get; set; }
        // Other properties...
    }
}

namespace rpglms_backend.src.models
{
    public class Performance
    {
        public int PerformanceId { get; set; }
        public int ChronicleId { get; set; }
        public Chronicle? Chronicle { get; set; }
        public int ChapterId { get; set; }
        public Chapter? Chapter { get; set; }
        public required string Title { get; set; }
        public string? Instructions { get; set; }
        public int? Attempts { get; set; }
        public int? Timer { get; set; }
        public int? PassingScore { get; set; }
        public required ICollection<Question> Questions { get; set; }
    }
}

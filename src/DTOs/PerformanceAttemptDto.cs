namespace rpglms_backend.DTOs
{
    public class PerformanceAttemptDto
    {
        public int PerformanceAttemptId { get; set; }
        public int Score { get; set; }
        public int HighestStreak { get; set; }
        public DateTime CompletedAt { get; set; }
        // Other properties...
    }
}

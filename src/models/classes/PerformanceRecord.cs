namespace rpglms_backend.src.models
{
    public class PerformanceRecord
    {
        public int PerformanceRecordId { get; set; }
        public required AppUser User { get; set; }
        public required Performance Performance { get; set; }
        public int HighestStreak { get; set; }
        public int HighestScore { get; set; }
        public DateTime LastUpdated { get; set; }
        public required ICollection<PerformanceAttempt> PerformanceAttempts { get; set; }
        public bool Passed { get; set; }
    }
}

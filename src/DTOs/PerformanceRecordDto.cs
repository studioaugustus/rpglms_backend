namespace rpglms_backend.DTOs
{
    public class PerformanceRecordDto
    {
        public int PerformanceRecordId { get; set; }
        public int HighestStreak { get; set; }
        public int HighestScore { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Passed { get; set; }
        // Other properties...
    }
}

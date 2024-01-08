namespace rpglms.src.models
{
    public class LearnerView
    {
        public required string AppUserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public int CompletedChapterRecordsCount { get; set; }
        public int CompletedPerformanceRecordsCount { get; set; }
        public int ReflectionResponsesCount { get; set; }
    }
}
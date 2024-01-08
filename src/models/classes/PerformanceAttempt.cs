namespace rpglms.src.models
{
    public class PerformanceAttempt
    {
        public int PerformanceAttemptId { get; set; }
        public required PerformanceRecord PerformanceRecord { get; set; }
        public ICollection<Question>? CorrectQuestions { get; set; }
        public ICollection<Question>? IncorrectQuestions { get; set; }
        public int Score { get; set; }
        public int HighestStreak { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

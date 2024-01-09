namespace rpglms_backend.DTOs
{
    public class QuestRecordDto
    {
        public int QuestRecordId { get; set; }
        public int UserId { get; set; }
        public int QuestId { get; set; }
        public bool InvestigationComplete { get; set; }
        public bool ReflectionComplete { get; set; }
        public bool PracticeComplete { get; set; }
        public bool Completed { get; set; }
        public DateTime UpdatedAt { get; set; }
        // Other properties...
    }
}

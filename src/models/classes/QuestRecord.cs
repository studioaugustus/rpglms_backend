namespace rpglms.src.models
{
    public class QuestRecord
    {
        public int QuestRecordId { get; set; } // Primary key

        // Relationship to User
        public int UserId { get; set; }
        public required AppUser User { get; set; }

        // Relationship to Quest
        public int QuestId { get; set; }
        public required Quest Quest { get; set; }

        // Boolean fields
        public bool InvestigationComplete { get; set; }
        public bool ReflectionComplete { get; set; }
        public bool PracticeComplete { get; set; }
        public bool Completed { get; set; }

        // DateTime field
        public DateTime UpdatedAt { get; set; }

        // Additional properties and methods...
    }

}

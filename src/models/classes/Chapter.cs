namespace rpglms.src.models
{
    public class Chapter
    {
        public int ChapterId { get; set; } // Primary key
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
        public bool Prerequisite { get; set; }
        public int Step { get; set; }

        // Relationship to Chronicle
        public int ChronicleId { get; set; }
        public required Chronicle Chronicle { get; set; }
        // List of related Quests
        public List<Quest>? Quests { get; set; }
        // List of related Questions
        public List<Question>? Questions { get; set; }
        public ICollection<Performance>? Performances { get; set; }

        // Constructor to initialize collections
        public Chapter()
        {
            Title = "";
            Description = "";
            Published = false;
            Prerequisite = false;
        }
    }
}


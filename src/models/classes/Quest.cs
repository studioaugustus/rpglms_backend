namespace rpglms.src.models
{
    public class Quest
    {
        public int QuestId { get; set; } // Primary key
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? DueDate { get; set; } // Nullable for optional due date
        public bool Published { get; set; }
        public bool Prerequisite { get; set; }
        public int Step { get; set; }

        // Relationship to Chapter
        public int ChapterId { get; set; }
        public required Chapter Chapter { get; set; }

        // One-to-One relationship with Investigation, Reflection, and Practice
        public Investigation? Investigation { get; set; }
        public Reflection? Reflection { get; set; }
        public Practice? Practice { get; set; }
        // List of related QuestRecords
        public List<QuestRecord>? QuestRecords { get; set; }
        // List of related Questions

        // Constructor to initialize collections
        public Quest()
        {
            Title = "";
            Description = "";
        }
    }
}

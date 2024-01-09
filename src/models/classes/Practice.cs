namespace rpglms_backend.src.models
{
    public class Practice
    {
        public int PracticeId { get; set; } // Primary key

        // Relationship to Quest
        public int QuestId { get; set; }
        public required Quest Quest { get; set; }

        // Nullable Intro property
        public string Intro { get; set; }

        // List of related Questions
        public List<Question> Questions { get; set; }

        // Constructor to initialize the Questions collection
        public Practice()
        {
            Intro = "";
            Questions = [];
        }

        // Additional properties and methods...
    }

}

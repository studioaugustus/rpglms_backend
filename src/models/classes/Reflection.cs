namespace rpglms_backend.src.models
{
    public class Reflection
    {
        public int ReflectionId { get; set; } // Primary key
        public string Intro { get; set; }

        // Foreign key and navigation property to Quest
        public int QuestId { get; set; }
        public required Quest Quest { get; set; }

        // List of related ReflectionForms
        public required List<ReflectionForm> ReflectionForms { get; set; }

        public Reflection()
        {
            Intro = "";
        }
    }

}

namespace rpglms.src.models
{
    public class Chronicle
    {
        public int ChronicleId { get; set; } // Primary key
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string JoinCode { get; set; }
        public bool Published { get; set; }
        public bool OpenAccess { get; set; }

        // Navigation properties
        public List<Chapter>? Chapters { get; set; }
        public List<Enrollment>? Enrollments { get; set; }
        public List<Question>? Questions { get; set; }
        public ICollection<Performance>? Performances { get; set; }
        // Constructor to initialize collections
        public Chronicle()
        {
            Title = "";
            Description = "";
            JoinCode = "";
        }
    }
}


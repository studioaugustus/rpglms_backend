namespace rpglms.DTOs
{
    public class ChapterDto
    {
        public int ChapterId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool Published { get; set; }
        public bool Prerequisite { get; set; }
        public int Step { get; set; }
        public int ChronicleId { get; set; }
        // Other properties...
    }
}

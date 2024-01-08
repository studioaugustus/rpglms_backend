namespace rpglms.DTOs
{
    public class QuestDto
    {
        public int QuestId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Published { get; set; }
        public bool Prerequisite { get; set; }
        public int Step { get; set; }
        public int ChapterId { get; set; }
        // Other properties...
    }
}

namespace rpglms.DTOs
{
    public class InvestigationDto
    {
        public int InvestigationId { get; set; }
        public required string Content { get; set; }
        public required string SerializedClues { get; set; }
        public int QuestId { get; set; }
        // Other properties...
    }
}

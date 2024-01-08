namespace rpglms.DTOs
{
    public class ChronicleDto
    {
        public int ChronicleId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string JoinCode { get; set; }
        public bool Published { get; set; }
        public bool OpenAccess { get; set; }
        // Other properties...
    }
}

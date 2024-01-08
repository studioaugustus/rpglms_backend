namespace rpglms.DTOs
{
    public class ReflectionDto
    {
        public int ReflectionId { get; set; }
        public required string Intro { get; set; }
        public required int QuestId { get; set; }

        // List of related ReflectionForms
        public required List<ReflectionFormDto> ReflectionForms { get; set; }
        // Other properties...
    }
}

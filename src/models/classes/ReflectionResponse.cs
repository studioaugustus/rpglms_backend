namespace rpglms.src.models
{
    public class ReflectionResponse
    {
        public int ReflectionResponseId { get; set; } // Primary key

        // Relationship to User
        public int UserId { get; set; }
        public required AppUser User { get; set; }

        // Relationship to ReflectionForm
        public int ReflectionFormId { get; set; }
        public required ReflectionForm ReflectionForm { get; set; }
        // Other properties...
    }
}

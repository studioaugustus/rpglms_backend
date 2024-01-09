using rpglms_backend.src.models.enums;

namespace rpglms_backend.DTOs
{
    public class ReflectionFormDto
    {
        public int ReflectionFormId { get; set; }
        public required string Prompt { get; set; }
        public ReflectionFormType Type { get; set; }
        public string? InitialValue { get; set; }
        public string? Examples { get; set; }
        public int ReflectionId { get; set; }
        // Other properties...
    }
}

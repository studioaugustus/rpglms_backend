using rpglms.src.models.enums;

namespace rpglms.src.models
{
    public class ReflectionForm
    {
        public int ReflectionFormId { get; set; } // Primary key
        public string Prompt { get; set; }
        public ReflectionFormType Type { get; set; }
        public string? InitialValue { get; set; }
        public string? Examples { get; set; }

        // Relationship to Reflection
        public int ReflectionId { get; set; }
        public required Reflection Reflection { get; set; }

        // List of related ReflectionResponses
        public List<ReflectionResponse>? ReflectionResponses { get; set; }

        public ReflectionForm()
        {
            Prompt = "Prompt Here";
        }
    }

}

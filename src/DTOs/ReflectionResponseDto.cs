using System.ComponentModel.DataAnnotations;

namespace rpglms_backend.DTOs
{
    public class ReflectionResponseDto
    {
        public int ReflectionResponseId { get; set; }
        public int UserId { get; set; }
        public int ReflectionFormId { get; set; }
        // Other properties...
    }
}

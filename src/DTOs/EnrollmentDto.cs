using rpglms_backend.src.models.enums;

namespace rpglms_backend.DTOs
{
    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }
        public int UserId { get; set; }
        public int ChronicleId { get; set; }
        public DateTime EnrolledDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Role Role { get; set; }
        // Other properties...
    }
}

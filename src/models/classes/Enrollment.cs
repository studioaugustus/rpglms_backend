using rpglms_backend.src.models.enums;

namespace rpglms_backend.src.models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; } // Primary key

        // One-to-One relationship with User
        public int UserId { get; set; }
        public required AppUser AppUser { get; set; }

        // One-to-One relationship with Chronicle
        public int ChronicleId { get; set; }
        public required Chronicle Chronicle { get; set; }

        public DateTime EnrolledDate { get; set; }
        public DateTime? CompletedDate { get; set; } // Nullable for ongoing enrollments

        public Role Role { get; set; } // Assuming Role is an enum defined elsewhere
    }

}


namespace rpglms_backend.DTOs
{
    public class AppUserDto
    {
        public required string Id { get; set; }
        public required string UserName { get; set; } // IdentityUser's Id
        public required string Email { get; set; }  // IdentityUser's Email
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? DisplayName { get; set; }
        public int TotalPoints { get; set; }
        public int PointsBalance { get; set; }
        public string? Token { get; set; }
        public DateTime? LastLogin { get; set; }



        // Navigation properties
        public List<EnrollmentDto>? Enrollments { get; set; }
        public List<QuestRecordDto>? QuestRecords { get; set; }
        public List<ReflectionResponseDto>? ReflectionResponses { get; set; }
        public List<PerformanceRecordDto>? PerformanceRecords { get; set; }
        public AvatarDto? Avatar { get; set; }

    }
}

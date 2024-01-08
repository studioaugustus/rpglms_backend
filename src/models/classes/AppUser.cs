using Microsoft.AspNetCore.Identity;

namespace rpglms.src.models
{
    public class AppUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? DisplayName { get; set; }
        public int TotalPoints { get; set; }
        public int PointsBalance { get; set; }

        // Navigation properties
        public List<Enrollment>? Enrollments { get; set; }
        public List<QuestRecord>? QuestRecords { get; set; }
        public List<ReflectionResponse>? ReflectionResponses { get; set; }
        public List<PerformanceRecord>? PerformanceRecords { get; set; }
        public Avatar? Avatar { get; set; }

        public AppUser()
        {
            TotalPoints = 0;
            PointsBalance = 0;
        }
    }

}

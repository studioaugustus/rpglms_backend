using System.ComponentModel.DataAnnotations;

namespace rpglms_backend.src.DTOs
{
    public class RegisterDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string DisplayName { get; set; }
        // You can add more fields as required for registration
    }

}

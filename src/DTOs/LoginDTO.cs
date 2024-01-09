namespace rpglms_backend.src.DTOs
{
    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        // You might include a 'RememberMe' field if needed
    }

}

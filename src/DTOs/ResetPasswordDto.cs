namespace rpglms_backend.src.DTOs
{
    public class ResetPasswordDto
    {


        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string Token { get; set; }
    }

}
namespace rpglms.src.DTOs
{
    public class RefreshTokenDto
    {
        public required string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public required string AppUserId { get; set; }

    }
}
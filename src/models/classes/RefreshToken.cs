namespace rpglms_backend.src.models
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public required string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public required string AppUserId { get; set; }
        public required AppUser AppUser { get; set; }
    }
}
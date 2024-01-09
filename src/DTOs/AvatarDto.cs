namespace rpglms_backend.DTOs
{
    public class AvatarDto
    {
        public int AvatarId { get; set; }  // Primary key

        // Nullable int properties for various avatar features
        public int? Skin { get; set; }
        public int? Top { get; set; }
        public int? Hat { get; set; }
        public int? Head { get; set; }
        public int? Eyes { get; set; }
        public int? EyeColor { get; set; }
        public int? Hair { get; set; }
        public int? HairColor { get; set; }
        public int? Beard { get; set; }
        public int? BeardColor { get; set; }
        public int? Body { get; set; }
        public int? Shoes { get; set; }
        public int? Glasses { get; set; }
        public int? Item { get; set; }

        // One-to-One relationship with User
        public required string Id { get; set; }  // IdentityUser's Id
        public required AppUserDto AppUser { get; set; }
    }
}
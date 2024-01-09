namespace rpglms_backend.src.models
{
    public class Avatar
    {
        public int AvatarId { get; set; } // Primary key

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
        public int AppUserId { get; set; }
        public required AppUser AppUser { get; set; }
    }

}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rpglms.src.models;

namespace rpglms.src.data
{
    public class DatabaseContext : IdentityDbContext<AppUser>
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Chronicle> Chronicles { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Reflection> Reflections { get; set; }
        public DbSet<Investigation> Investigations { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<PerformanceRecord> PerformanceRecords { get; set; }
        public DbSet<PerformanceAttempt> PerformanceAttempts { get; set; }
        public DbSet<QuestRecord> QuestRecords { get; set; }
        public DbSet<ReflectionResponse> ReflectionResponses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<ReflectionForm> ReflectionForms { get; set; }

        // Add DbSets for any relationship join tables needed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // enum conversions
            modelBuilder
                .Entity<ReflectionForm>()
                .Property(rf => rf.Type)
                .HasConversion<string>();
            modelBuilder
                .Entity<Enrollment>()
                .Property(e => e.Role)
                .HasConversion<string>();
            modelBuilder.Entity<Question>()
                .Property(q => q.Type)
                .HasConversion<string>();
            // One-to-One relationship between User and Avatar
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Avatar)
                .WithOne(a => a.AppUser)
                .HasForeignKey<Avatar>(a => a.AppUserId);
            // Many-to-Many: User and Chronicle (via Enrollment)
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.UserId, e.ChronicleId });
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.AppUser)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId);
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Chronicle)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.ChronicleId);
            // One-to-Many: Chronicle to Chapters
            modelBuilder.Entity<Chapter>()
                .HasOne(ch => ch.Chronicle)
                .WithMany(c => c.Chapters)
                .HasForeignKey(ch => ch.ChronicleId);
            // One-to-Many: Chapter to Quests
            modelBuilder.Entity<Quest>()
                .HasOne(q => q.Chapter)
                .WithMany(ch => ch.Quests)
                .HasForeignKey(q => q.ChapterId);
            // One-to-Many relationship between Practice and Questions
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Practice)
                .WithMany(p => p.Questions)
                .HasForeignKey(q => q.PracticeId);
            // One-to-One relationships with Quest and Investigation, Reflection, and Practice
            modelBuilder.Entity<Quest>()
                .HasOne(q => q.Reflection)
                .WithOne(r => r.Quest)
                .HasForeignKey<Reflection>(r => r.QuestId);
            modelBuilder.Entity<Quest>()
                .HasOne(q => q.Investigation)
                .WithOne(i => i.Quest)
                .HasForeignKey<Investigation>(i => i.QuestId);
            modelBuilder.Entity<Quest>()
                .HasOne(q => q.Practice)
                .WithOne(p => p.Quest)
                .HasForeignKey<Practice>(p => p.QuestId);
            // Optionally, configure unique constraints for Investigation, Reflection, and Practice
            modelBuilder.Entity<Quest>()
                .HasIndex(q => q.Investigation)
                .IsUnique();
            modelBuilder.Entity<Quest>()
                .HasIndex(q => q.Reflection)
                .IsUnique();
            modelBuilder.Entity<Quest>()
                .HasIndex(q => q.Practice)
                .IsUnique();
            // Relationship configurations
            modelBuilder.Entity<QuestRecord>()
                .HasOne(br => br.User)
                .WithMany(u => u.QuestRecords)
                .HasForeignKey(br => br.UserId);
            modelBuilder.Entity<QuestRecord>()
                .HasOne(br => br.Quest)
                .WithMany(b => b.QuestRecords)
                .HasForeignKey(br => br.QuestId);
            modelBuilder.Entity<ReflectionResponse>()
            .HasOne(rr => rr.User)
            .WithMany(u => u.ReflectionResponses)
            .HasForeignKey(rr => rr.UserId);
            // ReflectionResponse to ReflectionForm - Many-to-One
            modelBuilder.Entity<ReflectionResponse>()
                .HasOne(rr => rr.ReflectionForm)
                .WithMany(rf => rf.ReflectionResponses)
                .HasForeignKey(rr => rr.ReflectionFormId);

            // Indexes for Chronicle, Chapter, Quest, and Performance
            modelBuilder.Entity<Question>()
                .HasIndex(q => q.ChronicleId);

            modelBuilder.Entity<Question>()
                .HasIndex(q => q.ChapterId);

            modelBuilder.Entity<Question>()
                .HasIndex(q => q.PracticeId);

            modelBuilder.Entity<Question>()
                .HasIndex(q => q.PerformanceId);

            modelBuilder.Entity<Performance>()
            .HasOne(p => p.Chronicle)
            .WithMany(b => b.Performances)
            .HasForeignKey(p => p.ChronicleId);

            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Chapter)
                .WithMany(b => b.Performances)
                .HasForeignKey(p => p.ChapterId);

            modelBuilder.Entity<Performance>()
                .HasMany(p => p.Questions)
                .WithOne(b => b.Performance)
                .HasForeignKey(p => p.PerformanceId);
            // performance record relations to user and performance
            modelBuilder.Entity<PerformanceRecord>()
                .HasIndex(p => p.User);

            modelBuilder.Entity<PerformanceRecord>()
                .HasIndex(p => p.Performance);

            modelBuilder.Entity<PerformanceAttempt>()
            .HasOne(p => p.PerformanceRecord)
            .WithMany(b => b.PerformanceAttempts)
            .HasForeignKey(p => p.PerformanceRecord);
        }
    }
}

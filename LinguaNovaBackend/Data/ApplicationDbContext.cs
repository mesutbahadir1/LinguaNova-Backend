using Microsoft.EntityFrameworkCore;
//using LinguaNova.Models;

namespace LinguaNova.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public DbSet<ArticleTest> ArticleTests { get; set; }
        public DbSet<VideoTest> VideoTests { get; set; }
        public DbSet<AudioTest> AudioTests { get; set; }
        public DbSet<UserArticleProgress> UserArticleProgresses { get; set; }
        public DbSet<UserVideoProgress> UserVideoProgresses { get; set; }
        public DbSet<UserAudioProgress> UserAudioProgresses { get; set; }
        public DbSet<TestResult> TestResults { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            
        }
    }
} 
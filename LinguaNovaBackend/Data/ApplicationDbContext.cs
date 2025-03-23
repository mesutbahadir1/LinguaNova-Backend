using Microsoft.EntityFrameworkCore;
using LinguaNovaBackend.Models;

namespace LinguaNovaBackend.Data
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
        public DbSet<UserTestProgress> UserTestProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Article - ArticleTest relationship
            modelBuilder.Entity<ArticleTest>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticleTests)
                .HasForeignKey(at => at.ArticleId);

            // Video - VideoTest relationship
            modelBuilder.Entity<VideoTest>()
                .HasOne(vt => vt.Video)
                .WithMany(v => v.VideoTests)
                .HasForeignKey(vt => vt.VideoId);

            // Audio - AudioTest relationship
            modelBuilder.Entity<AudioTest>()
                .HasOne(at => at.Audio)
                .WithMany(a => a.AudioTests)
                .HasForeignKey(at => at.AudioId);

            // User - UserTestProgress relationship
            modelBuilder.Entity<UserTestProgress>()
                .HasOne(utp => utp.User)
                .WithMany(u => u.TestProgress)
                .HasForeignKey(utp => utp.UserId);

            // UserTestProgress - ArticleTest relationship
            modelBuilder.Entity<UserTestProgress>()
                .HasOne(utp => utp.ArticleTest)
                .WithMany(at => at.UserTestProgresses)
                .HasForeignKey(utp => utp.ArticleTestId);

            // UserTestProgress - VideoTest relationship
            modelBuilder.Entity<UserTestProgress>()
                .HasOne(utp => utp.VideoTest)
                .WithMany(vt => vt.UserTestProgresses)
                .HasForeignKey(utp => utp.VideoTestId);

            // UserTestProgress - AudioTest relationship
            modelBuilder.Entity<UserTestProgress>()
                .HasOne(utp => utp.AudioTest)
                .WithMany(at => at.UserTestProgresses)
                .HasForeignKey(utp => utp.AudioTestId);
        }
    }
} 
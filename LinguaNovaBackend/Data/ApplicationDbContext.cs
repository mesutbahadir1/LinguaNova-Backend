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
        public DbSet<UserArticleProgress> UserArticleProgresses { get; set; }
        public DbSet<UserAudioProgress> UserAudioProgresses { get; set; }
        public DbSet<UserVideoProgress> UserVideoProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - UserArticleProgress relationship
            modelBuilder.Entity<UserArticleProgress>()
                .HasOne(uap => uap.User)
                .WithMany()
                .HasForeignKey(uap => uap.UserId);

            modelBuilder.Entity<UserArticleProgress>()
                .HasOne(uap => uap.Article)
                .WithMany()
                .HasForeignKey(uap => uap.ArticleId);

            // User - UserAudioProgress relationship
            modelBuilder.Entity<UserAudioProgress>()
                .HasOne(uap => uap.User)
                .WithMany()
                .HasForeignKey(uap => uap.UserId);

            modelBuilder.Entity<UserAudioProgress>()
                .HasOne(uap => uap.Audio)
                .WithMany()
                .HasForeignKey(uap => uap.AudioId);

            // User - UserVideoProgress relationship
            modelBuilder.Entity<UserVideoProgress>()
                .HasOne(uvp => uvp.User)
                .WithMany()
                .HasForeignKey(uvp => uvp.UserId);

            modelBuilder.Entity<UserVideoProgress>()
                .HasOne(uvp => uvp.Video)
                .WithMany()
                .HasForeignKey(uvp => uvp.VideoId);
        }
    }
}
using MauiApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Story> Stories { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Scene> Scenes { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<StoryResult> StoryResults { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<TutorialAnswer> TutorialAnswers { get; set; }

        public DbSet<ImpactAnswer> ImpactAnswers { get; set; }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<UserChapter> UserChapters { get; set; }
        
        public DbSet<Emotion> Emotions { get; set; }
        
        public DbSet<ImageType> ImageTypes { get; set; }
        
        public DbSet<QuestionType> QuestionTypes { get; set; }
        
        public DbSet<Subscription> Subscriptions { get; set; }
        
        public DbSet<SceneType> SceneTypes { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=envndb;User Id=postgres;Password=root");
            }
        }
    }
}
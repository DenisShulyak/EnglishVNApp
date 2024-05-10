using MauiAppSqlite.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiAppSqlite;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite();
    }
    
    public DbSet<Story> Stories { get; set; }
    
    public DbSet<Chapter> Chapters { get; set; }
    
    public DbSet<Scene> Scenes { get; set; }
    
    public DbSet<Image> Images { get; set; }
    
    public DbSet<Speaker> Speakers { get; set; }
    
    public DbSet<StoryParameter> StoryParameters { get; set; }
    
    public DbSet<Question> Questions { get; set; }
    
    public DbSet<TutorialAnswer> TutorialAnswers { get; set; }
    
    public DbSet<ImpactAnswer> ImpactAnswers { get; set; }
}
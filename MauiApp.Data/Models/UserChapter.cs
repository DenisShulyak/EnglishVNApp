using MauiApp.Data.Models.Abstract;

namespace MauiApp.Data.Models;

public class UserChapter : Entity
{
    public Guid UserId { get; set; }
    
    public User User { get; set; }
    
    public Guid ChapterId { get; set; }
    
    public Chapter Chapter { get; set; }
    
    public bool IsActive { get; set; }
}
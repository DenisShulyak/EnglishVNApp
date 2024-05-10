using MauiAppSqlite.Models.Abstract;

namespace MauiAppSqlite.Models;

/// <summary>
/// Глава
/// </summary>
public class Chapter : Entity
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Номер
    /// </summary>
    public int Number { get; set; }
    
    /// <summary>
    /// История
    /// </summary>
    public Guid StoryId { get; set; }
    
    /// <summary>
    /// История
    /// </summary>
    public Story Story { get; set; }
    
    /// <summary>
    /// Фон
    /// </summary>
    public Guid ImageId { get; set; }
    
    /// <summary>
    /// Фон
    /// </summary>
    public Image Image { get; set; }
}
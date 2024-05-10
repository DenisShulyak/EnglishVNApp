using MauiAppSqlite.Models.Abstract;

namespace MauiAppSqlite.Models;

/// <summary>
/// Статистика истории
/// </summary>
public class StoryParameter : Entity
{
    /// <summary>
    /// История
    /// </summary>
    public Guid StoryId { get; set; }
    
    /// <summary>
    /// История
    /// </summary>
    public Story Story { get; set; }
    
    /// <summary>
    /// Счетчик ветви сюжета
    /// </summary>
    public int MoralCount { get; set; }
}
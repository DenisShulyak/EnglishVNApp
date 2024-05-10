using MauiAppSqlite.Enums;
using MauiAppSqlite.Models.Abstract;

namespace MauiAppSqlite.Models;

/// <summary>
/// Сцена
/// </summary>
public class Scene : Entity
{
    /// <summary>
    /// Контент сцены
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Тип сцены
    /// </summary>
    public SceneType SceneType { get; set; }
    
    /// <summary>
    /// MoralType
    /// </summary>
    public MoralType MoralType { get; set; }
    
    /// <summary>
    /// Номер сцены
    /// </summary>
    public int Number { get; set; }
    
    /// <summary>
    /// Говорящий персонаж
    /// </summary>
    public Guid SpeakerId { get; set; }
    
    /// <summary>
    /// Говорящий персонаж
    /// </summary>
    public Speaker Speaker { get; set; }
    
    /// <summary>
    /// Глава
    /// </summary>
    public Guid ChapterId { get; set; }
    
    /// <summary>
    /// Глава
    /// </summary>
    public Chapter Chapter { get; set; }
    
    /// <summary>
    /// Изображение
    /// </summary>
    public Guid ImageId { get; set; }
    
    /// <summary>
    /// Изображение
    /// </summary>
    public Image Image { get; set; }
}
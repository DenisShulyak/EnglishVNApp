using MauiAppSqlite.Enums;
using MauiAppSqlite.Models.Abstract;

namespace MauiAppSqlite.Models;

/// <summary>
/// Изображение
/// </summary>
public class Image : Entity
{
    /// <summary>
    /// Путь к изображению
    /// </summary>
    public string Path { get; set; }
    
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Тип изображения
    /// </summary>
    public ImageType ImageType { get; set; }
}
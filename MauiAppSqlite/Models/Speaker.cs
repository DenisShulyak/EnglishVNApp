using MauiAppSqlite.Models.Abstract;

namespace MauiAppSqlite.Models;

/// <summary>
/// Говорящий персонаж
/// </summary>
public class Speaker : Entity
{
    /// <summary>
    /// Имя персонажа
    /// </summary>
    public string Name { get; set; }
}
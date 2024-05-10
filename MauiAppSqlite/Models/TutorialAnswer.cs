using MauiAppSqlite.Models.Abstract;

namespace MauiAppSqlite.Models;

/// <summary>
/// Практикующий ответ
/// </summary>
public class TutorialAnswer : Answer
{
    /// <summary>
    /// Корректный
    /// </summary>
    public bool IsCorrect { get; set; }
}
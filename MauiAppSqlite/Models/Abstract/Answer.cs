namespace MauiAppSqlite.Models.Abstract;

/// <summary>
/// Ответ на вопрос
/// </summary>
public abstract class Answer : Entity
{
    /// <summary>
    /// Вопрос
    /// </summary>
    public Guid QuestionId { get; set; }
    
    /// <summary>
    /// Вопрос
    /// </summary>
    public Question Question { get; set; }
    
    /// <summary>
    /// Ответ
    /// </summary>
    public string Text { get; set; }
}
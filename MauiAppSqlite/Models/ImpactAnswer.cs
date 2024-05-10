using MauiAppSqlite.Enums;
using MauiAppSqlite.Models.Abstract;

namespace MauiAppSqlite.Models;

/// <summary>
/// Сюжетный ответ
/// </summary>
public class ImpactAnswer : Answer
{
    /// <summary>
    /// 
    /// </summary>
    public MoralType MoralType { get; set; }
}
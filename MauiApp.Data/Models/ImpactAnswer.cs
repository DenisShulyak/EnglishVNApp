using MauiApp.Data.Models.Abstract;

namespace MauiApp.Data.Models
{

    /// <summary>
    /// Сюжетный ответ
    /// </summary>
    public class ImpactAnswer : Answer
    {
        public Guid EmotionId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public Emotion Emotion { get; set; }
    }
}
using MauiApp.Data.Models.Abstract;

namespace MauiApp.Data.Models
{

    /// <summary>
    /// Статистика истории
    /// </summary>
    public class StoryResult : Entity
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
        
        /// <summary>
        /// Пользователь
        /// </summary>
        public Guid UserId { get; set; }
        
        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }
    }
}
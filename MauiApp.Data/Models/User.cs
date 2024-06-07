using MauiApp.Data.Models.Abstract;

namespace MauiApp.Data.Models
{

    public class User : Entity
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; set; }
        
        public Guid SubscriptionId { get; set; }
        /// <summary>
        /// Подписка
        /// </summary>
        public Subscription Subscription { get; set; }
        
        /// <summary>
        /// Конец подписки
        /// </summary>
        public DateTime? EndSubscriptionDate { get; set; }
        
        /// <summary>
        /// Количество жизней
        /// </summary>
        public int NumberOfLives { get; set; }
    }
}
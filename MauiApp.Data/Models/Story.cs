using MauiApp.Data.Models.Abstract;

namespace MauiApp.Data.Models
{

    /// <summary>
    /// История
    /// </summary>
    public class Story : Entity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Фон
        /// </summary>
        public Guid ImageId { get; set; }

        /// <summary>
        /// Фон
        /// </summary>
        public Image Image { get; set; }
    }
}
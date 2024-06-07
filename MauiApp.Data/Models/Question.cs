using MauiApp.Data.Models.Abstract;

namespace MauiApp.Data.Models
{

    /// <summary>
    /// Вопрос
    /// </summary>
    public class Question : Entity
    {
        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Сцена
        /// </summary>
        public Guid SceneId { get; set; }

        /// <summary>
        /// Сцена
        /// </summary>
        public Scene Scene { get; set; }

        public Guid QuestionId { get; set; }
        /// <summary>
        /// Тип вопроса
        /// </summary>
        public QuestionType QuestionType { get; set; }

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
using MauiApp.Data.Models.Abstract;

namespace MauiApp.Data.Models
{

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

        
        public Guid ImageTypeId { get; set; }
        
        /// <summary>
        /// Тип изображения
        /// </summary>
        public ImageType ImageType { get; set; }
    }
}
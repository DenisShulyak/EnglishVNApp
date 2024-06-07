using MauiApp.Data.Models.Abstract;

namespace MauiApp.Data.Models
{

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
}
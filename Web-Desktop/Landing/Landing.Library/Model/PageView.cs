using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Landing.Library.Model
{
    [Table("PageView")]
    public class PageView
    {
        public int Id { get; set; }
        /// <summary>
        /// Заголовок шапки на главной странице
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string HeaderName { get; set; }
        /// <summary>
        /// Название кнопки в шапке страницы
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string HeaderButtton { get; set; }
        /// <summary>
        /// Название формы отправки заявок
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string ApplicationHead { get; set; }
        /// <summary>
        /// Координыты для карты в разделе контакты
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Coordinates { get; set; }
        /// <summary>
        /// Цвет метки на карте
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string LabelColor { get; set; }
        /// <summary>
        /// Информация о контактах
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string? ContactInfo { get; set; }
        /// <summary>
        /// Идентификатор картинки шапки
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public int HeaderImageId { get; set; }
        /// <summary>
        /// Токен для телеграмм бота
        /// </summary>
        public string TelegramToken { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        [ForeignKey("HeaderImageId")]
        public Images GetImages { get; set; }
    }
}

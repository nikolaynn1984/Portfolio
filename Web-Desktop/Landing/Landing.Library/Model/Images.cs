using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Landing.Library.Model
{
    /// <summary>
    /// Список картинок/фото
    /// </summary>
    [Table("Images")]
    public class Images
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Название кртинки
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Name { get; set; }
        /// <summary>
        /// Расположение папки
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Location { get; set; }
        /// <summary>
        /// Расширение файла
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Extension { get; set; }
        /// <summary>
        /// Размер - байты
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public long Size { get; set; }
        /// <summary>
        /// Описание - используется для Alt в разметке
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Description { get; set; }
        /// <summary>
        /// ХэшКод
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string HashCode { get; set; }
    }
}

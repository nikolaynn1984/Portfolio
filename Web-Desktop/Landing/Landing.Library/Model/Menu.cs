using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Landing.Library.Model
{
    [Table("Menu")]
    public class Menu
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Название не можут быть меньше 3 и больше 10 символов")]
        public string Name { get; set; }
        /// <summary>
        /// Имя контроллера
        /// </summary>
        public string Contoller { get; set; }
        /// <summary>
        /// Название метода
        /// </summary>
        public string Method { get; set; }
    }
}

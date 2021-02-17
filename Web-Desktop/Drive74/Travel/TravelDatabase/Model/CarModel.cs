using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelDatabase.Model
{
    /// <summary>
    /// Модели автомобилей
    /// </summary>
    [Table("CarModel")]
    public class CarModel
    {
        /// <summary>
        /// ИД
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Модель автомобиля
        /// </summary>
        [Required(ErrorMessage = "Не заполнено поле Модель")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Недопустимая длина поля")]
        public string ModelName { get; set; }
        /// <summary>
        /// ИД Марка автомобиля
        /// </summary>
        [Range(1, 200, ErrorMessage = "Требуется выбрать марку автомобиля")]
        public int ModelMarkId { get; set; }
        /// <summary>
        /// Разрешение 
        /// </summary>
        public bool ModelResolution { get; set; }

        [ForeignKey("ModelMarkId")]
        public virtual CarMark GetCarMark { get; set; }
    }
}

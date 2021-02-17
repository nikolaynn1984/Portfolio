using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelDatabase.Model
{
    /// <summary>
    /// Автомобили
    /// </summary>
    [Table("Cars")]
    public class Car
    {
        /// <summary>
        /// Ид
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Марка автомобиля
        /// </summary>
        [Required(ErrorMessage = "Не указана марка автомобиля")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Недопустимое число букв")]
        public int CarMarkId { get; set; }
        /// <summary>
        /// Модель автомобиля
        /// </summary>
        [Required(ErrorMessage = "Не указана модель автомобиля")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Недопустимое число букв")]
        public int CarModelId { get; set; }
        /// <summary>
        /// Год выпуска автомобиля
        /// </summary>
        [Required]
        [Range(1900, 2100, ErrorMessage = "Недопустимое значение год выпуска")]
        public int Years { get; set; }
        //public int TypeOfCarId { get; set; }
        //public string CarColor { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 7, ErrorMessage = "Не правильно заполнено поле")]
        public string PlaceNumber { get; set; }
        //public int STSNumber { get; set; }
        //public int QtyPlace { get; set; }
        //public int UserId { get; set; }
        //public bool CarResolution { get; set; }

        //[ForeignKey("CarMarkId")]
        //public virtual CarMark GetMark { get; set; }

        //[ForeignKey("CarModelId")]
        //public virtual CarModel GetModel { get; set; }

    }
}

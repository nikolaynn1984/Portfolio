using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelDatabase.Model
{
    /// <summary>
    /// Марки автомобилей
    /// </summary>
    [Table("CarMark")]
    public class CarMark
    {
        /// <summary>
        /// Ид
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Марка автомобиля
        /// </summary>
        [Required(ErrorMessage = "Не заполнено поле Марка")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Недопустимая длина поля")]
        public string MarkName { get; set; }
        /// <summary>
        /// Разрешение 
        /// </summary>
        public bool MarkResolution { get; set; }
    }
}

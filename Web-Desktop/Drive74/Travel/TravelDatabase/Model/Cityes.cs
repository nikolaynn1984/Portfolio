using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelDatabase.Model
{
    /// <summary>
    /// Города
    /// </summary>
    [Table("Cityes")]
    public class Cityes
    {
        /// <summary>
        /// Ид
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        [Required(ErrorMessage = "Не указано название города")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Недопустимая длина поля - мин 2, макс 50")]
        public string City { get; set; }
        /// <summary>
        /// Ид Региона
        /// </summary>
        [Range(1, 1000, ErrorMessage = "Требуется выбрать регион")]
        public int RegionId { get; set; }
        /// <summary>
        /// Блокировка (блокирует создание поездок в городах)
        /// </summary>
        public bool Acting { get; set; }

        //[ForeignKey("RegionId")]
        //public virtual Regions GetRegions { get; set; }
    }
}

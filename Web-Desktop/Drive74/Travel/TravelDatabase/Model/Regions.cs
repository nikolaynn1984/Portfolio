using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelDatabase.Model
{
    /// <summary>
    /// Регионы/области
    /// </summary>
    [Table("Regions")]
    public class Regions
    {
        /// <summary>
        /// Ид
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Название региона/области
        /// </summary>
        [Required(ErrorMessage = "Не казано название региона/области")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Не допустимая длина поля")]
        public string Region { get; set; }
        /// <summary>
        /// Номер региона
        /// </summary>
        [Range(1, 1000, ErrorMessage = "Не указан номер региона")]
        public int RegionNumber { get; set; }
        /// <summary>
        /// Блокировка (блокирует создание поездок в регионе)
        /// </summary>
        public bool RegionActing { get; set; }
    }
}

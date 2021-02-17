using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelDatabase.Model
{
    /// <summary>
    /// Поездки
    /// </summary>
    [Table("Ride")]
    public class Ride
    {
        /// <summary>
        /// Ид
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// От куда - ИД город
        /// </summary>
        [Range(1, 500)]
        public int FromCityId { get; set; }
        /// <summary>
        /// Куда - ИД город
        /// </summary>
        [Range(1, 500)]
        public int ToCityId { get; set; }
        /// <summary>
        /// Дата поездки
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime RideDate { get; set; }

      

        //[Required]
        //[DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode =true)]
        //public TimeSpan RideTime { get; set; }
        /// <summary>
        /// Цена поездки
        /// </summary>
        [Range(1, 100)]
        public int FreePlace { get; set; }
        /// <summary>
        /// Ид Машины
        /// </summary>
        [Required]
        [Range(1, Double.MaxValue)]
        public int CarId { get; set; }
        /// <summary>
        /// Цена поездки
        /// </summary>
        [Range(1, Double.MaxValue)]
        public decimal Price { get; set; }
        /// <summary>
        /// Статус ИД
        /// </summary>
        public int StatusId { get; set; }

        [ForeignKey("FromCityId")]
        public virtual Cityes FromCityes { get; set; }

        [ForeignKey("ToCityId")]
        public virtual Cityes ToCityes { get; set; }

        [ForeignKey("CarId")]
        public virtual Car GetCar { get; set; }

        [ForeignKey("StatusId")]
        public virtual RideStatus GetStatus { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTravelApp.Model
{
    /// <summary>
    /// Автомобили
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Ид
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Марка автомобиля
        /// </summary>
        public int CarMarkId { get; set; }
        /// <summary>
        /// Модель автомобиля
        /// </summary>
        public int CarModelId { get; set; }
        /// <summary>
        /// Год выпуска автомобиля
        /// </summary>
        public int Years { get; set; }
        //public int TypeOfCarId { get; set; }
        //public string CarColor { get; set; }
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

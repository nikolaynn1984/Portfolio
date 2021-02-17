using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WpfTravelApp.Model
{
    /// <summary>
    /// Города
    /// </summary>
    public class Cityes
    {
        /// <summary>
        /// Ид
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Ид Региона
        /// </summary>
        public int RegionId { get; set; }
        /// <summary>
        /// Блокировка (блокирует создание поездок в городах)
        /// </summary>
        public bool Acting { get; set; }

        //[ForeignKey("RegionId")]
        //public virtual Regions GetRegions { get; set; }
    }
}

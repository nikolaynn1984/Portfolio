using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WpfTravelApp.Model
{
    /// <summary>
    /// Регионы/области
    /// </summary>
    public class Regions
    {
        /// <summary>
        /// Ид
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название региона/области
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// Номер региона
        /// </summary>
        public int RegionNumber { get; set; }
        /// <summary>
        /// Блокировка (блокирует создание поездок в регионе)
        /// </summary>
        public bool RegionActing { get; set; }
    }
}

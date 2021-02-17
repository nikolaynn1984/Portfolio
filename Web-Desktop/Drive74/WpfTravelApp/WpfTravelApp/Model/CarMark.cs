using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WpfTravelApp.Model
{
    /// <summary>
    /// Марки автомобилей
    /// </summary>
    public class CarMark
    {
        /// <summary>
        /// Ид
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Марка автомобиля
        /// </summary>
        public string MarkName { get; set; }
        /// <summary>
        /// Разрешение 
        /// </summary>
        public bool MarkResolution { get; set; }
    }
}

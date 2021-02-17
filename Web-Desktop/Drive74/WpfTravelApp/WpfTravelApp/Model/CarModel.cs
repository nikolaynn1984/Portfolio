using System;
using System.Collections.Generic;
using System.Text;

namespace WpfTravelApp.Model
{
    /// <summary>
    /// Модели автомобилей
    /// </summary>
    public class CarModel
    {
        /// <summary>
        /// ИД
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Модель автомобиля
        /// </summary>
        public string ModelName { get; set; }
        /// <summary>
        /// ИД Марка автомобиля
        /// </summary>
        public int ModelMarkId { get; set; }
        /// <summary>
        /// Разрешение 
        /// </summary>
        public bool ModelResolution { get; set; }

    }
}

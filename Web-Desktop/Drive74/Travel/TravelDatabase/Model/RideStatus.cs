using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TravelDatabase.Model
{
    /// <summary>
    /// Статус поездки
    /// </summary>
    [Table("RadeStatus")]
    public class RideStatus
    {
        /// <summary>
        /// Ид
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; }
    }
}

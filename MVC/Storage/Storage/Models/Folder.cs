using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Storage.Models
{
    /// <summary>
    /// Папки
    /// </summary>
    [Table("Folder")]
    public class Folder
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        [Required]
        public int ParentId { get; set; }
    }
}
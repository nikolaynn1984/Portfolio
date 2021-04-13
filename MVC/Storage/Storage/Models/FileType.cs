using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Storage.Models
{
    /// <summary>
    /// Типы файлов
    /// </summary>
    [Table("FileType")]
    public class FileType
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Иконка
        /// </summary>
        public string Icon { get; set; }
    }
}
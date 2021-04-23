using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Storage.Models
{
    /// <summary>
    /// Файлы
    /// </summary>
    [Table("File")]
    public class StoreFile
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Идентификатор типа файла
        /// </summary>
        public int FileTypeId { get; set; }
        /// <summary>
        /// Идентификатор папки
        /// </summary>
        public int FolderId { get; set; }
        /// <summary>
        /// Контент
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        [ForeignKey("FileTypeId")]
        public virtual FileType Type { get; set; }

        public IEnumerable<FileType> GetTypes { get; set; }
    }
}
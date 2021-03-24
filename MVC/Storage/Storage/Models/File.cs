using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Storage.Models
{
    [Table("File")]
    public class File
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FileTypeId { get; set; }
        public int FolderId { get; set; }
        public string Content { get; set; }

        [ForeignKey("FileTypeId")]
        public virtual FileType Type { get; set; }

        public IEnumerable<FileType> GetTypes { get; set; }
    }
}
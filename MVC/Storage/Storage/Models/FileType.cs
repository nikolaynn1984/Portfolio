using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Storage.Models
{
    [Table("FileType")]
    public class FileType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
    }
}
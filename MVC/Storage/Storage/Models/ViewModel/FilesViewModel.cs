using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Storage.Models.ViewModel
{
    public class FilesViewModel
    {
        public int? Seed { get; set; }
        public IEnumerable<Folder> GetFolders { get; set; }
        public IEnumerable<File> GetFiles { get; set; }
    }
}
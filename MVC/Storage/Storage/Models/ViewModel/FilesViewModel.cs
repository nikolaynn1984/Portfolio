using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Storage.Models.ViewModel
{
    /// <summary>
    /// Модель представления дерева папок и файлов
    /// </summary>
    public class FilesViewModel
    {
        /// <summary>
        /// Принадлежность идентификатору родителя
        /// </summary>
        public int? Seed { get; set; }
        /// <summary>
        /// Список папок
        /// </summary>
        public IEnumerable<Folder> GetFolders { get; set; }
        /// <summary>
        /// Список файлов
        /// </summary>
        public IEnumerable<File> GetFiles { get; set; }
    }
}
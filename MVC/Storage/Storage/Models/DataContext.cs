using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Storage.Models
{
    /// <summary>
    /// Контекст данных
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext() : base("StorageDatabase") { }

        /// <summary>
        /// Файлы
        /// </summary>
        public DbSet<File> Files { get; set; }
        /// <summary>
        /// Типы файлов
        /// </summary>
        public DbSet<FileType> FileTypes { get; set; }
        /// <summary>
        /// Папки
        /// </summary>
        public DbSet<Folder> Folders { get; set; }
    }
}
using Storage.Models;
using Storage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Storage.Repository
{
    /// <summary>
    /// Папки
    /// </summary>
    public class FolderRepository : IFolders
    {
        private readonly DataContext context;
        private readonly FilesRepository files;

        public FolderRepository()
        {
            context = new DataContext();
            this.files = new FilesRepository();
        }

        public List<Folder> Get()
        {
            return context.Folders.ToList();
        }

        public bool Add(int id, string name, int parentid, out Folder file)
        {
            bool result = true;
            file = new Folder()
            {
                Id = id,
                Name = name,
                ParentId = parentid
            };
            try
            {
                if (file.Id == 0)
                {
                    context.Folders.Add(file);
                    context.SaveChanges();
                }
                else
                {
                    var confile = context.Folders.Find(file.Id);
                    confile.Name = file.Name;
                    context.SaveChanges();
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }



        public bool Remove(int id)
        {
            bool result = true;
            try
            {
                var item = context.Folders.Find(id);

                var parent = context.Folders.Where(s => s.ParentId == item.Id).ToList();

                RemoveParentFolder(parent);
                files.RemoveParent(item.Id);// RemoveFiles(item.Id);
                context.Folders.Remove(item);
                context.SaveChanges();
            }
            catch
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Удаление подпапок
        /// </summary>
        /// <param name="folder">Список папок</param>
        private void RemoveParentFolder(List<Folder> folder)
        {
            foreach (var item in folder)
            {
                context.Folders.Remove(item);
                var parent = context.Folders.Where(s => s.ParentId == item.Id).ToList();
                files.RemoveParent(item.Id);// RemoveFiles(item.Id);
                RemoveParentFolder(parent);
            }
        }
    }
}
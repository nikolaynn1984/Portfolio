using Storage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Storage.Repository
{
    public class FilesRepository
    {
        private DataContext data = new DataContext();
        private List<Folder> folders;
        private List<Models.File> files;
        private List<FileType> fileTypes;
        public FilesRepository()
        {
            this.folders = new List<Folder>();
            this.files = new List<Models.File>();
            this.fileTypes = new List<FileType>();
        }

        public List<Folder> GetFolders()
        {
            using(DataContext context = new DataContext())
            {
                folders = context.Folders.ToList();

                return folders;
            }
        }
        public List<Models.File> GetFiles()
        {
            using(DataContext context = new DataContext())
            {
                files = context.Files.ToList();
                foreach(var type in files)
                {
                    type.Type = context.FileTypes.Find(type.FileTypeId);
                }
                return files;
            }
        }
        /// <summary>
        /// Добавление папки
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Название</param>
        /// <param name="parentid">Идентификатор подпапки</param>
        /// <param name="file">Выходные данные папки</param>
        /// <returns>false - если вызвано исключение, в противном случае true</returns>
        public bool AddFolder(int id, string name, int parentid, out Folder file)
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
                
                using (DataContext context = new DataContext())
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
            }
            catch
            {
                result = false;
            }
           
            return result;
        }
        /// <summary>
        /// Удалени папки и все его корни
        /// </summary>
        /// <param name="id">Идентификатор папки</param>
        /// <returns>false - если вызвано исключение, в противном случае true</returns>
        public bool RemoveFolder(int id)
        {
            bool result = true;
            try
            {
                    var item = data.Folders.Find(id);

                    var parent = data.Folders.Where(s => s.ParentId == item.Id).ToList();

                RemoveParentFolder(parent);
                RemoveFiles(item.Id);
                data.Folders.Remove(item);
                data.SaveChanges();
                data.Dispose();
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public async Task<Models.File> UploadFile(HttpPostedFileBase upload, int parentid)
        {
            Models.File file = new Models.File();
            string resulte = new StreamReader(upload.InputStream).ReadToEnd();
            int maxSize = 10 * 1024 * 1024;
            var type = GetTypeByName(upload.FileName);
            try
            {
                if (upload.ContentLength > maxSize) return null;

                if (type.Id == 9) file.Content = "Не известный формат файла";
                else file.Content = resulte;
                if (parentid == 0) file.FolderId = 1;
                else file.FolderId = parentid;
                file.FileTypeId = type.Id;
                file.Name = upload.FileName;
                file.Description = upload.FileName;
                file.Type = type;

                 data.Files.Add(file);
                await data.SaveChangesAsync();
                data.Dispose();
                
            }
            catch
            {
                return null;
            }


            return file;
        }

        /// <summary>
        /// Удаление подпапок
        /// </summary>
        /// <param name="folder">Список папок</param>
        private void RemoveParentFolder(List<Folder> folder)
        {
            foreach (var item in folder)
            {
                data.Folders.Remove(item);
                    var parent = data.Folders.Where(s => s.ParentId == item.Id).ToList();
                     RemoveFiles(item.Id);
                    RemoveParentFolder(parent);
            }
        }
        /// <summary>
        /// Удаление файлов
        /// </summary>
        /// <param name="id">идентификатор папки</param>
        private void RemoveFiles(int id)
        {
            var items = data.Files.Where(s => s.FolderId == id).ToList();
            foreach (var file in items) data.Files.Remove(file);
        }

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="id">Идентификатор файла</param>
        /// <returns>false - если вызвано исключение, в противном случае true</returns>
        public bool RemoveFile(int id)
        {
            bool resul = true;
            try
            {
                using(DataContext context = new DataContext())
                {
                    var item = context.Files.Find(id);
                    context.Files.Remove(item);
                    context.SaveChanges();
                }
            }
            catch
            {
                resul = false;
            }
            return resul;
        }
        /// <summary>
        /// Переименовать названия файла
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Название</param>
        /// <returns>false - если вызвано исключение, в противном случае true</returns>
        public bool RenameFile(int id, string name)
        {
            bool result = true;
            try
            {
                using(DataContext context = new DataContext())
                {
                    var file = context.Files.SingleOrDefault(x => x.Id == id);
                    file.Name = name;
                    context.SaveChanges();
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

      

        /// <summary>
        /// Тип файла по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Тип дайла</returns>
        public FileType GetTypeById(int id)
        {
            try
            {
                var item = data.FileTypes.Find(id);
                data.Dispose();
                return item;
                
            }
            catch
            {
                data.Dispose();
                return null;
            }
        }

        public FileType GetTypeByName(string filename)
        {
            var filetype = new FileType();
            try
            {
                string extension = Path.GetExtension(filename);
                filetype = data.FileTypes.SingleOrDefault(s => s.Type == extension);

                if (filetype == null) filetype = data.FileTypes.Find(9);
            }
            catch
            {
                filetype = data.FileTypes.Find(9);
            }
            
            

            return filetype;
        }
        /// <summary>
        /// Получить данные файла по идентификатору
        /// </summary>
        /// <param name="id">/Идентификатор</param>
        /// <returns>Модель фаил</returns>
        public async Task<Models.File> GetFileById(int id)
        {
            try
            {
                var file = new Models.File();
                using(DataContext context = new DataContext())
                {
                    await context.Files.ForEachAsync(x =>
                    {
                        if(x.Id == id)
                        {
                            file.Id = x.Id;
                            file.Name = x.Name;
                            file.Content = x.Content;
                        }
                    });
                }
                if (file != null) return file;
                else return null;
            }
            catch
            {
                return null;
            }
        }

        public string LoadFile(int id)
        {
            var item = data.Files.SingleOrDefault(s => s.Id == id);
            string path = HttpContext.Current.Server.MapPath($"\\Content\\temporary\\" + item.Name);

            System.IO.File.WriteAllText(path, item.Content);
            return path;
        }
    }
}
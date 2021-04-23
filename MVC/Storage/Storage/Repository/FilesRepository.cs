using Storage.Models;
using Storage.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Storage.Repository
{
    /// <summary>
    /// Хранилище файлов
    /// </summary>
    public class FilesRepository : IFiles
    {
        private DataContext context;
        private List<FileType> fileTypes;
        public FilesRepository()
        {
            this.context = new DataContext();

            this.fileTypes = new List<FileType>();
        }

        /// <summary>
        /// Получить список файлов
        /// </summary>
        /// <returns>Список файлов</returns>
        public IEnumerable<StoreFile> Get()
        {
                var files =  context.Files.ToList();
                fileTypes = context.FileTypes.ToList();
                foreach(var type in files)
                {
                    type.Type = fileTypes.SingleOrDefault(s => s.Id == type.FileTypeId);
                }
                return files;
        } 


        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="upload">Фаил</param>
        /// <param name="parentid">Идентификатор родителя</param>
        /// <returns>Модель данных</returns>
        public async Task<StoreFile> Upload(HttpPostedFileBase upload, int parentid)
        {
            StoreFile file = new StoreFile();
            string resulte = new StreamReader(upload.InputStream).ReadToEnd();
            int maxSize = 10 * 1024 * 1024;
            int defaultType = 9;
            var type = GetTypeByName(upload.FileName);
            try
            {
                if (upload.ContentLength > maxSize) return null;

                if (type.Id == defaultType) 
                    file.Content = "Не известный формат файла"; 
                else 
                    file.Content = resulte;

                if (parentid == 0) 
                    file.FolderId = 1;
                else 
                    file.FolderId = parentid;

                file.FileTypeId = type.Id;
                file.Name = upload.FileName;
                file.Description = upload.FileName;
                file.Type = type;

                 context.Files.Add(file);
                await context.SaveChangesAsync();
                
            }
            catch
            {
                return null;
            }


            return file;
        }


        /// <summary>
        /// Удаление файлов
        /// </summary>
        /// <param name="id">идентификатор папки</param>
        public async void RemoveParent(int id)
        {
            var items = await context.Files.Where(s => s.FolderId == id).ToListAsync();
            foreach (var file in items) context.Files.Remove(file);
        }  

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="id">Идентификатор файла</param>
        /// <returns>false - если вызвано исключение, в противном случае true</returns>
        public async Task<bool> Remove(int id)
        {
            bool resul = true;
            try
            {
                using(DataContext context = new DataContext())
                {
                    var item = context.Files.Find(id);
                    context.Files.Remove(item);
                    await context.SaveChangesAsync();
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
        public async Task<bool> Rename(int id, string name)
        {
            bool result = true;
            try
            {
                var file = context.Files.SingleOrDefault(x => x.Id == id);
                file.Name = name;
                await context.SaveChangesAsync();
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
                var item = context.FileTypes.Find(id);
                return item;
                
            }
            catch
            {
                return null;
            }
        }  
        /// <summary>
        /// Получить модель FileType по названию файла
        /// </summary>
        /// <param name="filename">Название файла</param>
        /// <returns>Модель данных</returns>
        public FileType GetTypeByName(string filename)
        {
            var filetype = new FileType();
            try
            {
                string extension = Path.GetExtension(filename);
                filetype = context.FileTypes.SingleOrDefault(s => s.Type == extension);

                if (filetype == null) filetype = context.FileTypes.Find(9);
            }
            catch
            {
                filetype = context.FileTypes.Find(9);
            }
            
            

            return filetype;
        }   
        /// <summary>
        /// Получить данные файла по идентификатору
        /// </summary>
        /// <param name="id">/Идентификатор</param>
        /// <returns>Модель фаил</returns>
        public async Task<StoreFile> GetById(int id)
        {
            try
            {
                var file = new StoreFile();

                await context.Files.ForEachAsync(x =>
                {
                    if(x.Id == id)
                    {
                        file.Id = x.Id;
                        file.Name = x.Name;
                        file.Content = x.Content;
                    }
                });

                if (file != null) return file;
                else return null;
            }
            catch
            {
                return null;
            }
        }  
        /// <summary>
        /// Получить путь к файлу по ID (Предварительно сохраняеи в директории)
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Путь к файлу</returns>
        public async Task<string> Load(int id)
        {
            var item = await context.Files.SingleOrDefaultAsync(s => s.Id == id);
            string path = HttpContext.Current.Server.MapPath($"\\Content\\temporary\\" + item.Name);

            System.IO.File.WriteAllText(path, item.Content);
            return path;
        }   


    }
}
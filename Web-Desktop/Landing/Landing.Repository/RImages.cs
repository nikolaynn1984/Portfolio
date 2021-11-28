using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Landing.Interface;
using Landing.Repository.Interfase;
using Landing.Model.Data;
using Landing.Repository.Loader;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

namespace Landing.Repository
{
    public class RImages : IImagesFile
    {
        public static event Action<Images> ImageEventChanged;
        public static event Action<Images> ImageLoadChanged;
        private readonly IDataHandler<Images> data;
        private readonly IFilesLanding files;
        
        private static Images images;
        public RImages(string url, string token)
        {
            images = new Images();
            this.data = new DataHandler<Images>(images, url, token);
            this.files = new FilesLanding(url, token);
        }

        /// <summary>
        /// Получить список изображений
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Images>> GetItems()
        {
            return await data.GetItems();
        }
        /// <summary>
        /// Получение информации о файле по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель с данными</returns>
        public async Task<Images> GetItemById(int id)
        {
            return await data.GetItemById(id); 
        }
        /// <summary>
        /// Удалить фаил
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Рузультат</returns>
        public async Task<(bool Result, string Error)> Delete(int id)
        {
            var item = await data.Delete(id);
            if (!item) return (Result: item, Error: "Что то пошло не так");
            return (Result: item, Error: "");
        }
        /// <summary>
        /// Скачивание файла по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Руть к файлу</returns>
        public async Task<string> Download(Images image)
        {
            string result = await files.Download(image);
            if (result != null)
            {
                if(string.IsNullOrEmpty(image.Location)) image = await GetItemById(image.Id);
                ImageLoadChanged?.Invoke(image);
            }
            return result;
        }
        /// <summary>
        /// Загрузка файла на сервер
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="location">Расположение</param>
        /// <param name="description">Описание файла</param>
        /// <returns>Информация о файле</returns>
        public async Task<Images> Upload(string path, FileLoacations location, string description)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(path, out string exteption);

            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            long filesize = stream.Length;
            byte[] bytes = new byte[filesize];
            stream.Read(bytes, 0, (int)filesize);

            var file = new Images()
            {
                Name = Path.GetFileName(path),
                Location = location.ToString(),
                Description = description,
                Extension = exteption
            };
            var item = await files.Upload(bytes, file);
            if (item != null) ImageEventChanged?.Invoke(item);

            return item;
        }

 
        /// <summary>
        /// Загрузка файла на сервер
        /// </summary>
        /// <param name="bytes">Массив бит файла</param>
        /// <param name="name">Название файла</param>
        /// <param name="location">Расположение</param>
        /// <param name="description">Описание файла</param>
        /// <returns>Информация о файле</returns>
        public async Task<Images> Upload(byte[] bytes, string name, FileLoacations location, string description)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(name, out string exteption);
            var img = new Images()
            {
                Name = name,
                Location = location.ToString(),
                Description = description,
                Extension = exteption
            };

            var item = await files.Upload(bytes, img);
            if (item != null) ImageEventChanged?.Invoke(item);

            return item;
        }

        #region Не требуется
        Task<Images> IImagesFile.Add(string location, string name, string description, string extension)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Images>> IImagesFile.GetItemByLocation(string location)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

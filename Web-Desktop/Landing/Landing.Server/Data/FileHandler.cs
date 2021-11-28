using Landing.Model.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Threading.Tasks;
using Landing.Interface;
using Landing.Server.Intarfase;

namespace Landing.Server.Data
{
    /// <summary>
    /// Обработка файлов
    /// </summary>
    public class FileHandler : IFilesHandler
    {
        private readonly IImagesFile img;

        public FileHandler(IImagesFile images)
        {
            img = images;
        }
        /// <summary>
        /// Скачивание файла по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Фаил</returns>
        public async Task<FileStreamResult> DowloadFile(int id)
        {
            Images file = await img.GetItemById(id);
            if (file == null)
            {
                return null;
            }

            string path = Path.Combine("Files", file.Location, file.Name);
            new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);
            FileStream stream = File.OpenRead(path);
            return new FileStreamResult(stream, contentType);
        }



        /// <summary>
        /// Загрузка файла 
        /// </summary>
        /// <param name="file">Фаил</param>
        /// <param name="location">Расположение</param>
        /// <param name="description">Описание</param>
        /// <returns>Модель данных</returns>
        public async Task<Images> UploadFile(IFormFile file, string location, string description)
        {
            Images images = new Images();
            try
            {
                if (file != null)
                {
                    string extension = Path.GetExtension(file.FileName);
                    string name = FileNameGenerator(location, extension);
                    string path = Path.Combine("Files", location, name);
                    string folder = Path.Combine("Files", location);
                    if (!File.Exists(path)) Directory.CreateDirectory(folder);
                    using (FileStream fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    if (File.Exists(path))
                    {
                        images = await img.Add(location, name, description, extension);
                    }
                }
            }
            catch
            {
                return null;
            }

            return images;
        }
        /// <summary>
        /// Генератор названия файла
        /// </summary>
        /// <param name="location">Папка расположения</param>
        /// <param name="extension">Расширение</param>
        /// <returns>Название</returns>
        public static string FileNameGenerator(string location, string extension)
        {
            DateTime date = DateTime.Now;
            string datetime = $"{date.Day}{date.Month}{date.Year}{date.Hour}{date.Minute}";
            string type;
            switch (location)
            {
                case "Icons":
                    type = "IC";
                    break;
                case "Header":
                    type = "HD";
                    break;
                case "Blog":
                    type = "BLG";
                    break;
                case "Project":
                    type = "PRJ";
                    break;
                case "Social":
                    type = "SOC";
                    break;
                case "Messages":
                    type = "MESS";
                    break;
                default: type = "DOC"; break;

            }
            return $"{type}{datetime}{extension}";
        }
    }
}

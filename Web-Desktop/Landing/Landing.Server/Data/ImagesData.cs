using Landing.Interface;
using Landing.Model.Data;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Landing.Model.Hash;
using Landing.Server.Database.Query;

namespace Landing.Server.Data
{
    /// <summary>
    /// Данные изображений
    /// </summary>
    public class ImagesData :  IImagesFile
    {
        private readonly BaseImages baseImages;
        public ImagesData()
        {
            this.baseImages = new BaseImages(Connection.GetConnectionString());
        }
        /// <summary>
        /// Получить все записи файлов
        /// </summary>
        /// <returns>Список</returns>
        public async Task<IEnumerable<Images>> GetItems()
        {
            return await baseImages.GetItems();
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<Images> GetItemById(int id)
        {
            return await baseImages.GetItemById(id);
        }


        /// <summary>
        /// Удаление записи 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true - если редактирование прошло успешно, в противном случае false</returns>
        public async Task<(bool Result, string Error)> Delete(int id)
        {
            bool result = true;
            string error = "Ok";
            try
            {
                Images item = await baseImages.GetItemById(id);
                if (item == null)
                {
                    return (Result: false, Error: "Идентификатор не найден");
                }
                string path = Path.Combine("Files", item.Location, item.Name);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                if (!File.Exists(path))
                {
                    result = await baseImages.Remove(id);
                }
                else
                {
                    return (Result: false, Error: "Что то пошло не так");
                }

                
            }
            catch
            {
                result = false;
                error = "Что то пошло не так";
            }
            return (Result: result, Error: error);
        }
        /// <summary>
        /// Добавление записи изображения
        /// </summary>
        /// <param name="location">Папка</param>
        /// <param name="description">Описание</param>
        /// <returns>Данные о файле</returns>
        public async Task<Images> Add(string location, string name, string description, string extension)
        {
            Images images;
            try
            {
                images = GetImageModel(location, name, description);
                images = await baseImages.Add(images);
            }
            catch
            {
                return null;
            }
            return images;
        }
        
        /// <summary>
        /// Заполнить модель информации о файле
        /// </summary>
        /// <param name="location">Расположение</param>
        /// <param name="name">Название</param>
        /// <param name="description">Описание</param>
        /// <returns>Информация</returns>
        private static Images GetImageModel(string location, string name, string description)
        {
            Images image = new Images();
            var path = Path.Combine("Files", location, name);
            var extension = Path.GetExtension(path);

            image.Description = description;
            image.Name = name;
            image.Extension = extension;
            image.Location = location;
            image.HashCode = HaxCodeFile.GetSHA1(path);

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                
                image.Size = stream.Length;
            }
            return image;
        }
        /// <summary>
        /// Получаем данные по названию папки
        /// </summary>
        /// <param name="location">Папка хранения  </param>
        /// <returns>Модлеь данных</returns>
        public async Task<IEnumerable<Images>> GetItemByLocation(string location)
        {
            return await baseImages.GetItemByLocation(location);
        }
    }
}

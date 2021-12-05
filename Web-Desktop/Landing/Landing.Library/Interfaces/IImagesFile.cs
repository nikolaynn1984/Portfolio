using Landing.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Library.Interfaces
{
    public interface IImagesFile
    {
        /// <summary>
        /// Получить список файлов
        /// </summary>
        /// <returns>Список изображений</returns>
        public Task<IEnumerable<Images>> GetItems();
        /// <summary>
        /// Данные о файле по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель - Данные о файле</returns>
        public Task<Images> GetItemById(int id);
        /// <summary>
        /// Получаем список изображение по расположению
        /// </summary>
        /// <param name="location">Расположение</param>
        /// <returns>Список</returns>
        Task<IEnumerable<Images>> GetItemByLocation(string location);
        /// <summary>
        /// Добавление информации о файле
        /// </summary>
        /// <param name="location">Расположение в папке</param>
        /// <param name="name">Название файла</param>
        /// <param name="description">Описание</param>
        /// <param name="extension">Расширение</param>
        /// <returns>Модель информации с идентификатором</returns>
        Task<Images> Add(string location, string name, string description, string extension);
        /// <summary>
        /// Удалить файл
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true / null- Если запись удалена, иначе false / сообщение об ошибке</returns>
        public Task<(bool Result, string Error)> Delete(int id);
    }
}

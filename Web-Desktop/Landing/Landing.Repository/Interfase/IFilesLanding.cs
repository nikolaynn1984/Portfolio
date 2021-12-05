using Landing.Library.Model;
using System.Threading.Tasks;

namespace Landing.Repository.Interfase
{
    /// <summary>
    /// Работат с файлами/изображениями
    /// </summary>
    interface IFilesLanding
    {
        /// <summary>
        /// Загрузка файла на сервер
        /// </summary>
        /// <param name="bytes">Массив файла</param>
        /// <param name="images">Информация о файле</param>
        /// <returns>Информация о файле</returns>
        Task<Images> Upload(byte[] bytes, Images images);
        /// <summary>
        /// Загрузка файла с сервера
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Путь к файлу на локальном устройстве</returns>
        Task<string> Download(Images images);

    }
}

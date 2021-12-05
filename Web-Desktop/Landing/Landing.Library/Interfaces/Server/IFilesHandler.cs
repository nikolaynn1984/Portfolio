using Landing.Library.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Landing.Library.Interfaces.Server
{
    public interface IFilesHandler
    {
        /// <summary>
        /// Загрузка файлу по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Фаил</returns>
        Task<FileStreamResult> DowloadFile(int id);
        /// <summary>
        /// Загрузка и созранение файл 
        /// </summary>
        /// <param name="file">Фаил</param>
        /// <returns>Модель данных с информацией о файле</returns>
        Task<Images> UploadFile(IFormFile file, string location, string description);
    }
}

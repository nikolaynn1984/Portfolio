using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Landing.Library.Model;
using Landing.Server.Data;
using Landing.Server.Auth;
using Landing.Library.Interfaces.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Landing.Library.Interfaces;

namespace Landing.Server.Controllers
{
    
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        
        private readonly IImagesFile file;
        private readonly IFilesHandler handler;

        public ImagesController(IImagesFile File)
        {
            file = File;
            handler = new FileHandler(file);
        }

        /// <summary>
        /// Список изображений
        /// </summary>
        /// <returns>Список</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Images>>> GetImages()
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            var item = await file.GetItems();
            if(item == null)
            {
                ModelState.AddModelError("Loader", "Список пуст");
                return BadRequest(ModelState);
            }
            return item.ToList();
        }

        /// <summary>
        /// Получаем информацию о изображении по идентификатору
        /// </summary>
        /// <param name="id">Идентфикатор</param>
        /// <returns>Модель Images с данныим</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Images>> GetImages(int id)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            var images = await file.GetItemById(id);

            if (images == null)
            {
                ModelState.AddModelError("Loader", "Запись не найдена");
                return NotFound(ModelState);
            }

            return images;
        }


        /// <summary>
        /// Удаление изображения по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Ок если удаление прошло успешно</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImages(int id)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            var (Result, Error) = await file.Delete(id);
            if (!Result)
            {
                ModelState.AddModelError("Delete", Error);
                return BadRequest(ModelState);
            }

            return Ok();
        }
        /// <summary>
        /// Скачивание изображения по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>FileStream файла</returns>
        [HttpGet]
        [Route("/api/Download/{id}")]
        public async Task<IActionResult> DowloadFile(int id)
        {
            if (id < 1)
            {
                ModelState.AddModelError("File", "Не верный запрос");
                return BadRequest(ModelState);
            }

            var file = await handler.DowloadFile(id);

            if (file == null)
            {
                ModelState.AddModelError("File", "Фаил не найден");
                return NotFound(ModelState);
            }

            return file;
        }
        /// <summary>
        /// Загружаем изображение в папку сервера
        /// </summary>
        /// <param name="file">Файл</param>
        /// <param name="location">Расположение - папка</param>
        /// <param name="description">Описание</param>
        /// <returns>Модель Images с данными о изображении</returns>
        [HttpPost]
        [Route("/api/Upload/{location}/{description}")]
        public async Task<ActionResult<Images>> UploadFile(IFormFile file, string location, string description)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            var img = new Images();
            try
            {
                img = await handler.UploadFile(file, location, description);
                if (file == null)
                {
                    ModelState.AddModelError("File", "Фаил не найден");
                    return NotFound(ModelState);
                }
            }
            catch
            {
                ModelState.AddModelError("Catch", "Что то поршло не так");
                return BadRequest(ModelState);
            }
            

            return img;
        }
    }
}

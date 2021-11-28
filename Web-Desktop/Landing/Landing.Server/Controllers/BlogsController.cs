using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Landing.Model.Data;
using Landing.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Landing.Server.Auth;

namespace Landing.Server.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlog blog;

        public BlogsController(IBlog Blog)
        {
            blog = Blog;
        }

        /// <summary>
        /// Получаем список блогов
        /// </summary>
        /// <returns>Список</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            var item = await blog.GetItems();
            if(item == null)
            {
                ModelState.AddModelError("Loader", "Список пуст");
                return BadRequest(ModelState);
            }

            return item.ToList();
        }

        /// <summary>
        /// Получаем блог по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель Blog с даными</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var item = await blog.GetItemById(id);

            if (item == null)
            {
                ModelState.AddModelError("Loader", "Запись не найдена");
                return NotFound(ModelState);
            }

            return item;
        }

        /// <summary>
        /// Редактирование блога
        /// </summary>
        /// <param name="item">Модель даных</param>
        /// <returns>Модель Blog с обновленными данными</returns>
        [HttpPut]
        public async Task<ActionResult<Blog>> Update([FromBody] Blog item)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
               item =   await blog.Edit(item);


                if (item == null)
                {
                    ModelState.AddModelError("Edit", "Ошибка добавления");
                    return BadRequest(ModelState);
                }
            }
            catch 
            {
                ModelState.AddModelError("Catch", "Что то пошло не так");
                return BadRequest(ModelState);
            }

            return item;
        }

        /// <summary>
        /// Добавление записи блога
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>Модель Blog с данными</returns>
        [HttpPost]
        public async Task<ActionResult<Blog>> Create([FromBody] Blog model)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                model = await blog.Add(model);
                if(model == null)
                {
                    ModelState.AddModelError("Add", "Ошибка добвления");
                    return BadRequest(ModelState);
                }
            }
            catch
            {
                ModelState.AddModelError("Catch", "Что то пошло не так");
                return BadRequest();
            }
            return model;
        }

        /// <summary>
        /// Удаление записи блог
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Ок если удаление прошло успешно</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            var (Result, Error) = await blog.Delete(id);
            if (!Result)
            {
                ModelState.AddModelError("Delete", Error);
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}

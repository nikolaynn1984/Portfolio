using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Landing.Library.Model;
using Landing.Server.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Landing.Library.Interfaces;

namespace Landing.Server.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class SocialsController : ControllerBase
    {
        private readonly ISocial social;

        public SocialsController(ISocial Social)
        {
            social = Social;
        }

        /// <summary>
        /// Получаем список соц сетей
        /// </summary>
        /// <returns>Список</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Social>>> GetSocials()
        {
            var item = await social.GetItems();
            if(item == null)
            {
                ModelState.AddModelError("Loader", "Список пуст");
                return BadRequest(ModelState);
            }
            return item.ToList();
        }

        /// <summary>
        /// Получаем информацию о соцюсети
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель Social с данными</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Social>> GetSocial(int id)
        {
            var item = await social.GetItemById(id);

            if (item == null)
            {
                ModelState.AddModelError("Loader", "Запись не найдена");
                return NotFound(ModelState);
            }

            return item;
        }

        /// <summary>
        /// Редактируем запись соц сети
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>Модель Social если редактирвание прошло успешно</returns>
        [HttpPut]
        public async Task<ActionResult<Social>> Update([FromBody] Social model)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();

            if (!ModelState.IsValid) return BadRequest(ModelState);


            try
            {
                model = await social.Edit(model);

                if (model == null)
                {
                    ModelState.AddModelError("Edit", "Не успешное обновление данных");
                    return BadRequest(ModelState);
                }
            }
            catch
            {
                ModelState.AddModelError("Catch", "Что то пошло не так");
                return BadRequest(ModelState);
            }

            return model;
        }

        /// <summary>
        /// Добавляем поле соцюсети
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>Модель Social с списвоеным идентификатором</returns>
        [HttpPost]
        public async Task<ActionResult<Social>> Create([FromBody] Social model)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                model = await social.Add(model);
                if(model == null)
                {
                    ModelState.AddModelError("Add", "Не успешное добавление данных");
                    return BadRequest(ModelState);
                }
            }
            catch
            {
                ModelState.AddModelError("Catch", "Что то пошло не так");
                return BadRequest(ModelState);
            }
            return model;
        }

        /// <summary>
        /// Удаление записи соцюсети
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Ок если удаление прошло успешно</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();

            var (Result, Error) = await social.Delete(id);
            if (!Result)
            {
                ModelState.AddModelError("Delete", Error);
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Landing.Library.Model;
using Landing.Server.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Landing.Library.Interfaces;

namespace Landing.Server.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IServices services;

        public ServicesController(IServices Services)
        {
            services = Services;
        }

        /// <summary>
        /// Получаем список сервисо
        /// </summary>
        /// <returns>Список</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Services>>> GetServices()
        {
            var item = await services.GetItems();
            if(item == null)
            {
                ModelState.AddModelError("Loader", "Список пуст");
                return BadRequest(ModelState);
            }
            return item.ToList();
        }

        /// <summary>
        /// Получаем сервис по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель Services</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Services>> GetServices(int id)
        {
            var item = await services.GetItemById(id);

            if (item == null)
            {
                ModelState.AddModelError("Loader", "Запись не найдена");
                return NotFound(ModelState);
            }

            return item;
        }

        /// <summary>
        /// Редактируем поле сервис
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>Модель Services с обновленными данными</returns>
        [HttpPut]
        public async Task<ActionResult<Services>> Update([FromBody] Services model)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);


            try
            {
                model =  await services.Edit(model);

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
        /// Добавление нового поля сервис
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>Модель Services с присвоеным идентификатором</returns>
        [HttpPost]
        public async Task<ActionResult<Services>> Create([FromBody] Services model)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                model = await services.Add(model);
                if(model == null)
                {
                    ModelState.AddModelError("Add", "Не успешное добавление данных");
                    return BadRequest(ModelState);
                }
            }
            catch
            {
                return BadRequest();
            }
            return model;
        }

        /// <summary>
        /// Удаление записи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Ок если удаление прошло успешно</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            var (Result, Error) = await services.Delete(id);
            if (!Result)
            {
                ModelState.AddModelError("Delete", Error);
                return BadRequest(ModelState);
            }

            return Ok();
        }

    }
}

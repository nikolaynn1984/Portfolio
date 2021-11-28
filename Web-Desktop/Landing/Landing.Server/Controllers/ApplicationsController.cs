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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplication application;

        public ApplicationsController(IApplication Application)
        {
            application = Application;
        }

        /// <summary>
        /// Получаем список заявок
        /// </summary>
        /// <returns>Список заявок если успешно, в противном случае список ошибок</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requisition>>> GetApplications()
        {
            bool result = User.IsInRole(LRols.Admin.ToString());
            if (!result)
            {
                return Unauthorized();
            }

            IEnumerable<Requisition> item = await application.GetItems();
            if(item == null)
            {
                ModelState.AddModelError("Get", "Список пуст");
                return BadRequest(ModelState);
            }

            return item.ToList();
        }

        /// <summary>
        /// Получаем заявку по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель Requisition с данными если успешно, в противном случае ошибки</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Requisition>> GetApplication(int id)
        {
            bool result = User.IsInRole(LRols.Admin.ToString());
            if (!result)
            {
                return Unauthorized();
            }

            Requisition item = await application.GetItemById(id);

            if (item == null)
            {
                ModelState.AddModelError("NotFind", "Запись не найдена");
                return NotFound(ModelState);
            }

            return item;
        }

        /// <summary>
        /// Редактирование заявки
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>модель Requisition с обновленными даными</returns>
        [HttpPut]
        public async Task<ActionResult<Requisition>> Update([FromBody] Requisition model)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = model;
            try
            {
                item = await application.Edit(model);

                if (item == null)
                {
                    ModelState.AddModelError("Edit", "Запись не найдена");
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
        /// Добавление новой заявки
        /// </summary>
        /// <param name="model">Модель данныхз</param>
        /// <returns>модель Requisition с присвоеным идентификатором, в противном случае ошибкт</returns>
        [HttpPost]
        public async Task<ActionResult<Requisition>> Create([FromBody] Requisition model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var item = await application.Add(model);
                if (item == null)
                {
                    ModelState.AddModelError("Add", "Запись не одновлена");
                    return BadRequest(ModelState);
                }
                return item;
            }
            catch
            {
                ModelState.AddModelError("Catch", "Что то пошло не так");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Удаление заявки
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Ок если успешно, в противном случае ошибки</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            var (Result, Error) = await application.Delete(id);
            if (!Result)
            {
                ModelState.AddModelError("Delete", Error);
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}

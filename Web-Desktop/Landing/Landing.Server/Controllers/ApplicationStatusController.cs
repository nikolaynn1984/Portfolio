using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Landing.Library.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Landing.Server.Auth;
using Landing.Library.Interfaces;

namespace Landing.Server.Controllers
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ApplicationStatusController : ControllerBase
    {
        private readonly IApplicationStatus status;

        public ApplicationStatusController(IApplicationStatus Status)
        {
            status = Status;
        }

        
        /// <summary>
        /// Получаем список статусов
        /// </summary>
        /// <returns>Список статусов с случае успеха, в противном случае ошибки</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequisitionStatus>>> GetStatuses()
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            var item = await status.GetItems();
            if(item == null)
            {
                ModelState.AddModelError("Loader", "Список пуст");
                return BadRequest(ModelState);
            }
            return item.ToList();
        }

        /// <summary>
        /// Получаем статус по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель RequisitionStatus в случае успеха, в противном случае ошибка</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RequisitionStatus>> GetStatusId(int id)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            var item = await status.GetItemById(id);
            if (item == null)
            {
                ModelState.AddModelError("NotFind", "Запись не найдена");
                return NotFound(ModelState);
            }

            return item;
        }

        /// <summary>
        /// Редактируем статус
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>Модель RequisitionStatus с данными </returns>
        [HttpPut]
        public async Task<ActionResult<RequisitionStatus>> Update([FromBody] RequisitionStatus model)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = model;
            try
            {
               item =  await status.Edit(model);
                if (item == null)
                {
                    ModelState.AddModelError("Edit", "Запись не одновлена");
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
        /// Добавление статуса
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>Модель RequisitionStatus с присвоеным идентификаторм</returns>
        [HttpPost]
        public async Task<ActionResult<RequisitionStatus>> Create([FromBody] RequisitionStatus model)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();

            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var item = await status.Add(model);
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
        /// Удаление статуса по идетификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Ок если удаление прошло успешно</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();

            var (Result, Error) = await status.Delete(id);
            if (!Result)
            {
                ModelState.AddModelError("Delete", Error);
                return BadRequest(ModelState);
            }

            return Ok();
            
        }
    }
}

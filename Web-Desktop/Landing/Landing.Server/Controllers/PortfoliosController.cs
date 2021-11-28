using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Landing.Interface;
using Landing.Model.Data;
using Landing.Server.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Landing.Server.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolio portfolio;

        public PortfoliosController(IPortfolio Portfolio)
        {
            portfolio = Portfolio;
        }

        /// <summary>
        /// Получаем список портфолио
        /// </summary>
        /// <returns>Список</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetPortfolios()
        {
            var item = await portfolio.GetItems();
            if(item == null)
            {
                ModelState.AddModelError("Loader", "Список пуст");
                return BadRequest(ModelState);
            }
            return item.ToList();
        }

        /// <summary>
        /// Получаем информацию о портфолио по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель Portfolio</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Portfolio>> GetPortfolio(int id)
        {
            var item = await portfolio.GetItemById(id);

            if (item == null)
            {
                ModelState.AddModelError("Loader", "Запись не найдена");
                return NotFound(ModelState);
            }

            return item;
        }

        /// <summary>
        /// Обновляем информацию о портфолио
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>Модель Portfolio с присвоеных идентификатором</returns>
        [HttpPut]
        public async Task<ActionResult<Portfolio>> Update([FromBody] Portfolio model)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var item = model;
            try
            {
                item = await portfolio.Edit(model);

                if (item == null)
                {
                    ModelState.AddModelError("Edit", "Не успешное обновление");
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
        /// Добавляем запись портфолио
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>Модель Portfolio с обновленными данными</returns>
        [HttpPost]
        public async Task<ActionResult<Portfolio>> Create([FromBody] Portfolio model)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var item = await portfolio.Add(model);

                if (item == null)
                {
                    ModelState.AddModelError("Add", "Не успешное добавление данных");
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
        /// Удаляем запись по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Ок если удаление прошло успешно</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            var (Result, Error) = await portfolio.Delete(id);
            if (!Result)
            {
                ModelState.AddModelError("Delete", Error);
                return BadRequest(ModelState);
            }
            else
            {
                return Ok();
            }
        }
    }
}

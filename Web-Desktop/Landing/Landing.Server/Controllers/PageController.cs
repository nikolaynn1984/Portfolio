using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Landing.Library.Model;
using System.Threading.Tasks;
using Landing.Server.Auth;
using Landing.Library.Interfaces;

namespace Landing.Server.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PageController : ControllerBase
    {
        private readonly IPageView view;
        public PageController(IPageView page)
        {
            this.view = page;
        }
        /// <summary>
        /// Получаем информацию параметров
        /// </summary>
        /// <returns>Модель PageView</returns>
        [HttpGet]
        public async Task<ActionResult<PageView>> Get()
        {
            var item = await view.Get();
            if(item == null)
            {
                ModelState.AddModelError("Page", "Параметры отсутствуют");
                return BadRequest(ModelState);
            }
            return await view.Get();
        }


        /// <summary>
        /// Редактируем параметры страницы
        /// </summary>
        /// <param name="value">Модель данных</param>
        /// <returns>Ок Если изменения прошло успешно</returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PageView value)
        {
            var resultAuth = User.IsInRole(LRols.Admin.ToString());
            if (!resultAuth) return Unauthorized();
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                bool result = await view.Edit(value);
                if(!result)
                {
                    ModelState.AddModelError("Edit", "Обновление прошло не успешно");
                    return BadRequest(ModelState);
                }
                return Ok();
            }
            catch
            {
                ModelState.AddModelError("Catch", "Что то пошло не так");
                return BadRequest(ModelState);
            }
        }

    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Landing.Interface;
using Landing.Model.Data;
using System.Linq;
using Landing.Server.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Landing.Server.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class MenusController : ControllerBase
    {
        private readonly IMainMenu mainMenu;

        public MenusController(IMainMenu menu)
        {
            mainMenu = menu;
        }

        /// <summary>
        /// Получаем список меню
        /// </summary>
        /// <returns>Список</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenu()
        {
            var item = await mainMenu.GetMenus();
            if(item == null)
            {
                ModelState.AddModelError("Loader", "Список пуст");
                return BadRequest(ModelState);
            }
            return item.ToList();
        }

        /// <summary>
        /// Обновление меню
        /// </summary>
        /// <param name="menu">Модель данным</param>
        /// <returns>Ок если измененеи прошло успешно</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Menu menu)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool resultEdit = await mainMenu.Edit(menu);
            if (!resultEdit)
            {
                ModelState.AddModelError("Edit", "Что то пошло не так");
                return BadRequest(ModelState);
            }
            return Ok();
        }

       
    }
}

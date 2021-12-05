using Landing.Library.Interfaces.Server;
using Landing.Library.ViewModel;
using Landing.Server.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Landing.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountHandler account;
        

        public AccountController(IAccountHandler account)
        {
            this.account = account;
        }
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Модель UserResult с данными если успешно, в противном случае ошибки</returns>
        [HttpPost]
        [Route("/api/Login")]
        public async Task<ActionResult<UserResult>> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
 
             var item = await account.Login(model);
            if (item == null)
            {
                ModelState.AddModelError("Login", "Не верный логин и/или пароль");
                return Unauthorized(ModelState);
            }

            var user = HttpContext.User.Identity.IsAuthenticated;
            return item;
        }
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="model">Модель данных для регистрации</param>
        /// <returns>Модель UserResult с данными если успешно, в противном случае ошибки</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("/api/Register")]
        public async Task<ActionResult<UserResult>> Register([FromBody] RegisterViewModel model)
        {
            var result = User.IsInRole(LRols.Admin.ToString());
            if (!result) return Unauthorized();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var item = await account.Register(model);
            if (item == null)
            {
                ModelState.AddModelError("Register", "Не удачная регистрация");
                return BadRequest(ModelState);
            }
                
      
            return item;
        }

    }
}

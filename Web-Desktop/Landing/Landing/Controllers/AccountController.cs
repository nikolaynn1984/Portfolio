using Landing.Library.ViewModel;
using Landing.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Landing.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    public class AccountController : Controller
    {
        private readonly RAccount account;

        public AccountController()
        {
            this.account = new RAccount(Connection.Url);
        }
        /// <summary>
        /// Авторизируем как гость
        /// </summary>
        /// <returns>Перекидываем в главную страницу сайта</returns>
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            LoginViewModel user = new LoginViewModel
            {
                Email = Connection.UserName,
                Password = Connection.Password
            };
            
            var userResult = await account.Login(user);
            if(userResult != null)
            {
                await LogUser(userResult);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        /// <summary>
        /// Сохраняем информацию о пользователе в куках
        /// </summary>
        /// <param name="user">Польователь</param>
        private async Task LogUser(UserResult user)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Actor, user.Token),
            };
            Connection.Token = user.Token;
            HttpContext.Response.Cookies.Append("token", user.Token);
            ClaimsIdentity id = new ClaimsIdentity(claim, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
           
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}

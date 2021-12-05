using Landing.Library.Model;
using Landing.Models;
using Landing.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Landing.Controllers
{
    /// <summary>
    /// Контроллер странц сайта
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {

        private LandingViewModel landing;

        public HomeController()
        {
           this.landing = new LandingViewModel(HttpContext);
        }
        /// <summary>
        /// Главное окно
        /// </summary>
        /// <returns>Представление с данными для заполнение</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            PageView page = await landing.GetPageView(HttpContext);

            return View(page);
        }
        /// <summary>
        /// Окно проектов
        /// </summary>
        /// <returns>Представление и список проектов</returns>
        [HttpGet]
        public async Task<IActionResult> Projects()
        {
            var list = await landing.GetPortfolios();
            if(list == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(list);
        }
        /// <summary>
        /// Окно детализации проекта
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Представление с информацией о проекте</returns>
        [HttpGet]
        public async Task<IActionResult> ProgectDetail(int id)
        {
            Portfolio item = await landing.GetPortfolioById(id);
            if(item == null) RedirectToAction("Index", "Projects");
            return View(item);
        }
        /// <summary>
        /// Окно сервисы
        /// </summary>
        /// <returns>Представление со список сервисов</returns>
        [HttpGet]
        public async Task<IActionResult> Services()
        {
            var list = await landing.GetServices();
            if (list == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(list);
        }
        /// <summary>
        /// Окно блог
        /// </summary>
        /// <returns>Представление со списокм блога</returns>
        [HttpGet]
        public async Task<IActionResult> Blog()
        {
            var list = await landing.GetBlog();
            if (list == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(list);
        }
        /// <summary>
        /// Окно детализации блога
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Представление с информацией о блоге</returns>
        [HttpGet]
        public async Task<IActionResult> BlogDetail(int id)
        {
            Blog item = await landing.GetBlogById(id);
            if(item == null) return RedirectToAction("Index", "Blog");
            return View(item);
        }
        /// <summary>
        /// Окно контакты
        /// </summary>
        /// <returns>Преставление с информацией</returns>
        [HttpGet]
        public async Task<IActionResult> Contacts()
        {
            var data = await landing.GetPageView(HttpContext);
            return View(data);
        }
        /// <summary>
        /// Получаем информацию настроек страницы (Запрос с mapscrits.js.)
        /// Для отображение цвета маркера и координат на карте
        /// </summary>
        /// <returns>Информация в формате JSON </returns>
        [HttpGet]
        public async Task<IActionResult> ContactsMap()
        {
            var data = await landing.GetPageView(HttpContext);
            return Json(data);
        }
        /// <summary>
        /// Отправка запроса 
        /// </summary>
        /// <param name="model">Модель с заполнеными данными </param>
        /// <returns>Модель в JSON формате</returns>
        [HttpPost]
        public async Task<IActionResult> Requestion([FromBody] Requisition model)
        {
                await landing.NewRequestion(model);
            return Json(model);
        }
        /// <summary>
        /// Получаем список настроки соцсетей для отображения в Футере страницы
        /// </summary>
        /// <returns>Список в JSon формате</returns>
        [HttpGet]
        public async Task<IActionResult> Socials()
        {
            var data = await landing.GetSocials();
            return Json(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(IFormFile file)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

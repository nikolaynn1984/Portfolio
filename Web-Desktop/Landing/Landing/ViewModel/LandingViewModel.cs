using Landing.Library.Model;
using Landing.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.ViewModel
{
    /// <summary>
    /// Модель представления страниц
    /// </summary>
    public class LandingViewModel
    {
        
        private readonly RPageView pageView;
        private readonly RPortfolio portfolio;
        private readonly RServices services;
        private readonly RSocial social;
        private readonly RBlog blog;
        private readonly RRequisition requisition;
        public LandingViewModel(HttpContext context)
        {
           this.pageView = new RPageView(Connection.Url, Connection.Token);
            this.social = new RSocial(Connection.Url, Connection.Token);
            this.portfolio = new RPortfolio(Connection.Url, Connection.Token);
            this.services = new RServices(Connection.Url, Connection.Token);
            this.blog = new RBlog(Connection.Url, Connection.Token);
            this.requisition = new RRequisition(Connection.Url, Connection.Token);
        }


        public async Task<PageView> GetPageView(HttpContext context)
        {
            var result = await pageView.Get();
            return result;
        }
        /// <summary>
        /// Получаем список ссылок в соцсети
        /// </summary>
        /// <returns>Список Social</returns>
        public async Task<List<Social>> GetSocials()
        {
            List<Social> result = (List<Social>)await social.GetItems();
            return result;
        }
        /// <summary>
        /// Список проектов
        /// </summary>
        /// <returns>Список</returns>
        public async Task<IEnumerable<Portfolio>> GetPortfolios()
        {
            return await portfolio.GetItems();
        }
        /// <summary>
        /// Получаем портфолио по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Объект Portfolio</returns>
        public async Task<Portfolio> GetPortfolioById(int id)
        {
            return await portfolio.GetItemById(id);
        }
        /// <summary>
        /// Получаем список услуг
        /// </summary>
        /// <returns>Список Services</returns>
        public async Task<IEnumerable<Services>> GetServices()
        {
            return await services.GetItems();
        }
        /// <summary>
        /// Получаем список блога
        /// </summary>
        /// <returns>Список Blog</returns>
        public async Task<IEnumerable<Blog>> GetBlog()
        {
            return await blog.GetItems();
        }
        /// <summary>
        /// Получаем блог по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель Blog</returns>
        public async Task<Blog> GetBlogById(int id)
        {
            return await blog.GetItemById(id);
        }
        /// <summary>
        /// Добавляем запрос пользователя со страницы
        /// </summary>
        /// <param name="model">Данные</param>
        /// <returns>Данные если добавление прошло успешно</returns>
        public async Task<Requisition> NewRequestion(Requisition model)
        {
            return await requisition.Add(model);
        }

    }
}

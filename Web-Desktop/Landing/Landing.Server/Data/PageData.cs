using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Landing.Library.Model;
using Landing.Library.Interfaces;
using Landing.Server.Database.Query;

namespace Landing.Server.Data
{
    internal class PageData : IPageView
    {
        private readonly BasePage page;
        private readonly BaseImages baseImages;
        public static event Action<string> TelegramTokenChanged;
        public PageData()
        {
            this.page = new BasePage(Connection.GetConnectionString());
            this.baseImages = new BaseImages(Connection.GetConnectionString());
        }
        /// <summary>
        /// Редактировать параметры страницы
        /// </summary>
        /// <param name="model">Данные</param>
        /// <returns>true - если редактирование прошло успешно, в противном случае false</returns>
        public async Task<bool> Edit(PageView model)
        {
            bool result = true;
            try
            {
                
                var item = await page.Edit(model);
                TelegramTokenChanged?.Invoke(model.TelegramToken);
            }
            catch
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// Получить Параметры страницы
        /// </summary>
        /// <returns>Параметры</returns>
        public async Task<PageView> Get()
        {
            try
            {
                var item = await page.Get(1);
                item.GetImages = await baseImages.GetItemById(item.HeaderImageId);
                return item;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }

        }
    }
}

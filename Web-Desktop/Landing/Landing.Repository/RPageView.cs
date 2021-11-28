using Landing.Interface;
using Landing.Model.Data;
using Landing.Repository.Loader;
using System.Threading.Tasks;

namespace Landing.Repository
{
    public class RPageView : IPageView
    {
        private readonly PageData page;
        private readonly RImages images;
        public RPageView(string url, string token)
        {
            page = new PageData(url, token);
            images = new RImages(url, token);
        }
        /// <summary>
        /// Получить информацию главной страницы
        /// </summary>
        /// <returns>Информация</returns>
        public async Task<PageView> Get()
        {
            PageView item = await page.Get();
            if (item.HeaderImageId != 0)
            {
                _ = await images.Download(item.GetImages);
            }

            return await page.Get();
        }
        /// <summary>
        /// Редактировать данные
        /// </summary>
        /// <param name="model">Информация страницы</param>
        /// <returns>true - если редактирование прошло успешно, в противном случае false</returns>
        public async Task<bool> Edit(PageView model)
        {
            return await page.Edit(model);
        }


    }
}

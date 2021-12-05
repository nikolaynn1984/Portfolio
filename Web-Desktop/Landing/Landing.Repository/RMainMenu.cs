using Landing.Library.Model;
using Landing.Library.Interfaces;
using Landing.Repository.Interfase;
using Landing.Repository.Loader;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Repository
{
    public class RMainMenu : IMainMenu
    {
        private readonly IDataHandler<Menu> data;
        private static Menu menu;
        public RMainMenu(string url, string token)
        {
            menu = new Menu();
            this.data = new DataHandler<Menu>(menu, url, token);
        }
        /// <summary>
        /// Получить список навигации
        /// </summary>
        /// <returns>список навигации</returns>
        public async Task<IEnumerable<Menu>> GetMenus()
        {
            return await data.GetItems();
        }
        /// <summary>
        /// Редактировать название меню
        /// </summary>
        /// <param name="model">Модель Меню</param>
        /// <returns>true - если прошло успешно, в противном случа false</returns>
        public async Task<bool> Edit(Menu model)
        {
            var item = await data.Edit(model);
            if (item == null) return false;
            return true;
        }
    }
}

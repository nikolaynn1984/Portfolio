using System.Collections.Generic;
using System.Threading.Tasks;
using Landing.Library.Model;
using Landing.Library.Interfaces;
using Landing.Server.Database.Query;

namespace Landing.Server.Data
{
    public class MenuData : IMainMenu
    {

        private readonly BaseMenu baseMenu;
        public MenuData()
        {
            this.baseMenu = new BaseMenu(Connection.GetConnectionString());
        }
        /// <summary>
        /// Получить все записи меню
        /// </summary>
        /// <returns>Список</returns>
        public async Task<IEnumerable<Menu>> GetMenus()
        {
            var item = await baseMenu.GetItems();
            return item;
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект Menu с обновлеными даными</param>
        /// <returns>Menu если редактирование прошло успешно, в противном случа null</returns>
        public async Task<bool> Edit(Menu model)
        {
            try
            {
                return await baseMenu.Edit(model);
            }
            catch
            {
                return false;
            }
        }


    }
}

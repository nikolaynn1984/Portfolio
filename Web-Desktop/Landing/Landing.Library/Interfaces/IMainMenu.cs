using Landing.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Library.Interfaces
{
    public interface IMainMenu
    {
        /// <summary>
        /// Получить список навигации
        /// </summary>
        /// <returns>список навигации</returns>
        Task<IEnumerable<Menu>> GetMenus();
        /// <summary>
        /// Редактировать название меню
        /// </summary>
        /// <param name="model">Модель Меню</param>
        /// <returns>true - если прошло успешно, в противном случа false</returns>
        Task<bool> Edit(Menu model);
    }
}

using Landing.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landing.Interface
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

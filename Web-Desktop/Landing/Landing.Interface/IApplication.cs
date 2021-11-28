using Landing.Model.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Interface
{
    public interface IApplication
    {
        /// <summary>
        /// Получить список заявок
        /// </summary>
        /// <returns>Список заявок</returns>
        public Task<IEnumerable<Requisition>> GetItems();
        /// <summary>
        /// Заявка по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель - заявка</returns>
        public Task<Requisition> GetItemById(int id);
        /// <summary>
        /// Добавить заявку
        /// </summary>
        /// <param name="application">Модель с данными</param>
        /// <returns>Модель с присвоенным идентификатором</returns>
        public Task<Requisition> Add(Requisition application);
        /// <summary>
        /// Редактирование заявку
        /// </summary>
        /// <param name="application">Модель - новые данные</param>
        /// <returns>Модель с обновленными данными</returns>
        public Task<Requisition> Edit(Requisition application);
        /// <summary>
        /// Удалить заявку
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true / null- Если заявка удалена, иначе false / сообщение об ошибке</returns>
        public Task<(bool Result, string Error)> Delete(int id);
    }
}

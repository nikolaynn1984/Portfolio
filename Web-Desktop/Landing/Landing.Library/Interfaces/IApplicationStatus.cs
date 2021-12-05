using Landing.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Library.Interfaces
{
    public interface IApplicationStatus
    {
        /// <summary>
        /// Получить список статусов
        /// </summary>
        /// <returns>Список стутусов</returns>
        public Task<IEnumerable<RequisitionStatus>> GetItems();
        /// <summary>
        /// Статус по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель - статус</returns>
        public Task<RequisitionStatus> GetItemById(int id);
        /// <summary>
        /// Добавить статус
        /// </summary>
        /// <param name="status">Модель с данными</param>
        /// <returns>Модель с присвоенным идентификатором</returns>
        public Task<RequisitionStatus> Add(RequisitionStatus status);
        /// <summary>
        /// Редактирование статуса
        /// </summary>
        /// <param name="status">Модель - новые данные</param>
        /// <returns>Модель с обновленными данными</returns>
        public Task<RequisitionStatus> Edit(RequisitionStatus status);
        /// <summary>
        /// Удалить статус
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true / null- Если статус удален, иначе false / сообщение об ошибке</returns>
        public Task<(bool Result, string Error)> Delete(int id);
    }
}

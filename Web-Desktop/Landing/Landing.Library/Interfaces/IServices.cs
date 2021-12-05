using Landing.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Library.Interfaces
{
    public interface IServices
    {
        /// <summary>
        /// Получить список сервисов
        /// </summary>
        /// <returns>Список сервисов</returns>
        public Task<IEnumerable<Services>> GetItems();
        /// <summary>
        /// Сервис по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель - Сервис</returns>
        public Task<Services> GetItemById(int id);
        /// <summary>
        /// Добавить сервис
        /// </summary>
        /// <param name="services">Модель с данными</param>
        /// <returns>Модель с присвоенным идентификатором</returns>
        public Task<Services> Add(Services services);
        /// <summary>
        /// Редактирование сервис
        /// </summary>
        /// <param name="services">Модель - новые данные</param>
        /// <returns>Модель с обновленными данными</returns>
        public Task<Services> Edit(Services services);
        /// <summary>
        /// Удалить сервис
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true / null- Если сервис удалена, иначе false / сообщение об ошибке</returns>
        public Task<(bool Result, string Error)> Delete(int id);
    }
}

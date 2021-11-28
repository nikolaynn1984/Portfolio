using System.Collections.Generic;
using System.Threading.Tasks;
using Landing.Model.Data;

namespace Landing.Interface
{
    public interface IPortfolio
    {
        /// <summary>
        /// Получить список портфолио
        /// </summary>
        /// <returns>Список сервисов</returns>
        public Task<IEnumerable<Portfolio>> GetItems();
        /// <summary>
        /// Портфолио по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель - Портфолио</returns>
        public Task<Portfolio> GetItemById(int id);
        /// <summary>
        /// Добавить портфолио
        /// </summary>
        /// <param name="portfolio">Модель с данными</param>
        /// <returns>Модель с присвоенным идентификатором</returns>
        public Task<Portfolio> Add(Portfolio portfolio);
        /// <summary>
        /// Редактирование портфолио
        /// </summary>
        /// <param name="portfolio">Модель - новые данные</param>
        /// <returns>Модель с обновленными данными</returns>
        public Task<Portfolio> Edit(Portfolio portfolio);
        /// <summary>
        /// Удалить портфолио
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true / null- Если запись удалена, иначе false / сообщение об ошибке</returns>
        public Task<(bool Result, string Error)> Delete(int id);
    }
}

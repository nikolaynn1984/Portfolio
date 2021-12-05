using Landing.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Library.Interfaces
{
    public interface ISocial
    {
        /// <summary>
        /// Получить список соц-сетей
        /// </summary>
        /// <returns>Список соц-сетей</returns>
        public Task<IEnumerable<Social>> GetItems();
        /// <summary>
        /// Соц-сеть по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель - Соц-Сеть</returns>
        public Task<Social> GetItemById(int id);
        /// <summary>
        /// Добавить соц-сеть
        /// </summary>
        /// <param name="social">Модель с данными</param>
        /// <returns>Модель с присвоенным идентификатором</returns>
        public Task<Social> Add(Social social);
        /// <summary>
        /// Редактирование соц-сеть
        /// </summary>
        /// <param name="social">Модель - новые данные</param>
        /// <returns>Модель с обновленными данными</returns>
        public Task<Social> Edit(Social social);
        /// <summary>
        /// Удалить соц-сеть
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true / null- Если соц-сеть удалена, иначе false / сообщение об ошибке</returns>
        public Task<(bool Result, string Error)> Delete(int id);
    }
}

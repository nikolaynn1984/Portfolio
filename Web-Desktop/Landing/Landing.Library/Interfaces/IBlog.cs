using Landing.Library.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Library.Interfaces
{
    public interface IBlog
    {
        /// <summary>
        /// Получить список
        /// </summary>
        /// <returns>Список</returns>
        public Task<IEnumerable<Blog>> GetItems();
        /// <summary>
        /// Блог по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель - Блог</returns>
        public Task<Blog> GetItemById(int id);
        /// <summary>
        /// Добавить Блог
        /// </summary>
        /// <param name="blog">Модель с данными</param>
        /// <returns>Модель с присвоенным идентификатором</returns>
        public Task<Blog> Add(Blog blog);
        /// <summary>
        /// Редактирование блог
        /// </summary>
        /// <param name="blog">Модель - новые данные</param>
        /// <returns>Модель с обновленными данными</returns>
        public Task<Blog> Edit(Blog blog);
        /// <summary>
        /// Удалить блог
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true / null- Если блог удален, иначе false / сообщение об ошибке</returns>
        public Task<(bool Result, string Error)> Delete(int id);
    }
}

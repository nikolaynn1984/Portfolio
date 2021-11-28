using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Repository.Interfase
{
    /// <summary>
    /// Обработка данных - взаимодействие с API Сервером
    /// </summary>
    interface IDataHandler<T>
    {
        /// <summary>
        /// Получение списка данных
        /// </summary>
        /// <returns>Список даныых</returns>
        Task<IEnumerable<T>> GetItems();
        /// <summary>
        /// Данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель с данными</returns>
        Task<T> GetItemById(int id);
        /// <summary>
        /// Добавление данных
        /// </summary>
        /// <param name="model">Добавляемые данные</param>
        /// <returns>Данные с идентификатором если успех, в противном случае null</returns>
        Task<T> Add(T model);
        /// <summary>
        /// Редактирование данных
        /// </summary>
        /// <param name="model">Обновленные данные</param>
        /// <returns>Обновленные данные если успех, в противном случае null</returns>
        Task<T> Edit(T model);
        /// <summary>
        /// Удаление данных по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true -если успешно, в пртивном случае false</returns>
        Task<bool> Delete(int id);
    }
}

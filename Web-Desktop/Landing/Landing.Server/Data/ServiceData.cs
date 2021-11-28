using System.Collections.Generic;
using System.Threading.Tasks;
using Landing.Interface;
using Landing.Model.Data;
using Landing.Server.Database.Query;

namespace Landing.Server.Data
{
    /// <summary>
    /// Обработка данных по Сервисам
    /// </summary>
    public class ServiceData : IServices
    {
        private readonly BaseService baseService;
        public ServiceData()
        {
            this.baseService = new BaseService(Connection.GetConnectionString());
        }
        /// <summary>
        /// Получить все записи услун
        /// </summary>
        /// <returns>Список</returns>
        public async Task<IEnumerable<Services>> GetItems()
        {
            return await baseService.GetItems();
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<Services> GetItemById(int id)
        {
            var item = await baseService.GetItemById(id);
            return item;
        }

        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="model">Объект Services </param>
        /// <returns>Объект Services с заданым идентификатором</returns>
        public async Task<Services> Add(Services services)
        {
            try
            {
                return await baseService.Add(services);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Удаление записи 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true - если редактирование прошло успешно, в противном случае false</returns>
        public async Task<(bool Result, string Error)> Delete(int id)
        {
            bool result = true;
            string error = "Ok";
            try
            {
                var item = await baseService.Remove(id);
                if (!item)
                {
                    return (Result: false, Error: "Идентификатор не найден");
                }
            }
            catch
            {
                result = false;
                error = "Что то пошло не так";
            }
            return (Result: result, Error: error);
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект Services с обновлеными даными</param>
        /// <returns>Services если редактирование прошло успешно, в противном случа null</returns>
        public async Task<Services> Edit(Services services)
        {
            try
            {
                return await baseService.Edit(services);
            }
            catch
            {
                return null;
            }
        }


    }
}

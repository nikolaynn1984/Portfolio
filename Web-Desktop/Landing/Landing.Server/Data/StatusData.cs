using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Landing.Interface;
using Landing.Model.Data;
using Landing.Server.Database.Query;

namespace Landing.Server.Data
{
    public class StatusData : IApplicationStatus
    {
        private readonly BaseStatus baseStatus;

        public StatusData()
        {
            baseStatus = new BaseStatus(Connection.GetConnectionString());
        }
        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Список</returns>
        public async Task<IEnumerable<RequisitionStatus>> GetItems()
        {
            return await baseStatus.GetItems();
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<RequisitionStatus> GetItemById(int id)
        {
            return await baseStatus.GetItemById(id);
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="model">Объект RequisitionStatus </param>
        /// <returns>Объект RequisitionStatus с заданым идентификатором</returns>
        public async Task<RequisitionStatus> Add(RequisitionStatus status)
        {
            try
            {
                status = await baseStatus.Add(status);
            }
            catch(Exception e)
            {
                status = null;
                Debug.WriteLine(e.Message);
            }
            return status;
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
                var status = await baseStatus.Remove(id);
                if (!status)
                {
                    return (Result: false, Error: "Идентификатор не найден");
                }

            }
            catch (Exception e)
            {
                result = false;
                error = "Что то пошло не так";
                Debug.WriteLine(e.Message);
            }
            return (Result: result, Error: error);
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект RequisitionStatus с обновлеными даными</param>
        /// <returns>RequisitionStatus если редактирование прошло успешно, в противном случа null</returns>
        public async Task<RequisitionStatus> Edit(RequisitionStatus status)
        {
            try
            {
                return await baseStatus.Edit(status);
            }
            catch
            {
                return null;
            }
        }

        
    }
}

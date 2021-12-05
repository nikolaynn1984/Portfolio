using Landing.Library.Model;
using Landing.Library.Interfaces;
using Landing.Repository.Interfase;
using Landing.Repository.Loader;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Repository
{
    /// <summary>
    /// Статусы запросов
    /// </summary>
    public class RRequisitionStatus : IApplicationStatus
    {

        public static event Action<RequisitionStatus> StatusEventChanged;

        private readonly IDataHandler<RequisitionStatus> data;
        private readonly RequisitionStatus status = new RequisitionStatus();
        public RRequisitionStatus(string url, string token)
        {
            this.data = new DataHandler<RequisitionStatus>(status, url, token);
        }
        /// <summary>
        /// Получение списка данных
        /// </summary>
        /// <returns>Список даныых</returns>
        public async Task<IEnumerable<RequisitionStatus>> GetItems()
        {
            var list = await data.GetItems();
            return list;
        }
        /// <summary>
        /// Данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель с данными</returns>
        public async Task<RequisitionStatus> GetItemById(int id)
        {
            return await data.GetItemById(id);
        }
        /// <summary>
        /// Добавление данных
        /// </summary>
        /// <param name="model">Добавляемые данные</param>
        /// <returns>Данные с идентификатором если успех, в противном случае null</returns>
        public async Task<RequisitionStatus> Add(RequisitionStatus model)
        {

            var item = await data.Add(model);
            if (item != null) StatusEventChanged?.Invoke(item);

            return item;
        }
        /// <summary>
        /// Удаление данных по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true -если успешно, в пртивном случае false</returns>
        public async Task<(bool Result, string Error)> Delete(int id)
        {
            var item = await data.Delete(id);
            if (!item) return (Result: item, Error:"Что то пошло не так");
            return (Result: item, Error:"");
        }
        /// <summary>
        /// Редактирование данных
        /// </summary>
        /// <param name="model">Обновленные данные</param>
        /// <returns>Обновленные данные если успех, в противном случае null</returns>
        public async Task<RequisitionStatus> Edit(RequisitionStatus model)
        {
            var item = await data.Edit(model);
            if (item != null) StatusEventChanged?.Invoke(item);
            return item;
        }


    }
}

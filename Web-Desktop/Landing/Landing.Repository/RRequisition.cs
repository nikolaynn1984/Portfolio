using System;
using Landing.Interface;
using Landing.Model.Data;
using Landing.Repository.Interfase;
using Landing.Repository.Loader;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Landing.Repository.Hub;

namespace Landing.Repository
{
    public class RRequisition : IApplication
    {

        public static event Action<Requisition> RequisitionEvenetChanged;
        public static event Action<int> EditRequest;

        private readonly IDataHandler<Requisition> data;
        public DateTime DateStart;
        public DateTime DateFinish = DateTime.MinValue.AddMonths(1);
        private static Requisition requisition;
        public RRequisition(string url, string token)
        {
            requisition = new Requisition();
            this.data = new DataHandler<Requisition>(requisition, url, token);
            EditRequest += EditRequesr;
        }

        private async void EditRequesr(int id)
        {
            Requisition item = await GetItemById(id);
            RequisitionEvenetChanged?.Invoke(item);
        }

        public static void Create(int id)
        {
            EditRequest?.Invoke(id);
        }
        /// <summary>
        /// Получение списка запросов
        /// </summary>
        /// <returns>Список даныых</returns>
        public async Task<IEnumerable<Requisition>> GetItems()
        {
            IEnumerable<Requisition> list = await data.GetItems();
            IEnumerable<Requisition> order = list.OrderByDescending(s => s.Date).ToList();
            return order;
        }
        /// <summary>
        /// Данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель с данными</returns>
        public async Task<Requisition> GetItemById(int id)
        {
            return await data.GetItemById(id);
        }
        /// <summary>
        /// Общее количество заявок
        /// </summary>
        /// <returns>Количетво</returns>
        public async Task<int> GetCount()
        {
            IEnumerable<Requisition> item = await GetItems();
            return item != null ? item.Count() : 0;
        }
        /// <summary>
        /// Фильтр значений между начаьной и конечной датах
        /// </summary>
        /// <param name="dateStart">Начальная дата</param>
        /// <param name="dateFinish">Конечная дата</param>
        /// <returns>Список с выбранныем диапазоном дат</returns>
        public async Task<IEnumerable<Requisition>> GetDateBetween(DateTime dateStart, DateTime dateFinish)
        {
            DateStart = dateStart.Date;
            if (DateFinish > DateStart) DateFinish = dateFinish.Date;
            else DateFinish = dateStart.Date;
            var items = await GetItems();
            var filterlist = items.Where(RequisitionFilter).ToList();
            return filterlist;
        }
        /// <summary>
        /// Фильтр
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private  bool RequisitionFilter(object obj)
        {
            var date1 = DateStart.Date;
            var date2 = DateFinish.Date;
            Requisition item = obj as Requisition;

            if (DateStart > DateTime.MinValue)
            {
                return item.Date >= date1 && item.Date <= date2;
            }
            return true;

        }
        /// <summary>
        /// Добавление данных
        /// </summary>
        /// <param name="model">Добавляемые данные</param>
        /// <returns>Данные с идентификатором если успех, в противном случае null</returns>

        public async Task<Requisition> Add(Requisition model)
        {
            model.Date = DateTime.Now;
            model.StatusId = 1;
            var item = await data.Add(model);
            if (item != null)
                if(!Message.IsConnected)
                RequisitionEvenetChanged?.Invoke(item);

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
            if (!item) return (Result: item, Error: "Что то пошло не так");
            return (Result: item, Error: "");
        }
        /// <summary>
        /// Редактирование данных
        /// </summary>
        /// <param name="model">Обновленные данные</param>
        /// <returns>Обновленные данные если успех, в противном случае null</returns>
        public async Task<Requisition> Edit(Requisition model)
        {
            var edit = await GetItemById(model.Id);
            edit.StatusId = model.StatusId;

            var item = await data.Edit(edit);
            if (item != null)
                if (!Message.IsConnected)
                    RequisitionEvenetChanged?.Invoke(item);
            return item;
        }


    }
}

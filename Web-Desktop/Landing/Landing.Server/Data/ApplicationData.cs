using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Landing.Library.Model;
using Landing.Library.Interfaces;
using Landing.Server.Database.Query;
using Landing.Server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Landing.Server.Data
{
    /// <summary>
    /// Список заявок
    /// </summary>
    public class ApplicationData : IApplication
    {
        private readonly IHubContext<LandingHub> hubContext;
        private readonly BaseApplication baseApplication;
        private readonly BaseStatus baseStatus;
        public ApplicationData(IHubContext<LandingHub> hubContext)
        {
            this.baseApplication = new BaseApplication(Connection.GetConnectionString());
            this.baseStatus = new BaseStatus(Connection.GetConnectionString());
            this.hubContext = hubContext;
        }
        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Список</returns>
        public async Task<IEnumerable<Requisition>> GetItems()
        {
            List<Requisition> list = await baseApplication.GetItems();
            List<RequisitionStatus> status = await baseStatus.GetItems();
            foreach(var items in list)
            {
                items.Status = status.SingleOrDefault(s => s.Id == items.StatusId);
            }
            
            return list;
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<Requisition> GetItemById(int id)
        {
            Requisition item = await baseApplication.GetItemById(id);

            item.Status = await baseStatus.GetItemById(item.StatusId);
            return item;
        }


        // <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="model">Объект Requisition </param>
        /// <returns>Объект Requisition с заданым идентификатором</returns>
        public async Task<Requisition> Add(Requisition application)
        {
            try
            {
                application.OrderNumber = await OrderGenerat();

                application = await baseApplication.Add(application);

                if(application != null)
                {
                    await hubContext.Clients.All.SendAsync("EditRequest", application.Id);
                }
            }
            catch (Exception e)
            {
                application = null;
                Debug.WriteLine(e.Message);
            }
            return application;
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
                result = await baseApplication.Remove(id);
                if (!result)
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
        /// <param name="model">Объект Requisition с обновлеными даными</param>
        /// <returns>Requisition если редактирование прошло успешно, в противном случа null</returns>
        public async Task<Requisition> Edit(Requisition application)
        {
            
            try
            {
                Requisition item = await baseApplication.Edit(application);
                if(item != null)
                {
                    await hubContext.Clients.All.SendAsync("EditRequest", application.Id);
                }
                return item;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Генератор номера заявки
        /// </summary>
        /// <returns>Номер</returns>
        private async Task<int> OrderGenerat()
        {
            int number = 1;
            int count = await baseApplication.GetCount();
            if(count != 0)
            {
                number = count;
                number++;
            }
            return number;
        }


    }
}

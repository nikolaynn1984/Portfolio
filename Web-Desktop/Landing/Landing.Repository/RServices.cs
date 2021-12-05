using System;
using Landing.Library.Model;
using Landing.Repository.Interfase;
using Landing.Repository.Loader;
using System.Collections.Generic;
using System.Threading.Tasks;
using Landing.Library.Interfaces;

namespace Landing.Repository
{
    public class RServices : IServices
    {
        public static event Action<Services> ServicesEventChanged;

        private readonly IDataHandler<Services> data;
        private static Services services;
        /// <summary>
        /// Конструктор реппозитория
        /// </summary>
        /// <param name="url">Путь API</param>
        /// <param name="token">Токен</param>
        public RServices(string url, string token)
        {
            services = new Services();
            this.data = new DataHandler<Services>(services, url, token);
        }

        /// <summary>
        /// Получение списка данных
        /// </summary>
        /// <returns>Список даныых</returns>
        public async Task<IEnumerable<Services>> GetItems()
        {
            return await data.GetItems();
        }
        /// <summary>
        /// Данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Модель с данными</returns>
        public async Task<Services> GetItemById(int id)
        {
            return await data.GetItemById(id);
        }
        /// <summary>
        /// Добавление данных
        /// </summary>
        /// <param name="model">Добавляемые данные</param>
        /// <returns>Данные с идентификатором если успех, в противном случае null</returns>
        public async Task<Services> Add(Services model)
        {
            var item = await data.Add(model);
            if (item != null) ServicesEventChanged?.Invoke(item);

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
        public async Task<Services> Edit(Services model)
        {
            var item = await data.Edit(model);
            if (item != null) ServicesEventChanged?.Invoke(item);
            return item;
        }


    }
}

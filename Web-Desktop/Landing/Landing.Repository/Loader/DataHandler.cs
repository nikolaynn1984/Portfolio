using Landing.Library.Model;
using Landing.Repository.Interfase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Landing.Repository.Loader
{
    class DataHandler<T> : IDataHandler<T>
     {
        private HttpClient client;
        private readonly string controller;
        /// <summary>
        /// Связт данных с сервером
        /// </summary>
        /// <param name="model">Обрабатываеммая моедь </param>
        /// <param name="url">ссылка сайта</param>
        /// <param name="token">Токен пользователя</param>
        internal DataHandler(T model, string url, string token)
        {
            this.client = new HttpClient();
            this.controller = GetControllerName(model);
            client.BaseAddress = new Uri(url + "api/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
        /// <summary>
        /// Получение списка данных
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetItems()
        {
            try
            {
                var responce = await client.GetAsync(controller);
                if (responce.IsSuccessStatusCode)
                {
                    return  JsonConvert.DeserializeObject<IEnumerable<T>>(responce.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return null;
                }
            }
            catch(Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Получение данных по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Данные</returns>
        public async Task<T> GetItemById(int id)
        {
            try
            {
                var responce = await client.GetAsync($"{controller}/{id}");
                if (responce.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(responce.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return default;
                }
            }
            catch(Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return default;
            }
        }
        /// <summary>
        /// Добавление данных
        /// </summary>
        /// <param name="model">Добавляемые данные</param>
        /// <returns>Данные с идентификатором</returns>
        public async Task<T> Add(T model)
        {
            try
            {
                var responce = await client.PostAsJsonAsync(controller, model);
                if (responce.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(responce.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return default;
                }
            }
            catch(Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return default;
            }
        }
        /// <summary>
        /// Удаление данных по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true - если удаление прошло успешно, в противном случае false</returns>
        public async Task<bool> Delete(int id)
        {
            try
            {
                var responce = await client.DeleteAsync($"{controller}/{id}");
                if (responce.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return false;
                }
            }
            catch (Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return false;
            }
        }
        /// <summary>
        /// Редактирование данных
        /// </summary>
        /// <param name="model">Обновленные данные</param>
        /// <returns>Обновленные данные если прошло успешно</returns>
        public async Task<T> Edit(T model)
        {
            try
            {
                var responce = await client.PutAsJsonAsync(controller, model);
                if (responce.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(responce.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return default;
                }
            }
            catch(Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return default;
            }
        }





        /// <summary>
        /// Получение имени контроллера
        /// </summary>
        /// <param name="model">Модкль</param>
        /// <returns>Имя контроллера</returns>
        private static string GetControllerName(T model)
        {
            string result;
            switch (model)
            {
                case Requisition: result = "Applications"; break;
                case RequisitionStatus: result = "ApplicationStatus"; break;
                case Blog: result = "Blogs"; break;
                case Images: result = "Images"; break;
                case Menu: result = "Menus"; break;
                case Portfolio: result = "Portfolios"; break;
                case Services: result = "Services"; break;
                case Social: result = "Socials"; break;
                default: result = "None";break;
            }

            return result;
        } 
    }
}

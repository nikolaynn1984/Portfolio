using Landing.Interface;
using Landing.Model.Data;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Landing.Repository.Loader
{
    class PageData : IPageView
    {
        private HttpClient client;
        public PageData(string url, string token)
        {
            this.client = new HttpClient();

            client.BaseAddress = new Uri(url + "api/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
        /// <summary>
        /// Редактирование информации 
        /// </summary>
        /// <param name="model">Обновленные данные</param>
        /// <returns>true - если редактирование прошло успешно, в противном случае false</returns>
        public async Task<bool> Edit(PageView model)
        {
            try
            {
                var responce = await client.PutAsJsonAsync("Page", model);
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
            finally
            {

            }
        }
        /// <summary>
        /// Параметры главной страницы
        /// </summary>
        /// <returns>Информация</returns>
        public async Task<PageView> Get()
        {
            try
            {
                var responce = await client.GetAsync("Page");
                if (responce.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<PageView>(await responce.Content.ReadAsStringAsync());
                    return result;
                }
                else
                {
                    Failt.ErrorsHandler.Create(responce);
                    return null;
                }
            }
            catch (Exception e)
            {
                Failt.ErrorsHandler.AddMessage(e.Message);
                return null;
            }
            finally
            {

            }
        }
    }
}

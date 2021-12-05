using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Landing.Library.ViewModel;
using Landing.Repository.Interfase;
using Newtonsoft.Json;

namespace Landing.Repository.Loader
{
    class Account : IAccount
    {
        private HttpClient client;
        private string token;
        /// <summary>
        /// Аккаунт пользователя
        /// </summary>
        /// <param name="server"></param>
        public Account(string server, string token)
        {
            this.client = new HttpClient();
            this.token = token;

            client.BaseAddress = new Uri(server);
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="model">Модель авторизации</param>
        /// <returns>Данные о пользователе</returns>
        public async Task<UserResult> Login(LoginViewModel model)
        {
            try
            {
                var responce = await client.PostAsJsonAsync("api/Login", model);
                if (responce.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<UserResult>(await responce.Content.ReadAsStringAsync());
                    return result;
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
            finally
            {
                
            }
            
            
        }
        /// <summary>
        /// Решистрация пользователя
        /// </summary>
        /// <param name="model">Модель регистрации</param>
        /// <returns>Данные о пользователе</returns>
        public async Task<UserResult> Register(RegisterViewModel model)
        {
            try
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var responce = await client.PostAsJsonAsync("api/Register", model);
                if (responce.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<UserResult>(await responce.Content.ReadAsStringAsync());
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
            finally
            {
                
            }
            
        }
    }
}

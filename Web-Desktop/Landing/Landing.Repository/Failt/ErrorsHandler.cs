using Landing.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Landing.Repository.Failt
{
    /// <summary>
    /// Обработка ошибок
    /// </summary>
    public class ErrorsHandler
    {
        private static List<Errors> errors;

        public static event Action<List<Errors>> ErrorEvent;

        public ErrorsHandler()
        {
            errors = new List<Errors>();
        }
        /// <summary>
        /// Создание списка ошибок
        /// </summary>
        /// <param name="result">Ответ сервера responce.Content.ReadAsStringAsync().Result</param>
        public static void Create(HttpResponseMessage responce)
        {
            string result = responce.Content.ReadAsStringAsync().Result;
            errors = new List<Errors>();
            errors.Clear();
            if (responce.StatusCode.ToString() == "Unauthorized")
            {
                errors.Add(new Errors { Name = "Authorized", Result = "Отказано в доступе" });
            }
            else
            {
                var message = JObject.Parse(result)["errors"].ToArray();
                Errors error = new Errors();

                foreach (var item in message)
                {
                    error.Name = GetName(item.Path.ToString());
                    error.Result = item.First.First.ToString();

                    errors.Add(error);
                }
            }
            
            ErrorEvent?.Invoke(errors);

        }
        /// <summary>
        /// Дoбавление ошибки исключений 
        /// </summary>
        /// <param name="message">Сообщение</param>
        public static void AddMessage(string message)
        {
            errors = new List<Errors>();
            errors.Clear();
            errors.Add(new Errors { Name = "Catch", Result = message });
            ErrorEvent?.Invoke(errors);
        }
        /// <summary>
        /// Обрабатывает название, отделяет от слова errors.
        /// </summary>
        /// <param name="name">Название</param>
        /// <returns>Название</returns>
        private static string GetName(string name)
        {
            string[] results = name.Split('.');
            return results[1];
        }

    }
}

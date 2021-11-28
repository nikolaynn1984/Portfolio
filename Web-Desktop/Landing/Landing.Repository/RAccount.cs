using Landing.Model.ViewModel;
using Landing.Repository.Interfase;
using Landing.Repository.Loader;
using System;
using System.Threading.Tasks;

namespace Landing.Repository
{
    public class RAccount 
    {
        public static event Action<bool> ActivAccountEventChanged;
        public static event Action<UserResult> UserEventChanged;
        private readonly IAccount account;
        private static UserResult user;
        public RAccount(string url, string token = null)
        {
            this.account = new Account(url, token);
        }
        /// <summary>
        /// Авторизация пользователя 
        /// В случае удачи передает полученные данные пользователя в UserEventChanged
        /// </summary>
        /// <param name="model">Данные пользователя</param>
        public async Task<UserResult> Login(LoginViewModel model)
        {
            var item = await account.Login(model);
            if(item != null)
            {
                if (item.Role == LRols.Admin.ToString())
                {
                    
                    
                    ActivAccount(true);
                }
                else
                {
                    ActivAccount(false);
                    Failt.ErrorsHandler.AddMessage("Отказанно в доступе");
                }
                user = item;
                UserAccount(user);
            }
            return item;
        }
        /// <summary>
        /// Регистрация нового пользователя, роль по умолчание = гость
        /// </summary>
        /// <param name="model">Модель данных</param>
        /// <returns>Инфомаци о пользователе</returns>
        public async Task<UserResult> Register(RegisterViewModel model)
        {
            user = await account.Register(model);
            return user;
        }
        /// <summary>
        /// Активный аккаунт администратора
        /// </summary>
        /// <param name="result">true - если активный</param>
        public static void ActivAccount(bool result)
        {
            ActivAccountEventChanged?.Invoke(result);
        }
        /// <summary>
        /// Информация о пользователе
        /// </summary>
        /// <param name="result">Модель с данными</param>
        public static void UserAccount(UserResult result)
        {
            UserEventChanged?.Invoke(result);
        }

        enum LRols
        {
            Admin,
            Guest
        }
    }
}

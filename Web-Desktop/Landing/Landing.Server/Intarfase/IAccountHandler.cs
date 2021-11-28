﻿using Landing.Model.ViewModel;
using System.Threading.Tasks;

namespace Landing.Server.Intarfase
{
    public interface IAccountHandler
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login">Данные пользователя</param>
        /// <returns>Модель пользователя</returns>
        public Task<UserResult> Login(LoginViewModel login);
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="register">Данные для регистрации</param>
        /// <returns>Модель пользователя</returns>
        public Task<UserResult> Register(RegisterViewModel register);
    }
}

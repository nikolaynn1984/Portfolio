using Landing.Model.ViewModel;
using System.Threading.Tasks;

namespace Landing.Repository.Interfase
{
    /// <summary>
    /// Интерфейс авторизации
    /// </summary>
    interface IAccount
    {
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserResult> Login(LoginViewModel model);
        /// <summary>
        /// Регистраци
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<UserResult> Register(RegisterViewModel model);
    }
}

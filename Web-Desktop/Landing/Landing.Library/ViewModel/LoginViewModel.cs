using Landing.Library.Model;
using System.ComponentModel.DataAnnotations;

namespace Landing.Library.ViewModel
{
    /// <summary>
    /// Модель для авторизации
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Требуется указать Email"), MaxLength(50, ErrorMessage = "Количество символов не должно превышать 50")]
        [Display(Name = "Адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Не корректнотно введен Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Требуется указать пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
    /// <summary>
    /// Модель для регистрации
    /// </summary>
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Требуется указать Email"), MaxLength(50, ErrorMessage = "Количество символов не должно превышать 50")]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Требуется указать пароль")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
    /// <summary>
    /// Модель получаемого рузультата 
    /// </summary>
    public class UserResult
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public int ImageId { get; set; }
        public Images GetImages { get; set; }

        public string Role { get; set; }
        public bool Succeeded { get; set; }
    }
    /// <summary>
    /// Модель для проверки пользователя
    /// </summary>
    public class UserCheck
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int ImageId { get; set; }
        public Images GetImages { get; set; }

        public string Role { get; set; }
        public bool Succeeded { get; set; }
    }
}

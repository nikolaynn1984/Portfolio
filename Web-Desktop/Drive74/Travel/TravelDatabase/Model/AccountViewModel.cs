using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TravelDatabase.Model
{
    
        public class LoginViewModel
        {
            [Required, MaxLength(50)]
            [Display(Name = "Адрес электронной почты")]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

             public string ReturnUrl { get; set; }
        }

        public class RegisterViewModel
        {
            [Required, MaxLength(50)]
            [EmailAddress]
            [Display(Name = "Адрес электронной почты")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Подтверждение пароля")]
            [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
            public string ConfirmPassword { get; set; }
        }
}

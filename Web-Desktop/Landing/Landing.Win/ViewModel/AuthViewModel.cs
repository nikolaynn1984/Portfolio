using Landing.Repository;
using Landing.Win.Helper;
using System;
using System.Windows;
using Landing.Repository.Failt;
using System.Collections.Generic;
using Landing.Library;
using Landing.Library.ViewModel;
using Landing.Library.Notifity;
using System.ComponentModel;

namespace Landing.Win.ViewModel
{
    public class AuthViewModel : BaseNotifiry, IDataErrorInfo
    {
        private readonly RAccount account;
        public AuthViewModel()
        {
            this.account = new RAccount(App.url);
            LoadVisible = Visibility.Collapsed;
            Elements = true;
            Saved = false;
            ConfigureLoad();
            ErrorsHandler.ErrorEvent += ErrorEventChanged;
        }
        /// <summary>
        /// Отображаем сообщения об ошибке
        /// </summary>
        /// <param name="errors">Ошибка</param>
        private void ErrorEventChanged(List<Errors> errors)
        {

            ErrorText = errors[0].Result;
        }

        private RelayCommand closeCommand;
        private RelayCommand loginCommand;



        private Visibility loadvisible;
        public Visibility LoadVisible
        {
            get { return this.loadvisible; }
            set { this.loadvisible = value; OnPropertyChanged(nameof(this.LoadVisible)); }
        }

        

        public RelayCommand LoginCommand => loginCommand ??= new RelayCommand(Autorize);
        public RelayCommand CloseCommand => closeCommand ??= new RelayCommand(CloseWindow);
        /// <summary>
        /// Закрыть окно
        /// </summary>
        private void CloseWindow(object obj)
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// Запускаем прокрутку на кнопке
        /// </summary>
        private  void OnLoad()
        {
            LoadVisible = Visibility.Visible;
            Elements = false;
        }
        /// <summary>
        /// Останавливаем прокрутку на кнопке
        /// </summary>
        private void OffLoad()
        {
            LoadVisible = Visibility.Collapsed;
            Elements = true;
        }
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="obj"></param>
        private async void Autorize(object obj)
        {
            var item = new LoginViewModel()
            {
                Email = Login,
                Password = Password
            };

            OnLoad();
            await App.Account.Login(item);
            OffLoad();

           
            if (Saved) ConfigureSave();
        }
        /// <summary>
        /// Проверяем данные о пользователя в конфигураторе, если есть то авторизируем
        /// </summary>
        private async void ConfigureLoad()
        {
            var configer = LoginConfiguration.Get();

            if(configer != null)
            {
                Login = configer.Email;
                var item = new LoginViewModel()
                {
                    Email = configer.Email,
                    Password = configer.Token
                };
                OnLoad();
                await App.Account.Login(item);
                OffLoad();
            }
        }
        /// <summary>
        /// Сохранение данных пользователя
        /// </summary>
        private void ConfigureSave()
        {
            try
            {
                if (login.Length >= 1 && Password.Length >= 1)
                {
                    LoginConfiguration.Save(login, Password);
                }
            }catch (Exception ex)
            {

            }
           

        }

        public string Error { get { return null; } }
        /// <summary>
        /// Проверка валидации
        /// </summary>
        /// <param name="columnName"></param>
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;

                var valid = ValidateAuth();

                Saved = valid.result;
                if (valid.result)
                {
                    error = valid.error;
                }
                return error;
            }
        }
        /// <summary>
        /// Валидатор полей ввода
        /// </summary>
        /// <returns></returns>
        private (bool result, string error) ValidateAuth()
        {
            bool result = true;
            string error = String.Empty;
            if (Login != null)
            {
                if (login.Length < 5)
                {
                    ErrorsHandler.AddMessage("Логин не может быть менбше 5 символов");
                    result = false;
                }
                else
                {
                    if (login.IndexOf("@") < 0 && login.IndexOf(".") < 0)
                    {
                        ErrorsHandler.AddMessage("Не верный формат логина");
                        result = false;
                    }
                }
            }
            else
            {
                ErrorsHandler.AddMessage("Логин не может быть пустым");
                result = false;
            }

            if (Password != null)
            {
                if (Password.Length < 5)
                {
                    ErrorsHandler.AddMessage("Пароль не может быть меньше 5 символов");
                    result = false;
                }
            }
            else
            {
                ErrorsHandler.AddMessage("Пароль не может быть пустым");
                result = false;
            }
            if (result) ErrorsHandler.AddMessage("");
            return (result, error);
        }

        private bool saved;
        public bool Saved
        {
            get { return this.saved; }
            set { this.saved = value; OnPropertyChanged(nameof(this.Saved)); }
        }


        private string password;
        public string Password
        {
            get { return this.password; }
            set { this.password = value; OnPropertyChanged(nameof(this.Password)); }
        }

        private string login;
        public string Login
        {
            get { return this.login; }
            set { this.login = value; OnPropertyChanged(nameof(this.Login)); }
        }

        private bool elements;
        public bool Elements
        {
            get { return this.elements; }
            set { this.elements = value; OnPropertyChanged(nameof(this.Elements)); }
        }
        private string errortext;
        public string ErrorText
        {
            get { return this.errortext; }
            set { this.errortext = value; OnPropertyChanged(nameof(this.ErrorText)); }
        }


    }
}

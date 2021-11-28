using Landing.Model.ViewModel;
using Landing.Repository;
using Landing.Repository.Hub;
using Landing.Win.View;
using System;
using System.Windows;

namespace Landing.Win
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string url = "http://localhost:5000/";
        public static string TelegramToken = String.Empty;
        private static Message message;
        public App()
        {
            RAccount.UserEventChanged += Account_UserEventChanged;
            RAccount.ActivAccountEventChanged += ActivAccountEventChanged;
        }

        

        private void ActivAccountEventChanged(bool result)
        {
            
        }

        private async static void Account_UserEventChanged(UserResult model)
        {
            if (model != null)
            {
                user = model;
                AdminWindow admin = new AdminWindow();

                message = new Message(url, model.Token);


                foreach (Window window in Current.Windows)
                {
                    if (window.Name == "authWindow")
                    {
                        window.Close(); break;
                    }
                }
                admin.Show();

                await message.Connect();                

            }
            else
            {
                foreach (Window window in Application.Current.Windows)
                {
                    window.Close();
                }
                AuthWindow auth = new AuthWindow();
                auth.Show();

                await message.Disconnect();
            }
            
        }

        public static UserResult user = new UserResult();

        private void App_StartupAsync(object sender, StartupEventArgs e)
        {
            MainWindow = new AuthWindow();
            MainWindow.Show();
        }

        private static RAccount account = new RAccount(url, user.Token);
        public static RAccount Account
        {
            get { return account; }
            set { account = value; }
        }
    }
}

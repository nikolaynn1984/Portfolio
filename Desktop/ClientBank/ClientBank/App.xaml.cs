using System.Windows;
using ClientBank.View;
using ClientBank.ViewModel;
using StoreDatabase;

namespace ClientBank
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        private delegate void Factorial();
        private   void App_StartupAsync(object sender, StartupEventArgs e)
        {
            if (ConnectionClient.ConnectionTest())
            {
                AccountViewModel viewModel = new AccountViewModel();
                MainWindow = new AccountView() { DataContext = viewModel };
                MainWindow.Show();

                RepositoryAccount.LoadAsync();
            }
            else
            {
                MessageBox.Show("Отсутствует соединение с базой");
            } 
        }

        private static AccountData.Repository repositoryAccount = new AccountData.Repository();
        public static AccountData.Repository RepositoryAccount
        {
            get { return repositoryAccount; }
            set { repositoryAccount = value; }
        }

        private static CustomersData.Repository repositoryCustom = new CustomersData.Repository();
        public static CustomersData.Repository RepositoryCustom
        {
            get { return repositoryCustom; }
            set { repositoryCustom = value; }
        }
    }
}

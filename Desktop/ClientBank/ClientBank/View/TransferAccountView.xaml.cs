using AccountData.Model;
using ClientBank.ViewModel;
using System.Windows;

namespace ClientBank.View
{
    /// <summary>
    /// Логика взаимодействия для TransferAccountView.xaml
    /// </summary>
    public partial class TransferAccountView : Window
    {
        public TransferAccountView(Accounts accounts)
        {
            InitializeComponent();
            this.DataContext = new TransferAccountViewModel(accounts);
        }
    }
}

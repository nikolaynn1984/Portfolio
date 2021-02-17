using AccountData.Model;
using ClientBank.ViewModel;
using System.Windows;

namespace ClientBank.View
{
    /// <summary>
    /// Логика взаимодействия для AddDepositView.xaml
    /// </summary>
    public partial class AddDepositView : Window
    {
        public AddDepositView(Deposit deposit)
        {
            InitializeComponent();
            this.DataContext = new AddDepositViewModel(deposit);
        }
    }
}

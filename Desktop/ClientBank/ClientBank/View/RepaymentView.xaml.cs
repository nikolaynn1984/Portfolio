using AccountData.Model;
using ClientBank.ViewModel;
using System.Windows;

namespace ClientBank.View
{
    /// <summary>
    /// Логика взаимодействия для RepaymentView.xaml
    /// </summary>
    public partial class RepaymentView : Window
    {
        public RepaymentView(Accounts accounts)
        {
            InitializeComponent();
            this.DataContext = new RepaymentViewModel(accounts);
        }
    }
}

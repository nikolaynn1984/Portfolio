using ClientBank.ViewModel;
using System.Windows;

namespace ClientBank.View
{
    /// <summary>
    /// Логика взаимодействия для AccountView.xaml
    /// </summary>
    public partial class AccountView : Window
    {
        public AccountView()
        {
            InitializeComponent();
            this.Closing += (o, e) => AccountViewModel.CloseWindow(e);
            AccountControlTab.SelectionChanged += (o, e) => AccountViewModel.AccountControlTab_SelectionChanged(o, e);
        }

       
    }
}

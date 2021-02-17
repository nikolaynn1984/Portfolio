using AccountData.Model;
using ClientBank.ViewModel;
using System.Windows;

namespace ClientBank.View
{
    /// <summary>
    /// Логика взаимодействия для ReplenishView.xaml
    /// </summary>
    public partial class ReplenishView : Window
    {
        public ReplenishView(Accounts accounts)
        {
            InitializeComponent();
            this.DataContext = new ReplenishViewModel(accounts);
        }
    }
}

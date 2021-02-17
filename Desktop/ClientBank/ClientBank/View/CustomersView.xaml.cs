using ClientBank.ViewModel;
using System.Windows;

namespace ClientBank.View
{
    /// <summary>
    /// Логика взаимодействия для CustomersView.xaml
    /// </summary>
    public partial class CustomersView : Window
    {
        public CustomersView()
        {
            InitializeComponent();
            this.DataContext = new CustomersViewModel();
        }

    }
}

using AccountData.Model;
using ClientBank.ViewModel;
using System.Windows;

namespace ClientBank.View
{
    /// <summary>
    /// Логика взаимодействия для AddAccountView.xaml
    /// </summary>
    public partial class AddAccountView : Window
    {
        public AddAccountView(Personal personal)
        {
            InitializeComponent();
            this.DataContext = new AddAccountViewModel(personal);
        }
    }
}

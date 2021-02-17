using AccountData.Model;
using ClientBank.ViewModel;
using System.Windows;

namespace ClientBank.View
{
    /// <summary>
    /// Логика взаимодействия для AddCreditView.xaml
    /// </summary>
    public partial class AddCreditView : Window
    {
        public AddCreditView(Credits credits)
        {
            InitializeComponent();
            this.DataContext = new AddCreditViewModel(credits); 
        }
    }
}

using System.Windows.Controls;
using Landing.Win.ViewModel;

namespace Landing.Win.LanControls
{
    /// <summary>
    /// Логика взаимодействия для ContactsView.xaml
    /// </summary>
    public partial class ContactsView : UserControl
    {
        public ContactsView()
        {
            InitializeComponent();
            this.DataContext = new ContactsViewModel(webView);
        }
    }
}

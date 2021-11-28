using Landing.Win.ViewModel;
using System.Windows.Controls;

namespace Landing.Win.LanControls
{
    /// <summary>
    /// Логика взаимодействия для ServicesView.xaml
    /// </summary>
    public partial class ServicesView : UserControl
    {
        public ServicesView()
        {
            InitializeComponent();
            this.DataContext = new ServicesViewModel();
        }
    }
}

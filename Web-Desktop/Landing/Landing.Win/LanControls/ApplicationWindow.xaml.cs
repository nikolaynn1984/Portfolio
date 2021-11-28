using Landing.Win.ViewModel;
using System.Windows.Controls;

namespace Landing.Win.LanControls
{
    /// <summary>
    /// Логика взаимодействия для ApplicationWindow.xaml
    /// </summary>
    public partial class ApplicationWindow : UserControl
    {
        public ApplicationWindow()
        {
            InitializeComponent();
            this.DataContext = new ApplicationViewModel();
        }
    }
}

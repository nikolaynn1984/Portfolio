using Landing.Win.ViewModel;
using System.Windows.Controls;

namespace Landing.Win.LanControls
{
    /// <summary>
    /// Логика взаимодействия для ProjectsView.xaml
    /// </summary>
    public partial class ProjectsView : UserControl
    {
        public ProjectsView()
        {
            InitializeComponent();
            this.DataContext = new ProjectViewModel();
        }
    }
}

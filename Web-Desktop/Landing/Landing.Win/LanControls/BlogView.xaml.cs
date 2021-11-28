using Landing.Win.ViewModel;
using System.Windows.Controls;

namespace Landing.Win.LanControls
{
    /// <summary>
    /// Логика взаимодействия для BlogView.xaml
    /// </summary>
    public partial class BlogView : UserControl
    {
        public BlogView()
        {
            InitializeComponent();
            this.DataContext = new BlogViewModel();
        }
    }
}

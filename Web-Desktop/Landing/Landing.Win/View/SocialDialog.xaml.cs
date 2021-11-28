using Landing.Win.ViewModel;
using System.Windows;

namespace Landing.Win.View
{
    /// <summary>
    /// Логика взаимодействия для SocialDialog.xaml
    /// </summary>
    public partial class SocialDialog : Window
    {
        public SocialDialog()
        {
            InitializeComponent();
            this.DataContext = new SocialDialogViewModel();
            socialGrid.RowEditEnding += SocialDialogViewModel.SocialGrid_RowEditEnding;
        }
    }
}

using ClientBank.ViewModel;
using System.Windows;

namespace ClientBank.View
{
    /// <summary>
    /// Логика взаимодействия для HistoryView.xaml
    /// </summary>
    public partial class HistoryView : Window
    {
        public HistoryView()
        {
            InitializeComponent();
            this.DataContext = new HistoryViewModel();
        }
    }
}

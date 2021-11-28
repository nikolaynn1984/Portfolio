using Landing.Win.ViewModel;
using System.Windows.Controls;

namespace Landing.Win.LanControls
{
    /// <summary>
    /// Логика взаимодействия для TelegramBotView.xaml
    /// </summary>
    public partial class TelegramBotView : UserControl
    {
        public TelegramBotView()
        {
            InitializeComponent();
            this.DataContext = new MessageBotViewModel();
        }
    }
}

using ClientBank.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace ClientBank.View
{
    /// <summary>
    /// Логика взаимодействия для MessageView.xaml
    /// </summary>
    public partial class MessageView : Window
    {
        public MessageView(int Id)
        {
            InitializeComponent();
            this.DataContext = new MessageViewModel(Id);
        }

        private void gridBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
        }
    }
}

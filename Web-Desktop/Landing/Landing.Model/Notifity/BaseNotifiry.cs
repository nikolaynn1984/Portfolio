using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Landing.Model.Notifity
{
    /// <summary>
    /// Базовый класс Notifity
    /// </summary>
    public class BaseNotifiry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

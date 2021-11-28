using Landing.Win.Helper;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Landing.Win.ViewModel
{
    public class AdminViewModel : INotifyPropertyChanged
    {

        public AdminViewModel()
        {
            User = App.user.Email;
            MenuVisible = Visibility.Visible;
        }

        private static RelayCommand closeWindow;
        private static RelayCommand maximizeWindow;
        private static RelayCommand minimizedWindow;
        private static RelayCommand menuChanged;
        /// <summary>
        /// Команда, закрыть окно
        /// </summary>
        public static RelayCommand CloseWindow => closeWindow ??= new RelayCommand(Close);
        /// <summary>
        /// Команда разворачивания окна
        /// </summary>
        public static RelayCommand MaximizeWindow => maximizeWindow ??= new RelayCommand(MaximizeWin);
        /// <summary>
        /// Команда сворачивания окна
        /// </summary>
        public static RelayCommand MinimizedWindow => minimizedWindow ??= new RelayCommand(MinimizedWin);



        public  RelayCommand MenuChanged => menuChanged ??= new RelayCommand(MenuView);




        /// <summary>
        /// Открытие и закрытие меню
        /// </summary>
        /// <param name="obj"></param>
        private  void MenuView(object obj) =>   MenuVisible = MenuVisible == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;

        /// <summary>
        /// Развертываение окна
        /// </summary>
        /// <param name="obj">Окно</param>
        private static void MaximizeWin(object obj)
        {
            Window window = obj as Window;
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }
        /// <summary>
        /// Закрытие приложения
        /// </summary>
        private static void Close(object obj)
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// Сварачивание окна
        /// </summary>
        /// <param name="obj">Окно</param>
        private static void MinimizedWin(object obj)
        {
            Window window = obj as Window;
            if (window.WindowState != WindowState.Minimized)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private Visibility menuvisible;
        public Visibility MenuVisible
        {
            get { return this.menuvisible; }
            set { this.menuvisible = value; OnPropertyChanged(nameof(this.MenuVisible)); }
        }

        private string user;
        public string User
        {
            get { return this.user; }
            set { this.user = value; OnPropertyChanged(nameof(this.User)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

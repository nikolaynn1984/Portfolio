using System.ComponentModel;
using System.Runtime.CompilerServices;
using Landing.Repository;
using Landing.Library.Model;
using Landing.Win.Helper;
using System.Threading.Tasks;

namespace Landing.Win.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly RPageView menu;
        private  PageView page;
        public MainPageViewModel()
        {
            menu = new RPageView(App.url, App.user.Token);
            page = new PageView();
            Task.Run(Load);
            IsEnabled = false;
            EditButtomEnabled = true;
        }
       
        private RelayCommand editpageinfo;
        /// <summary>
        /// Команда редактировая данных 
        /// </summary>
        public RelayCommand EditPageInfo => editpageinfo ??= new RelayCommand(EditPage);
        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="obj">Объект</param>
        private async void EditPage(object obj)
        {
            if(!IsEnabled)
            {
                IsEnabled = true;
            }
            else
            {
                IsEnabled = false;
                EditButtomEnabled = false;
                await menu.Edit(Page);
                EditButtomEnabled = true;
            }
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        private async Task Load()
        {
            Page = await menu.Get();

            ImageItem = Page.GetImages;
        
        }

        private Images imageitem;
        public Images ImageItem
        {
            get { return this.imageitem; }
            set { this.imageitem = value; OnPropertyChanged(nameof(this.ImageItem)); }
        }

        private bool isenabled;
        public bool IsEnabled
        {
            get { return this.isenabled; }
            set { this.isenabled = value; OnPropertyChanged(nameof(this.IsEnabled)); }
        }

        private bool editbutton;
        public bool EditButtomEnabled
        {
            get { return this.editbutton; }
            set { this.editbutton = value; OnPropertyChanged(nameof(this.EditButtomEnabled)); }
        }
        public PageView Page
        {
            get { return this.page; }
            set { this.page = value; OnPropertyChanged(nameof(this.Page)); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

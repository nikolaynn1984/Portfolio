using Landing.Library.Model;
using Landing.Repository;
using Landing.Win.Helper;
using Landing.Win.View;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Landing.Win.ViewModel
{
    public class ContactsViewModel : INotifyPropertyChanged
    {

        private readonly RPageView menu;
        private readonly RSocial social;
        private readonly string path = Path.Combine(Directory.GetCurrentDirectory(), "Map", "Index.html");
        private readonly WebView2 webView;
        private List<Social> sociallist;

        private PageView page;

        public ContactsViewModel(WebView2 wevView)
        {
            menu = new RPageView(App.url, App.user.Token);
            social = new RSocial(App.url, App.user.Token);
            page = new PageView();
            Task.Run(Load);
            ButtonText = "Редактировать";
        
            IsEnabled = false;
            EditButtomEnabled = true;
            webView = wevView;
            webView.Source = new Uri(path);
            webView.NavigationStarting += WebView_NavigationStarting;
            this.sociallist = new List<Social>();

        }

       
        /// <summary>
        /// Запускаем картку 
        /// </summary>
        private async void WebView_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (Page != null)
            {
                PageView pageView = Page;
                pageView.GetImages = null;
                string pageitem = JsonConvert.SerializeObject(pageView);
                await webView.CoreWebView2.ExecuteScriptAsync("document.addEventListener('DOMContentLoaded', function(){" +
                                                                              $"  SetCoordinate({pageitem});" +
                                                                               "})");
            }

        }
        private RelayCommand editpageinfo;
        public RelayCommand EditPageInfo => editpageinfo ??= new RelayCommand(EditPage);

        private RelayCommand showeditsocial;
        public RelayCommand ShowEditSocialDialog => showeditsocial ??= new RelayCommand(ShowSocialDialog);
        /// <summary>
        /// Показываем окно соцсетей
        /// </summary>
        /// <param name="obj"></param>
        private void ShowSocialDialog(object obj)
        {
            SocialDialog list = new SocialDialog();
            list.ShowDialog();
        }

        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="obj">Объект</param>
        private async void EditPage(object obj)
        {
            string result = await webView.CoreWebView2.ExecuteScriptAsync(" EditView();");
            if (result != null)
            {
                if (!IsEnabled)
                {
                    IsEnabled = true;
                    ButtonText = "Сохранить";
                }
                else
                {

                    Result item = JsonConvert.DeserializeObject<Result>(result);
                    
                    Page.LabelColor = item.color;
                    Page.Coordinates = $"{item.coodinatx},{item.coodinaty}";
                    IsEnabled = false;
                    await webView.CoreWebView2.ExecuteScriptAsync(" SaveSetting();");
                    EditButtomEnabled = false;
                    await menu.Edit(Page);
                    EditButtomEnabled = true;
                    ButtonText = "Редактировать";
                    App.TelegramToken = Page.TelegramToken;

                }
            }

           
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        private async Task Load()
        {
            Page = await menu.Get();
            App.TelegramToken = Page.TelegramToken;
            bool result = true;
            if (!File.Exists(path))
            {
              result =  Map.MapСhecking();
            }
            if (result)
            {
                PathUri = path;
            }

            IEnumerable<Social> items = await social.GetItems();

            Parallel.ForEach(items, item => AddSocial(item));

        }
        /// <summary>
        /// Добавляет запись в список
        /// </summary>
        /// <param name="item">Модель данных</param>
        private void AddSocial(Social item)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                SocialList.Add(item);
            });
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

        private string pathuri;
        public string PathUri
        {
            get { return this.pathuri; }
            set { this.pathuri = value; OnPropertyChanged(nameof(this.PathUri)); }
        }
        private string buttontext;
        public string ButtonText
        {
            get { return this.buttontext; }
            set { this.buttontext = value; OnPropertyChanged(nameof(this.ButtonText)); }
        }

        public PageView Page
        {
            get { return this.page; }
            set { this.page = value; OnPropertyChanged(nameof(this.Page)); }
        }
        public List<Social> SocialList
        {
            get { return this.sociallist; }
            set { this.sociallist = value; OnPropertyChanged(nameof(this.SocialList)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


    }

    public class Result
    {
        public string color { get; set; }
        public string coodinatx { get; set; }
        public string coodinaty { get; set; }
    }
}

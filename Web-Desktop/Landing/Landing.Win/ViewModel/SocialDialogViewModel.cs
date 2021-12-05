using Landing.Library.Model;
using Landing.Repository;
using Landing.Win.Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Landing.Win.ViewModel
{
    public class SocialDialogViewModel : INotifyPropertyChanged
    {
        private static RSocial social;
        private ObservableCollection<Social> sociallist;
        private ListCollectionView socialLisView;
        OpenFileDialog dialog = new OpenFileDialog();
        public SocialDialogViewModel()
        {
            social = new RSocial(App.url, App.user.Token);
            sociallist = new ObservableCollection<Social>();
            socialLisView = (ListCollectionView)CollectionViewSource.GetDefaultView(sociallist);
            socialLisView.CurrentChanging += SocialLisView_CurrentChanged;
            RSocial.SocialEventChanged += RSocial_SocialEventChanged;
            Task.Run(Load);
        }
        /// <summary>
        /// Слушаем изменение поля 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SocialLisView_CurrentChanged(object sender, EventArgs e)
        {
            Social item = socialLisView.CurrentItem as Social;
            if (item != null)
            {
                SocialItem = item;
            }
        }

        private RelayCommand addemptysocial;
        private RelayCommand removeItem;
        private RelayCommand editIconsItem;
        public RelayCommand AddEmptySocial => addemptysocial ??= new RelayCommand(AddItem);
        public RelayCommand RemoveItemCommand => removeItem ??= new RelayCommand(RemoveItem);
        public RelayCommand EditIconDialog => editIconsItem ??= new RelayCommand(EditDialogIcon);
        /// <summary>
        /// Редактируем иконку
        /// </summary>
        /// <param name="obj"></param>
        private async void EditDialogIcon(object obj)
        {
            var ident = obj;
            Social item = new Social();
            if(ident is int)
            {

                item = SocialList.SingleOrDefault(s => s.Id == (int)ident);
            }
                dialog.Title = "Выбрать иконки";
                dialog.Filter = "Портативная сетевая графика | *.png; | Все возможные форматы | *.jpg; *.jpeg; *.png; | JPEG |*.jpg;*.jpeg;";
            if (dialog.ShowDialog() == true)
            {
                long fileSize = new FileInfo(dialog.FileName).Length;
                double sizeMB = (double)(fileSize / 1000.00 / 1000.00);
                string filePath = dialog.FileName.ToString();
                if(sizeMB <= 1)
                {
                    Social model = new Social()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Link = item.Link
                    };
                    await social.Edit(model, filePath);
                }
                else
                {
                    MessageBox.Show("Размер файла не должен превышать 1 МБ");
                }

            }
        }
        /// <summary>
        /// Удаляем запись
        /// </summary>
        /// <param name="obj"></param>
        private async void RemoveItem(object obj)
        {
            var ident = obj;
            if(ident is int)
            {
                var result = await social.Delete((int)ident);
                if (result.Result)
                {
                    var item = SocialList.FirstOrDefault(s => s.Id == (int)ident);
                    SocialList.Remove(item);
                }
            }
        }

        private async void AddItem(object obj)
        {
            _ = await social.Add(new Social { Id = 0, Name = "", Link = "", ImageId = 0 });          
        }
        /// <summary>
        /// Изменения записи или добавления
        /// </summary>
        /// <param name="model"></param>
        private void RSocial_SocialEventChanged(Social model)
        {
            Social item = SocialList.SingleOrDefault(s => s.Id == model.Id);
            if(item == null)
            {
                AddSocial(model);
            }
            else
            {
                item.Name = model.Name;
                item.Link = model.Link;
                item.ImageId = model.ImageId;
                item.GetImages = model.GetImages;

                
            }
        }


        /// <summary>
        /// Загрузка данных
        /// </summary>
        private async Task Load()
        {
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
        private Social socialitem;
        public Social SocialItem
        {
            get { return this.socialitem; }
            set { this.socialitem = value; OnPropertyChanged(nameof(this.SocialItem)); }
        }

        public ObservableCollection<Social> SocialList
        {
            get { return this.sociallist; }
            set { this.sociallist = value; OnPropertyChanged(nameof(this.SocialList)); }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private BitmapImage image;
        public BitmapImage Image
        {
            get { return this.image; }
            set { this.image = value; OnPropertyChanged(nameof(this.Image)); }
        }
        private static async Task EditSocialItem(Social model)
        {

            await social.Edit(model);
        }

        internal static void SocialGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            Social item = e.Row.Item as Social;
          
            if(item != null)
            {
               Task.Run(() => EditSocialItem(item));
                
            }
        }
    }
}

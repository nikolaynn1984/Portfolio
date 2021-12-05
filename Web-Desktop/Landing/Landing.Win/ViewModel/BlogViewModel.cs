using Landing.Library.Model;
using Landing.Repository;
using Landing.Win.Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Landing.Win.ViewModel
{
    public class BlogViewModel : ViewModelHelper
    {
        private readonly RBlog data;
        private ObservableCollection<Blog> bloglist;
        OpenFileDialog dialog = new OpenFileDialog();
        public BlogViewModel()
        {
            data = new RBlog(App.url, App.user.Token);
            bloglist = new ObservableCollection<Blog>();
            Task.Run(Load);
            RBlog.BlogEvenetChanged += RBlog_BlogEvenetChanged;
        }
        /// <summary>
        /// Отлавливает изменения 
        /// </summary>
        /// <param name="model">Измененная модель</param>
        private void RBlog_BlogEvenetChanged(Blog model)
        {
            Blog item = BlogList.SingleOrDefault(s => s.Id == model.Id);
            if (item == null)
            {
                AddBlog(model);
            }
            else
            {
                item.Name = model.Name;
                item.Description = model.Description;
                item.GetImages = model.GetImages;
            }
        }
        /// <summary>
        /// Загрузка данных
        /// </summary>
        private async Task Load()
        {
            IEnumerable<Blog> items = await data.GetItems();

            Parallel.ForEach(items, item => AddBlog(item));
            if (items.Any())
            {
                ChangeView(true);
            }
            else
            {
                ChangeView(false);
            }
        }
        /// <summary>
        /// Добавляет запись в список
        /// </summary>
        /// <param name="item">Модель данных</param>
        private void AddBlog(Blog item)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                BlogList.Add(item);
            });
        }

        private RelayCommand saveitemcommant;
        private RelayCommand edititemcommand;
        private RelayCommand removeitemcommand;
        private RelayCommand additemcommand;
        private RelayCommand cancelcommand;
        private RelayCommand showdialog;

        public RelayCommand ShowDialog => showdialog ??= new RelayCommand(ShowImage);
        public RelayCommand SaveItemCommant => saveitemcommant ??= new RelayCommand(SaveItem);
        public RelayCommand EditItemCommand => edititemcommand ??= new RelayCommand(EditItem);
        public RelayCommand RemoveItemCommand => removeitemcommand ??= new RelayCommand(RemoveItem);
        public RelayCommand AddItemCommand => additemcommand ??= new RelayCommand(AddItem);
        public RelayCommand CancelCommand => cancelcommand ??= new RelayCommand(CancelEditor);


        /// <summary>
        /// Сохранение изменений редактирования или добавление новой записи
        /// </summary>
        private async void SaveItem(object obj)
        {
            bool result = ChekItem();
            if (result)
            {
                Blog item = new Blog
                {
                    Name = Name,
                    Description = Description,
                    CreadDate = DateTime.Now
                };

                if (BlogItem == null)
                {
                    if (path == null)
                    {
                        await data.Add(item);
                    }
                    else
                    {
                        await data.Add(item, path);
                    }
                }
                else
                {
                    item.Id = BlogItem.Id;
                    item.ImageId = BlogItem.ImageId;
                    if (string.IsNullOrEmpty(path))
                    {
                        await data.Edit(item);
                    }
                    else
                    {
                        await data.Edit(item, path);
                    }
                }
                ChangeView(true);
                CleanItem();
            }
        }
        /// <summary>
        /// Редактирование записи, переключает в режим редактирования и заполняет данные выбранного объекта
        /// </summary>
        /// <param name="id">Идентификатор</param>
        private void EditItem(object id)
        {
            path = "";
            TextBox ident = (TextBox)id;
            if (ident != null)
            {
                BlogItem = BlogList.SingleOrDefault(s => s.Id == Convert.ToInt32(ident.Text));
                string directory = Directory.GetCurrentDirectory();
                if (BlogItem != null)
                {
                    string pathitem = Path.Combine(directory, "Files", BlogItem.GetImages.Location, BlogItem.GetImages.Name);
                    if (File.Exists(pathitem))
                    {
                        Image = new BitmapImage(new Uri(pathitem));
                    }
                    Name = BlogItem.Name;
                    Description = BlogItem.Description;
                    ChangeView(false);
                }
            }
        }
        /// <summary>
        /// Удаляет запись
        /// </summary>
        /// <param name="id">Идентификатор</param>
        private async void RemoveItem(object id)
        {
            TextBox ident = (TextBox)id;
            if (ident != null)
            {
                Blog item = BlogList.SingleOrDefault(s => s.Id == Convert.ToInt32(ident.Text));
                if (item != null)
                {
                    MessageBoxResult mesresult = MessageBox.Show($"Вы уверены что хотите \nудалить запись {item.Name}", "Удалить запись", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    if (mesresult == MessageBoxResult.OK)
                    {
                        await data.Delete(item.Id);
                        BlogList.Remove(item);
                    }
                }

            }
        }

        /// <summary>
        /// Открывает диалоговое окно для выбора изображения
        /// </summary>
        private void ShowImage(object obj)
        {
            dialog.Title = "Выбрать изобрадения";
            dialog.Filter = "Все возможные форматы | *.jpg; *.jpeg; *.png | JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg| Портативная сетевая графика (*.png)|*.png";
            if (dialog.ShowDialog() == true)
            {
                Image = new BitmapImage(new Uri(dialog.FileName));
                path = dialog.FileName.ToString();

            }

        }
        /// <summary>
        /// Добавление записи. Переключает в режим редактирования 
        /// </summary>
        private void AddItem(object obj)
        {
            ChangeView(false);
        }
        /// <summary>
        /// Отмена изменений
        /// </summary>
        private void CancelEditor(object obj)
        {
            ChangeView(true);
            CleanItem();
        }
        /// <summary>
        /// Чиста данных в редакторе
        /// </summary>
        private void CleanItem()
        {
            Name = string.Empty;
            Description = string.Empty;
            BlogItem = null;
            path = string.Empty;
            Image = null;
        }


        /// <summary>
        /// Переключает окна список или редактор
        /// </summary>
        /// <param name="list">true - если показать проекты, иначе false</param>
        private void ChangeView(bool list)
        {
            if (list)
            {
                ListVisible = Visibility.Visible;
                EditVisivle = Visibility.Collapsed;
                HeadText = "Блог";
            }
            else
            {
                EditVisivle = Visibility.Visible;
                ListVisible = Visibility.Collapsed;

                HeadText = BlogItem != null ? "Редактировать блог" : "Новая запись";
            }
        }

        /// <summary>
        /// Проверят заполнение полей
        /// </summary>
        /// <returns>true - если поля валидный, в противном случае false</returns>
        private bool ChekItem()
        {
            bool result = true;
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description))
            {
                result = false;
                MessageBox.Show("Поля не должны быть пустые");
            }
            else
            {
                if (Name.Length <= 4 || Description.Length <= 4)
                {
                    result = false;
                    MessageBox.Show("Значения полей не должно быть меньше 5 знаков");
                }
                if (Name.Length > 49)
                {
                    result = false;
                    MessageBox.Show("Значени поля - Название проекта не должно превышать 50 символов");
                }
            }

            return result;
        }

        private Blog blogitem;
        public Blog BlogItem
        {
            get { return blogitem; }
            set { blogitem = value; OnPropertyChanged(nameof(BlogItem)); }
        }
        public ObservableCollection<Blog> BlogList
        {
            get { return bloglist; }
            set { bloglist = value; OnPropertyChanged(nameof(BlogList)); }
        }
    }
}

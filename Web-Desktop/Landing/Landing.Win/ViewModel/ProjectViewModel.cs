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
    public class ProjectViewModel : ViewModelHelper
    {
        private readonly RPortfolio data;
        private ObservableCollection<Portfolio> portfoliolist;
        OpenFileDialog dialog = new OpenFileDialog();
        public ProjectViewModel()
        {
            data = new RPortfolio(App.url, App.user.Token);
            portfoliolist = new ObservableCollection<Portfolio>();
            Task.Run(LoadData);
            
            RPortfolio.PortfolioEventChanged += RPortfolio_PortfolioEventChanged;
        }
        /// <summary>
        /// Отлавливает изменения 
        /// </summary>
        /// <param name="model">Модель данных</param>
        private void RPortfolio_PortfolioEventChanged(Portfolio model)
        {
            Portfolio item = PortfolioList.SingleOrDefault(s => s.Id == model.Id);
            if (item == null)
            {
                AddProject(model);
            }
            else
            {
                item.Name = model.Name;
                item.Description = model.Description;
                item.GetImages = model.GetImages;
            }
            
        }

        private RelayCommand showdialog;
        private RelayCommand saveprogect;
        private RelayCommand editprogect;
        private RelayCommand removeprogect;
        private RelayCommand addprojectcommand;
        private RelayCommand cancelprogectcommand;
        

        public RelayCommand ShowDialog => showdialog ??= new RelayCommand(ShowImage);
        public RelayCommand SaveProgect => saveprogect ??= new RelayCommand(SaveItem);
        public RelayCommand EditProgect => editprogect ??= new RelayCommand(EditItem);
        public RelayCommand RemoveProgect => removeprogect ??= new RelayCommand(RemoveItem);
        public RelayCommand AddProjectCommand => addprojectcommand ??= new RelayCommand(AddProject);
        public RelayCommand CancelProgectCommand => cancelprogectcommand ??= new RelayCommand(CancelProgect);
        /// <summary>
        /// Удаление проекта
        /// </summary>
        /// <param name="id">Идентификатор</param>
        private async void RemoveItem(object id)
        {
            TextBox ident = (TextBox)id;
            if(ident != null)
            {
                Portfolio item  = PortfolioList.SingleOrDefault(s => s.Id == Convert.ToInt32(ident.Text));
                if(item != null)
                {
                    MessageBoxResult mesresult = MessageBox.Show($"Вы уверены что хотите \nудалить проект {item.Name}", "Удалить запись", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    if (mesresult == MessageBoxResult.OK)
                    {
                        await data.Delete(item.Id);
                        PortfolioList.Remove(item);
                    }
                }
                
            }
        }
        /// <summary>
        /// Открывает окно редактирования и передает данные
        /// </summary>
        /// <param name="id"></param>
        private void EditItem(object id)
        {
            path = string.Empty;
            TextBox ident = (TextBox)id;
            if (ident != null)
            {
                PortfolioItem = PortfolioList.SingleOrDefault(s => s.Id == Convert.ToInt32(ident.Text));
                string directory =  Directory.GetCurrentDirectory();
                if(PortfolioItem.GetImages != null)
                {
                    string pathitem = Path.Combine(directory, "Files", PortfolioItem.GetImages.Location, PortfolioItem.GetImages.Name);
                    if (File.Exists(pathitem))
                    {
                        Image = new BitmapImage(new Uri(pathitem));
                    }
                }
                

                Name = PortfolioItem.Name;
                Description = PortfolioItem.Description;

                ChangeView(false);
            }
           
        }



        /// <summary>
        /// Отмена изменений
        /// </summary>
        private void CancelProgect(object obj)
        {
            ChangeView(true);
            CleanEditor();
        }
        /// <summary>
        /// Очистака данных окна редактирования
        /// </summary>
        private void CleanEditor()
        {
            Name = string.Empty;
            Description = string.Empty;
            path = string.Empty;
            Image = null;
            PortfolioItem = null;
        }
        /// <summary>
        /// Открывает окно доббавления записи
        /// </summary>
        private void AddProject(object obj)
        {
            ChangeView(false);
        }
        /// <summary>
        /// Сохраняет изменения или добавляет новую запись
        /// </summary>
        private async void SaveItem(object obj)
        {
            bool result = ChekItem();
            if (result)
            {
                Portfolio item = new Portfolio()
                {
                    Name = Name,
                    Description = Description,
                };

                if(PortfolioItem == null)
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
                    item.Id = PortfolioItem.Id;
                    item.ImageId = PortfolioItem.ImageId;
                    
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
                CleanEditor();
            }
        }
        /// <summary>
        /// Проверят заполнение полей
        /// </summary>
        /// <returns>true - если поля валидный, в противном случае false</returns>
        private  bool ChekItem()
        {
            bool result = true;
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Description))
            {
                result = false;
                MessageBox.Show("Поля не должны быть пустые");
            }
            else
            {
                if(Name.Length <= 4 || Description.Length <= 4)
                {
                    result = false;
                    MessageBox.Show("Значения полей не должно быть меньше 5 знаков");
                }
                if (Name.Length > 149)
                {
                    result = false;
                    MessageBox.Show("Значени поля - Название проекта не должно превышать 150 символов");
                }
            }

            return result;
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
        /// Загрузка базы данных
        /// </summary>
        private async Task LoadData()
        {
            IEnumerable<Portfolio> items = await data.GetItems();

            Parallel.ForEach(items, item => AddProject(item));
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
        /// Переключает окна список или редактор
        /// </summary>
        /// <param name="list">true - если показать проекты, иначе false</param>
        private void ChangeView(bool list)
        {
            if (list)
            {
                ListVisible = Visibility.Visible;
                EditVisivle = Visibility.Collapsed;
                HeadText = "Проекты";
            }
            else
            {
                EditVisivle = Visibility.Visible;
                ListVisible = Visibility.Collapsed;

                HeadText = PortfolioItem != null ? "Редактировать проект" : "Новый проект";
            }
        }

        /// <summary>
        /// Добавляет запись в список
        /// </summary>
        /// <param name="item">Модель данных</param>
        private void AddProject(Portfolio item)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                PortfolioList.Add(item);
            });
        }
      

        private Portfolio portfolioitem;
        public Portfolio PortfolioItem
        {
            get { return portfolioitem; }
            set { portfolioitem = value; OnPropertyChanged(nameof(PortfolioItem)); }
        }

        public ObservableCollection<Portfolio> PortfolioList
        {
            get { return portfoliolist; }
            set { portfoliolist = value; OnPropertyChanged(nameof(PortfolioList)); }
        }

    }
}

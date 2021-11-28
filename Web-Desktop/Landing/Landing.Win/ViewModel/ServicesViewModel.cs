using Landing.Model.Data;
using Landing.Repository;
using Landing.Win.Helper;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Landing.Win.ViewModel
{
    public class ServicesViewModel : ViewModelHelper
    {
        private readonly RServices data;
        private ObservableCollection<Services> servicesList;
        public ServicesViewModel()
        {
            data = new RServices(App.url, App.user.Token);
            servicesList = new ObservableCollection<Services>();
            Task.Run(Load);

            RServices.ServicesEventChanged += RServices_ServicesEventChanged;
        }
        /// <summary>
        /// Отлавливает изменения
        /// </summary>
        /// <param name="model">Модель данных</param>
        private void RServices_ServicesEventChanged(Services model)
        {
            Services item = ServicesList.SingleOrDefault(s => s.Id == model.Id);
            if(item == null)
            {
                AddService(model);
            }
            else
            {
                item.Name = model.Name;
                item.Description = model.Description;
            }

        }
        /// <summary>
        /// Загрузка данных
        /// </summary>
        private async Task Load()
        {
            var items = await data.GetItems();
            Parallel.ForEach(items, item => AddService(item));
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
        /// Добавляет данные в список
        /// </summary>
        /// <param name="item">Модель данных</param>
        private void AddService(Services item)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ServicesList.Add(item);
            });
        }

        private RelayCommand saveitemcommant;
        private RelayCommand edititemcommand;
        private RelayCommand removeitemcommand;
        private RelayCommand additemcommand;
        private RelayCommand cancelcommand;


        public RelayCommand SaveItemCommant => saveitemcommant ??= new RelayCommand(SaveItem);
        public RelayCommand EditItemCommand => edititemcommand ??= new RelayCommand(EditItem);
        public RelayCommand RemoveItemCommand => removeitemcommand ??= new RelayCommand(RemoveItem);
        public RelayCommand AddItemCommand => additemcommand ??= new RelayCommand(AddItem);
        public RelayCommand CancelCommand => cancelcommand ??= new RelayCommand(CancelEditor);


        /// <summary>
        /// Сохраняет или редактирует данные в базе
        /// </summary>
        private async void SaveItem(object obj)
        {
            bool result = ChekItem();
            if (result)
            {
                Services item = new Services
                {
                    Name = Name,
                    Description = Description
                };

                if(ServicesItem == null)
                {
                    await data.Add(item);
                }
                else
                {
                    item.Id = ServicesItem.Id;
                    await data.Edit(item);
                }
                ChangeView(true);
                CleanItem();
            }
        }
        /// <summary>
        /// Открывает редактор по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        private void EditItem(object id)
        {
            TextBox ident = (TextBox)id;
            if(ident != null)
            {
                ServicesItem = ServicesList.SingleOrDefault(s => s.Id == Convert.ToInt32(ident.Text));
                if (ServicesItem != null)
                {
                    Name = ServicesItem.Name;
                    Description = ServicesItem.Description;
                    ChangeView(false);
                }
            }
        }
        /// <summary>
        /// Удаляет запись по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        private async void RemoveItem(object id)
        {
            TextBox ident = (TextBox)id;
            if (ident != null)
            {
                Services item = ServicesList.SingleOrDefault(s => s.Id == Convert.ToInt32(ident.Text));
                if (item != null)
                {
                    MessageBoxResult mesresult = MessageBox.Show($"Вы уверены что хотите \nудалить запись {ServicesItem.Name}", "Удалить запись", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    if (mesresult == MessageBoxResult.OK)
                    {
                        await data.Delete(item.Id);
                        ServicesList.Remove(item);
                    }
                }

            }
        }
        /// <summary>
        /// Открывает откно редактора 
        /// </summary>
        private void AddItem(object obj)
        {
            ChangeView(false);
        }
        /// <summary>
        /// Отменяет редактирование и переключается в окно список
        /// </summary>
        private void CancelEditor(object obj)
        {
            ChangeView(true);
            CleanItem();
        }
        /// <summary>
        /// Чистит данные в редакторе
        /// </summary>
        private void CleanItem()
        {
            Name = string.Empty;
            Description = string.Empty;
            ServicesItem = null;
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
                HeadText = "Что мы можем сделать";
            }
            else
            {
                EditVisivle = Visibility.Visible;
                ListVisible = Visibility.Collapsed;

                HeadText = ServicesItem != null ? "Редактировать услугу" : "Новая услуга";
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

        

        private Services service;
        public Services ServicesItem
        {
            get { return service; }
            set { service = value; OnPropertyChanged(nameof(ServicesItem)); }
        }
        public ObservableCollection<Services> ServicesList
        {
            get { return servicesList; }
            set { servicesList = value; OnPropertyChanged(nameof(ServicesList)); }
        }
    }
}

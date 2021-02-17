using ClientBank.Helper;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CustomersData;
using CustomersData.Model;
using AccountData.Model;

namespace ClientBank.ViewModel
{
    public class CustomersViewModel: CustomersHelper, IDataErrorInfo
    {

        public CustomersViewModel()
        {
            repository = App.RepositoryCustom;
            repositoryAccount = App.RepositoryAccount;

            CustomersItems = repository.GetCustomers();
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(CustomersItems);
            view.CurrentChanged += CustomerItemChanged;
            StatusList = repository.GetStatuses();
            CitiesList = repository.GetCities();
            GetVisibility = Visibility.Collapsed;
            CustomersItem = null;
            VisibleClient = Visibility.Collapsed;
            VisibleBusines = Visibility.Collapsed;
            VIPClientVisible = Visibility.Collapsed;
            SaveButton = false;
            view.Refresh();
            CustomersItems.CollectionChanged += CustomersChanged;
        }

        private void CustomersChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MessageBox.Show("Список обновился");
        }



        /// <summary>
        /// Метод отслеживания изменения в списке сотрудников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerItemChanged(object sender, EventArgs e)
        {
            EmploeePosition = $"Запись {view.CurrentPosition + 1} из {view.Count}";
            if (view.CurrentPosition > 0) EnabledPrevious = true;
            else EnabledPrevious = false;
            if (view.CurrentPosition < view.Count - 1) EnabledNext = true;
            else EnabledNext = false;
            var item = view.CurrentItem as Customers;
            if(GetVisibility == Visibility.Visible) EditChanged(item);
            
        }

        private void EditChanged(Customers model)
        {
            StatusId = model.StatusId;
            GetEditItems(model.Id);
            GetVisibility = Visibility.Visible;
            VipStatusCheched();
        }

        /// <summary>
        /// Команда добавления клиента
        /// </summary>
        public RelayCommand AddItem
        {
            get
            {
                return additem ?? (additem = new RelayCommand((selectedItem) =>
                {
                    CleanItems();
                    GetVisibility = Visibility.Visible;
                    VIPClientVisible = Visibility.Collapsed;
                    //CustomersItem = new Personal { Id = 0 };
                    StatusEnabled = true;
                }));
            }
        }
        /// <summary>
        /// Команда редактирования пользователя
        /// </summary>
        public RelayCommand EditItem
        {
            get
            {
                return edititem ?? (edititem = new RelayCommand((selectedItem) =>
                {
                    if (selectedItem == null)
                    {
                        MessageBox.Show("Требуется выбрать сотрудника");
                        return;
                    }
                    else
                    {

                        StatusEnabled = false;
                        CustomersItem = selectedItem as Customers;
                        StatusId = CustomersItem.StatusId;
                        GetEditItems(CustomersItem.Id);
                        GetVisibility = Visibility.Visible;
                        VipStatusCheched();
                    }

                }));
            }
        }
        /// <summary>
        /// Заполжение данных редактирования
        /// </summary>
        /// <param name="id"></param>
        private void GetEditItems(int id)
        {
            if(StatusId == 4)
            {
                var item = (Busines)customers.Single(s => s.Id == id);
                ClientId = item.Id;
                Name = item.OrganizationName;
                Surname = item.PositionName;
                FIO = item.FIO;
                CityId = item.CityId;
                INN = item.INNCode;
            }
            else
            {
                var item = (PhysicalClient)customers.Single(s => s.Id == id);
                ClientId = item.Id;
                Name = item.Name;
                Surname = item.Surname;
                DateBirthday = item.DateOfBirthday.ToShortDateString();
                CityId = item.CityId;
            }
        }

        /// <summary>
        /// Команда удаления клиента
        /// </summary>
        public RelayCommand RemoveItem
        {
            get
            {
                return removeitem ?? (removeitem = new RelayCommand((selectedItem) =>
                {
                    GetVisibility = Visibility.Collapsed;
                    Customers items = selectedItem as Customers;
                    bool result = repositoryAccount.CheckAccount(items.Id);
                    if (result)
                    {
                        MessageBoxResult resultmes = MessageBox.Show($"Вы точно хотите удалить Клиента? " +
                        $"\nКлиент: {items.ClientName}", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultmes == MessageBoxResult.Yes)
                        {
                            repository.Remove(items.Id);
                            view.Refresh();
                        }

                    }
                    else MessageBox.Show("У пользователя имеются счета, запись не может быть удалена");
                }));
            }
        }

        /// <summary>
        /// Команда сохранения данных 
        /// </summary>
        public RelayCommand SaveItem
        {
            get
            {
                return saveItem ?? (saveItem = new RelayCommand((o) =>
                {
                    
                    if (SaveButton)
                    {
                        if(StatusId == 4)
                        {
                            Busines busines = new Busines
                            {
                                Id = ClientId,
                                StatusId = StatusId,
                                OrganizationName = Name,
                                PositionName = Surname,
                                INNCode = INN,
                                CityId = CityId,
                                FIO = FIO
                            };
                            repository.EditCustom(busines);
                        }
                        else
                        {
                            PhysicalClient physical = new PhysicalClient
                            {
                                Id = ClientId,
                                StatusId = StatusId,
                                Name = Name,
                                Surname = Surname,
                                DateOfBirthday = Birthday,
                                CityId = CityId
                            };
                            repository.EditCustom(physical);
                        }
                        view.Refresh();
                        CleanItems();
                        GetVisibility = Visibility.Collapsed;
                    }

                }));
            }
        }
        /// <summary>
        /// Очистка полей
        /// </summary>
        private void CleanItems()
        {
            ClientId = 0;
            SaveButton = false;
            Name = "";
            Surname = "";
            FIO = "";
            INN = 0;
            CityId = 0;
            DateBirthday = null;
        }

        /// <summary>
        /// Команда скрывает панель редактирования
        /// </summary>
        public RelayCommand ExitEdit
        {
            get
            {
                return exitedit ?? (exitedit = new RelayCommand((o) =>
                {
                    GetVisibility = Visibility.Collapsed;
                }));
            }
        }
        /// <summary>
        /// Команда переключения к следующей записи
        /// </summary>
        public RelayCommand NextCommand
        {
            get
            {
                return nextCommand ?? (nextCommand = new RelayCommand((selecteditem) =>
                {
                    if (selecteditem == null) return;
                    view.MoveCurrentToNext();
                }));
            }
        }
        /// <summary>
        /// Команда переключения к предыдущей записи
        /// </summary>
        public RelayCommand PreviousCommand
        {
            get
            {
                return previousCommand ?? (previousCommand = new RelayCommand((selecteditem) =>
                {
                    if (selecteditem == null) return;
                    view.MoveCurrentToPrevious();
                }));
            }
        }
        /// <summary>
        /// Команда переключения к первой записи
        /// </summary>
        public RelayCommand FirstCommand
        {
            get
            {
                return firstCommand ?? (firstCommand = new RelayCommand((selecteditem) =>
                {
                    if (selecteditem == null) return;
                    view.MoveCurrentToFirst();
                }));
            }
        }
        /// <summary>
        /// Команда переключения к первой записи
        /// </summary>
        public RelayCommand LastCommand
        {
            get
            {
                return lastCommand ?? (lastCommand = new RelayCommand((selecteditem) =>
                {
                   // if (selecteditem == null) return;
                    view.MoveCurrentToLast();
                }));
            }
        }
        /// <summary>
        /// Изменение статуса клиента
        /// </summary>
        public RelayCommand VIPChangedCommand
        {
            get
            {
                return vipchangedcommand ?? (vipchangedcommand = new RelayCommand((o) =>
                {
                    if (StatusId == 2) StatusId = 3;
                    else StatusId = 2;
                }));
            }
        }

        public string Error { get { return null; } }
        /// <summary>
        /// Проверка валидации данных
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                    {
                        case "StatusId": if (StatusId < 1) error = $"Требуется выбрать элемент";  break;
                        case "INN": if (INN < 1000000000) error = $"ИНН должен содержать больше 10 знаков";   break;
                        case "Name": if (Name != null) if (Name.Length < 3) error = "Поле не должно содержать меньше 5 знаков";   break;
                        case "Surname": if (Surname != null) if (Surname.Length < 3)  error = "Поле не должно содержать меньше 5 знаков";  break;
                        case "FIO": if (FIO != null) if (FIO.Length < 3) error = "Поле не должно содержать меньше 5 знаков";   break;
                        case "CityId": if (CityId == 0) error = "Требуется выбрать элемент";  break;
                        case "DateBirthday": if(StatusId != 4) DateValid(DateBirthday,out error); break;
                    }

                bool result = repository.ValidItems(StatusId, Name, Surname, StatusId, CityId, FIO, INN, DateBirthday);
                if (result) SaveButton = true;
                else SaveButton = false;
                return error;
            }
        }
        private void DateValid(string date, out string mess)
        {
            DateTime newdate = DateTime.Today;
            mess = null;
            try
            {
                repository.ValidDate(date, out newdate);
            }
            catch (BankDateException e) { mess = $"{e.Message}"; }

            Birthday = newdate;
        }

        /// <summary>
        /// Проверка VIP Статуса
        /// </summary>
        private void VipStatusCheched()
        {
            if (StatusId != 4)
            {
                VIPClientVisible = Visibility.Visible;
                if (StatusId == 2) VipStatus = false;
                else VipStatus = true;
            }
            else VIPClientVisible = Visibility.Collapsed;
        }

    }
}

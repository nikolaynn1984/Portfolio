using Landing.Repository;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Landing.Library.Model;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows;
using System.Linq;
using Landing.Win.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landing.Win.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private readonly RRequisition data;
        private readonly RRequisitionStatus status;
        FilterStatus filterStatus = FilterStatus.Today;
        private readonly string token;
        private ObservableCollection<Requisition> requisitionsList;
        private ObservableCollection<RequisitionStatus> statuseslist;
        private static event Action FilterHandler;
        public ApplicationViewModel()
        {
            this.token = App.user.Token;
            this.data = new RRequisition(App.url, token);
            this.status = new RRequisitionStatus(App.url, token);
            this.requisitionsList = new ObservableCollection<Requisition>();
            this.statuseslist = new ObservableCollection<RequisitionStatus>();
            Task.Run(GetCount);
            Task.Run( LoadData);

            listCollection = (ListCollectionView)CollectionViewSource.GetDefaultView(RequisitionsList);
            listCollection.CurrentChanged += ApplicationListChanged;

            FilterHandler += FilterHandlerChanged;

            RRequisition.RequisitionEvenetChanged += RequisitionChanged;

            DateStart = DateTime.Now;
            DateFinish = DateTime.Now;
            EditStatusVisible = Visibility.Collapsed;

        }

        private  RelayCommand editStatusWindow;
        private RelayCommand saveEditStatus;
        /// <summary>
        /// Команда сохранения 
        /// </summary>
        public RelayCommand SaveEditStatus => saveEditStatus ??= new RelayCommand(SaveStatus);
        /// <summary>
        /// Сохранение статуса
        /// </summary>
        /// <param name="obj"></param>
        private async void SaveStatus(object obj)
        {
            OpenEdit(obj);
            Requisition item = new Requisition()
            {
                Id = RequisitionItem.Id,
                StatusId = RequisitionItem.StatusId
            };
            
            await data.Edit(item);

        }
        /// <summary>
        /// Команда открытия окна редактора статуса
        /// </summary>
        public  RelayCommand EditStatusWindow => editStatusWindow ??= new RelayCommand(OpenEdit);
        /// <summary>
        /// Редактирование 
        /// </summary>
        /// <param name="obj"></param>
        private void OpenEdit(object obj)
        {
            EditStatusVisible = EditStatusVisible == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }
        /// <summary>
        /// Слушаем полсупление заявок
        /// </summary>
        /// <param name="model"></param>
        private void RequisitionChanged(Requisition model)
        {
            var newmodel = AddRequest(model);
            var item = RequisitionsList.FirstOrDefault(x => x.Id == model.Id);
            if(item != null)
            {
                item.StatusId = model.StatusId;
                item.Status = model.Status;
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    RequisitionsList.Add(newmodel);
                });
                
            }
            listCollection.Refresh();
        }
        /// <summary>
        /// Обработчик изменения фильтра
        /// </summary>
        private void FilterHandlerChanged()
        {
            listCollection.Filter = new Predicate<object>(RequisitionFilter);
        }
        /// <summary>
        /// Событие изменения списка заявок
        /// </summary>
        private void ApplicationListChanged(object sender, EventArgs e)
        {
            CurrentTotal = $"Поступило заявок за указанный период: {listCollection.Count}";

            Requisition item = listCollection.CurrentItem as Requisition;
            if(item != null)
                RequisitionItem = item;
        }

        /// <summary>
        /// Фильтр
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool RequisitionFilter(object obj)
        {
            DateTime date1 = DateStart.Date;
            DateTime date2 = DateFinish.Date;
            Requisition item = obj as Requisition;

            if (DateStart > DateTime.MinValue)
            {
                return item.Date >= date1 && item.Date <= date2;
            }
            return true;

        }
        /// <summary>
        /// Загружаем данные
        /// </summary>
        private async Task LoadData()
        {
            IEnumerable<Requisition> items = await data.GetItems();

            Parallel.ForEach(items, item =>
            {
                var m = AddRequest(item);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    RequisitionsList.Add(m);
                });
            });

            IEnumerable<RequisitionStatus> statuse = await status.GetItems();
            Statuseslist = new ObservableCollection<RequisitionStatus>(statuse);
        }

        private Requisition AddRequest(Requisition model)
        {
            Requisition item = new(model);
           
            return item;
        }
        /// <summary>
        /// Получаем количество заявок
        /// </summary>
        private async Task GetCount()
        {
            int count = await data.GetCount();
            Total = $"Всего заявок: {count}";
        }
        /// <summary>
        /// Фильтруем записи
        /// </summary>
        private  void Filter()
        {

            switch (FilterStatus)
            {
                case FilterStatus.Today:
                    DateStart = DateTime.Now;
                    DateFinish = DateStart; break;
                case FilterStatus.Yesterday:
                    DateStart = DateTime.Now.AddDays(-1);
                    DateFinish = DateStart; break;
                case FilterStatus.Week:
                    DateStart = DateTime.Now.AddDays(-7);
                    DateFinish = DateTime.Now; break;
                case FilterStatus.Month:
                    DateStart = DateTime.Now.AddMonths(-1);
                    DateFinish = DateTime.Now; break;
                default:
                    break;
            }

        }

        private DateTime datestart;
        public DateTime DateStart
        {
            get { return this.datestart; }
            set { this.datestart = value; OnPropertyChanged(nameof(this.DateStart)); FilterHandler?.Invoke(); }
        }

        private DateTime datefinish;
        public DateTime DateFinish
        {
            get { return this.datefinish; }
            set { this.datefinish = value; OnPropertyChanged(nameof(this.DateFinish)); FilterHandler?.Invoke(); }
        }

        private string total;
        public string Total
        {
            get { return this.total; }
            set { this.total = value; OnPropertyChanged(nameof(this.Total)); }
        }
        /// <summary>
        /// Статусы фильтра
        /// </summary>
        public FilterStatus FilterStatus
        {
            get { return filterStatus; }
            set
            {
                if (filterStatus == value) return;
                filterStatus = value;
                OnPropertyChanged(nameof(this.FilterStatus));
                OnPropertyChanged(nameof(this.Today));
                OnPropertyChanged(nameof(this.Week));
                OnPropertyChanged(nameof(this.Month));
                Filter();
            }
        }

        public bool Today
        {
            get { return FilterStatus == FilterStatus.Today; }
            set   {   FilterStatus = value ? FilterStatus.Today : FilterStatus; }
        }

        public bool Yesterday
        {
            get { return FilterStatus == FilterStatus.Yesterday; }
            set  {  FilterStatus = value ? FilterStatus.Yesterday : FilterStatus; }
        }

        public bool Week
        {
            get { return FilterStatus == FilterStatus.Week; }
            set  { FilterStatus = value ? FilterStatus.Week : FilterStatus; }
        }
        public bool Month
        {
            get { return FilterStatus == FilterStatus.Month; }
            set { FilterStatus = value ? FilterStatus.Month : FilterStatus; }
        }

        private Visibility editStatusVisible;
        public Visibility EditStatusVisible
        {
            get { return this.editStatusVisible; }
            set { this.editStatusVisible = value; OnPropertyChanged(nameof(this.EditStatusVisible)); }
        }

        public ListCollectionView listCollection;

      

        private Requisition requisition;
        public Requisition RequisitionItem
        {
            get { return this.requisition; }
            set { this.requisition = value; OnPropertyChanged(nameof(this.RequisitionItem)); }
        }

       

        public ObservableCollection<Requisition> RequisitionsList
        {
            get { return this.requisitionsList; }
            set { this.requisitionsList = value; OnPropertyChanged(nameof(this.RequisitionsList)); }
        }

        public ObservableCollection<RequisitionStatus> Statuseslist
        {
            get { return this.statuseslist; }
            set { this.statuseslist = value; OnPropertyChanged(nameof(this.Statuseslist)); }
        }

        private string currenttotal;
        public string CurrentTotal
        {
            get { return this.currenttotal; }
            set { this.currenttotal = value; OnPropertyChanged(nameof(this.CurrentTotal)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public enum FilterStatus {
        Today,
        Yesterday,
        Week,
        Month
    }
}

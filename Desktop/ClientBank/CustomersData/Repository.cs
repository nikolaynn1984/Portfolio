using CustomersData.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Threading;

namespace CustomersData
{
    public class Repository
    {
        public static ObservableCollection<Customers> CustomersList;
        internal static List<PhysicalClient> physicalClients;
        internal static List<Busines> busines;
        internal static List<Cities> Citiess;
        internal static List<Statuses> statuses;



        public Repository()
        {

            CustomersList = new ObservableCollection<Customers>();
            physicalClients = new List<PhysicalClient>();
            busines = new List<Busines>();
            statuses = new List<Statuses>();
            Citiess = new List<Cities>();
            DataWork.LoaderData.Run();


        }
        


        /// <summary>
        /// Загрузка списка статусов 
        /// </summary>
        /// <returns></returns>
        public List<Statuses> GetStatuses()
        {
            var items = statuses.Where(s => s.Id != 1).ToList();
            return statuses = new List<Statuses>(items);
        }
        /// <summary>
        /// Загрузка списка статусов с пунком ВСЕ
        /// </summary>
        /// <returns></returns>
        public static List<Statuses> GetStatusesAll()
        {
            return statuses;
        }
        /// <summary>
        /// Загрузка списка городом
        /// </summary>
        /// <returns></returns>
        public List<Cities> GetCities() { return Citiess; }

        public static string CitiesId(int id)
        {
            string item = Citiess.Single(s => s.Id == id).City;
            return item;
        }

        /// <summary>
        /// Получение данных клиентов
        /// </summary>
        /// <returns>Список клиентов</returns>
        public ObservableCollection<Customers> GetCustomers() {  return CustomersList; }
        /// <summary>
        /// Получение списка данных физ.лих - клиентах
        /// </summary>
        /// <returns>Списко физюлих</returns>
        public ObservableCollection<PhysicalClient> GetPhysicalClients() {  return new ObservableCollection<PhysicalClient>( physicalClients); }
        /// <summary>
        /// Получение списка данных юр.лиц - клиентов
        /// </summary>
        /// <returns>Список юр.лих</returns>
        public ObservableCollection<Busines> GetBusines() {  return new ObservableCollection<Busines>( busines); }
        /// <summary>
        /// Загрузка данных обычного клиента
        /// </summary>
        /// <param name="id">ИД Клиента</param>
        /// <returns>Модель с данными клиента</returns>
        public static PhysicalClient GetPhysicalId(int id)
        {

            PhysicalClient item = physicalClients.Single(s => s.Id == id);
            return item;
        }
        /// <summary>
        /// Загрузка данных юр.лико (клиента)
        /// </summary>
        /// <param name="id">ИД Клиента</param>
        /// <returns>Модель с данными клиента</returns>
        public static Busines GetBusinesId(int id)
        {

            Busines item = busines.Single(s => s.Id == id);
            return item;
        }

        public static Customers GetCustomersId(int id)
        {
            Customers item = CustomersList.Single(s => s.Id == id);
            return item;
        }
        /// <summary>
        /// Герератор ИД 
        /// </summary>
        /// <returns>ИД</returns>
        private static int IdGenarator()
        {
            int id = 1;
            if (CustomersList.Count != 0)
            {
                id = CustomersList.OrderBy(s => s.Id).LastOrDefault().Id;
                id += 1;
            }
            return id;
        }

        
        /// <summary>
        /// Добавление/обновление простых клиентов
        /// </summary>
        /// <param name="model">Модель клиентов</param>
        public  void EditCustom(PhysicalClient model)
        {
            if (model.Id == 0)
            {
                
                
                model.Id = IdGenarator();
                physicalClients.Add(model);
                DataWork.AddData.AddPhysical(model);
                CustomersList.Add(CustomFactory.GetCustomerFactory(model));
            }
            else
            {
                Customers itemcustomer = CustomersList.Single(s => s.Id == model.Id);
                itemcustomer.Changed(model);
                DataWork.UpdateData.UpdatePhysical(model);
            }
        }
       

        /// <summary>
        /// Добавление/обновление данных юр.лиц
        /// </summary>
        /// <param name="model">Модель данных</param>
        public  void EditCustom(Busines model)
        {
            if (model.Id == 0)
            {
              
                   model.Id = IdGenarator();
                   busines.Add(model);
                DataWork.AddData.AddBusines(model);
                CustomersList.Add(CustomFactory.GetCustomerFactory(model));
            }
            else
            {
                var itemcustomer = CustomersList.Single(s => s.Id == model.Id);

                itemcustomer.Changed(model);
                DataWork.UpdateData.UpdateBusines(model);
            }
        }

        /// <summary>
        /// Метод удаление клиента
        /// </summary>
        /// <param name="id">ИД Клиента</param>
        public void Remove(int id)
        {
            var itemcustomer = CustomersList.Single(s => s.Id == id);
            if (itemcustomer.StatusId == 4)
            {
                var item = busines.Single(s => s.Id == id);
                busines.Remove(item);
                DataWork.DeleteData.DeleteBusines(id);
            }
            else
            {
                var item = physicalClients.Single(s => s.Id == id);
                physicalClients.Remove(item);
                DataWork.DeleteData.DeletePhysical(id);
            }
            CustomersList.Remove(itemcustomer);
        }


        /// <summary>
        /// Валидация данных
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="statusid">Статус ИД</param>
        /// <param name="cityid">Город ИД</param>
        /// <param name="fio">ФИО</param>
        /// <param name="inn">ИНН</param>
        /// <param name="date">Дата рождения</param>
        /// <returns></returns>
        public bool ValidItems(int status, string name, string surname, int statusid, int cityid, string fio, long inn, string date)
        {
            bool result = true;
            bool resulDate = true;
            DateTime date1;
            try
            {
               if(status != 4) resulDate = ValidDate(date, out date1);
            }catch(BankDateException) { result = false; }
            
            //DateTime minDate = DateTime.MinValue;
            if (statusid == 4)
            {
                if (!String.IsNullOrEmpty(fio)) if (fio.Length < 3) result = false;
                if (inn < 1000000000) result = false;
            }
            if (status < 0) result = false;
            if (cityid == 0) result = false;
            if (!String.IsNullOrEmpty(name)) if (name.Length < 3) result = false;
            if (!String.IsNullOrEmpty(surname)) if (surname.Length < 3) result = false;
            if (statusid != 4) if (!resulDate) result = false;
            return result;
        }

        public bool ValidDate(string date, out DateTime newDate)
        {
            bool result = true;
            
            char[] chardate = new char[0];
            if (date != null) chardate = date.ToArray<char>();
                DateTime dates = DateTime.Today;
            
            string mes = String.Empty;
            if (date == null || date == "")
            {
                result = false;
                mes = "Поле не должно быть пустым";
            }
            else if(result)
            {
                for(int i = 0; i < chardate.Length; i++)
                {
                    if (char.IsLetter(chardate[i]))
                    {
                        result = false;
                        mes = "Поле дата не может содержать буквы";
                        break;
                    }
                }
                if(result)
                {
                    if (!DateTime.TryParseExact(date, "dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dates))
                    {
                        result = false;
                        mes = "Требуется ввести правильный формат даты - дд.мм.гггг";
                    }
                }
                
            }else result = true;
            if (!result) throw new BankDateException(mes);
            newDate = dates;
            return result;
        }

    }
}

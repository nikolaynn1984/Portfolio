using System;
using System.Collections.Generic;

using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace CustomersData.Model
{
    public class PhysicalClient : Customers, INotifyPropertyChanged
    {

        public PhysicalClient()
        {
        }
        /// <summary>
        /// Конструктор - Клиенты
        /// </summary>
        /// <param name="Id">Ид</param>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="DateOfBirthday">Дата рождения</param>
        /// <param name="CityId">Ид города</param>
        /// <param name="StatusId">Статус</param>
        public PhysicalClient(int Id, string Name, string Surname, DateTime DateOfBirthday, int CityId, int StatusId)
        {
            this.Id = Id;
            this.Name = Name;
            this.Surname = Surname;
            this.DateOfBirthday = DateOfBirthday;
            this.CityId = CityId;
            this.StatusId = StatusId;
        }

        public PhysicalClient(PhysicalClient physical)
        {
            this.Id = physical.Id;
            this.Name = physical.Name;
            this.Surname = physical.Surname;
            this.DateOfBirthday = physical.DateOfBirthday;
            this.CityId = physical.CityId;
            this.StatusId = physical.StatusId;
        }

        /// <summary>
        /// Поле - ИД
        /// </summary>
        public override int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(nameof(this.id)); }
        }
        /// <summary>
        /// Поле - Имя
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; OnPropertyChanged(nameof(this.Name)); }
        }
        /// <summary>
        /// Поле - Фамилия
        /// </summary>
        public string Surname
        {
            get { return this.surname; }
            set { this.surname = value; OnPropertyChanged(nameof(this.Surname)); }
        }
        /// <summary>
        /// Название города
        /// </summary>
        public override string CityName
        {
            get { if (this.cityid != 0) return this.cityname = Repository.CitiesId(cityid); else return this.cityname = null; }
            set { this.cityname = value; OnPropertyChanged(nameof(this.CityName)); }
        }
        /// <summary>
        /// Поле - Дата рождения
        /// </summary>
        public DateTime DateOfBirthday
        {
            get { return this.dateofbirthday; }
            set { this.dateofbirthday = value; OnPropertyChanged(nameof(this.DateOfBirthday)); }
        }
        /// <summary>
        /// Поле - ИД Города
        /// </summary>
        public int CityId
        {
            get { return this.cityid; }
            set { this.cityid = value; OnPropertyChanged(nameof(this.CityId)); }
        }
        /// <summary>
        /// Поле - Статус клиента
        /// </summary>
        public override int StatusId
        {
            get { return this.statusid; }
            set { this.statusid = value; OnPropertyChanged(nameof(this.StatusId)); }
        }

        public override string ClientName
        {
            get { return this.clientname = this.Name + " " + this.Surname; }
            set { this.clientname = value; OnPropertyChanged(nameof(this.ClientName)); }
        }

        public override string ColorList
        {
            get
            {
                string colorresult = String.Empty;
                switch (statusid)
                {
                    case 2: colorresult = "White"; break;
                    case 3: colorresult = "#ffc108"; break;
                }

                return this.colorlist = colorresult;
            }
            set { this.colorlist = value; }
        }

        public override string StatusName
        {
            get
            {
                var item = Repository.GetStatusesAll();
                return this.statusname = item.Single(s => s.Id == this.StatusId).Status;
            }
            set { this.statusname = value; OnPropertyChanged(nameof(this.StatusName)); }
        }

        public override void Changed<T>(T model)
        {
            var item = model as PhysicalClient;
            this.ClientName = item.ClientName;
            this.Name = item.Name;
            this.CityId = item.CityId;
            this.Surname = item.Surname;
            this.DateOfBirthday = item.DateOfBirthday;
            this.StatusId = item.StatusId;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        private int id;
        /// <summary>
        /// Имя
        /// </summary>
        private string name;
        /// <summary>
        /// Фамилия
        /// </summary>
        private string surname;
        /// <summary>
        /// Дата рождения
        /// </summary>
        private DateTime dateofbirthday;
        /// <summary>
        /// ИД - города
        /// </summary>
        private int cityid;
        /// <summary>
        /// VIP Статус
        /// </summary>
        private int statusid;

        private string cityname;

        private string clientname;

        private string colorlist;

        private string statusname;


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace CustomersData.Model
{
    public class Busines : Customers, INotifyPropertyChanged
    {

        public Busines()
        {
        }
        /// <summary>
        /// Конструктор - киенты организации
        /// </summary>
        /// <param name="Id">ИД</param>
        /// <param name="OrganizationName">Название организации</param>
        /// <param name="INNCode">ИНН Код</param>
        /// <param name="StatusId">ИД Статус</param>
        /// <param name="CityId">ИД Города</param>
        /// <param name="FIO">ФИО Клиента(представителя)</param>
        /// <param name="PositionName">Должность представителя</param>
        public Busines(int Id, string OrganizationName, long INNCode, int StatusId, int CityId, string FIO, string PositionName)
        {
            this.Id = Id;
            this.OrganizationName = OrganizationName;
            this.INNCode = INNCode;
            this.StatusId = StatusId;
            this.CityId = CityId;
            this.FIO = FIO;
            this.PositionName = PositionName;
        }

        public Busines(Busines model)
        {
            this.Id = model.Id;
            this.OrganizationName = model.OrganizationName;
            this.INNCode = model.INNCode;
            this.StatusId = model.StatusId;
            this.CityId = model.CityId;
            this.FIO = model.FIO;
            this.PositionName = model.PositionName;
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
        /// Поле - Название организации
        /// </summary>
        public string OrganizationName
        {
            get { return this.organizationname; }
            set { this.organizationname = value; OnPropertyChanged(nameof(this.organizationname)); }
        }
        /// <summary>
        /// Поле - ИИН Код
        /// </summary>
        public long INNCode
        {
            get { return this.inncode; }
            set { this.inncode = value; OnPropertyChanged(nameof(this.inncode)); }
        }
        /// <summary>
        /// Поле - ИД Города
        /// </summary>
        public int CityId
        {
            get { return this.cityid; }
            set { this.cityid = value; OnPropertyChanged(nameof(this.cityid)); }
        }
        /// <summary>
        /// Поле - ИД Клиента
        /// </summary>
        public string FIO
        {
            get { return this.fio; }
            set { this.fio = value; OnPropertyChanged(nameof(this.FIO)); }
        }
        /// <summary>
        /// Название города
        /// </summary>
        public override string CityName
        {
            get { return this.cityname = Repository.CitiesId(cityid); }
            set { this.cityname = value; OnPropertyChanged(nameof(this.CityName)); }
        }
        /// <summary>
        /// Поле - Должность представителя
        /// </summary>
        public string PositionName
        {
            get { return this.positionname; }
            set { this.positionname = value; OnPropertyChanged(nameof(this.PositionName)); }
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
            get { return this.clientname = this.OrganizationName; }
            set { this.clientname = value; OnPropertyChanged(nameof(this.ClientName)); }
        }

        public override string ColorList
        {
            get
            {
                return this.colorlist =  "#FFA81AC7";
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
            var item = model as Busines;
            this.ClientName = item.ClientName;
            this.OrganizationName = item.OrganizationName;
            this.INNCode = item.INNCode;
            this.StatusId = item.StatusId;
            this.CityId = item.CityId;
            this.FIO = item.FIO;
            this.PositionName = item.PositionName;
        }

        /// <summary>
        /// ИД
        /// </summary>
        private int id;
        /// <summary>
        /// Название организации
        /// </summary>
        private string organizationname;
        /// <summary>
        /// ИИН Код
        /// </summary>
        private long inncode;
        /// <summary>
        /// ИД - Города
        /// </summary>
        private int cityid;
        /// <summary>
        /// ИД - клиента
        /// </summary>
        private string fio;
        /// <summary>
        /// Должность представителя
        /// </summary>
        private string positionname;
        /// <summary>
        /// Статус клиента
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

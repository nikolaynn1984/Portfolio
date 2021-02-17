using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CustomersData.Model
{
    public abstract class  Customers: IComparable<Customers>
    {


        //public static event Action<Customers> CustomChanged;

        //public Customers() { }
      
        //public void ChangeItem(Customers customers)
        //{
        //    CustomChanged?.Invoke(customers);
        //}

        


        /// <summary>
        /// Поле - ИД
        /// </summary>
        public abstract int Id { get; set; }
        /// <summary>
        /// Поле - Статус клиента
        /// </summary>
        public abstract int StatusId { get; set; }
        /// <summary>
        /// Поле - клиент
        /// </summary>
        public abstract string ClientName { get; set; }


        public abstract string ColorList { get; set; }
        /// <summary>
        /// Поле - Статус
        /// </summary>
        public abstract string StatusName { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public abstract string CityName { get; set; }

        public virtual void Changed<T>(T model)
        {
            Customers customers = model as Customers;
            this.Id = customers.Id;
            this.StatusId = customers.StatusId;
            this.ClientName = customers.ClientName;
            this.ColorList = customers.ColorList;
            this.StatusName = customers.StatusName;
            this.CityName = customers.CityName;
        }

        public int CompareTo(Customers other)
        {
            if (this.Id > other.Id) return 1;
            else if (this.Id < other.Id) return -1;
            else return 0;
        }

    }
}

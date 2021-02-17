using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AccountData.Model
{
    public abstract class Accounts : IComparable<Accounts>
    {
        /// <summary>
        /// ИД
        /// </summary>
        public abstract int Id { get; set; }
        /// <summary>
        /// ИД - Тип счета 
        /// </summary>
        public abstract int TypeId { get; set; }
        
        /// <summary>
        /// Название - Тип счета
        /// </summary>
        public abstract string TypeName{ get; }
        /// <summary>
        /// Состояние счета
        /// </summary>
        public abstract bool State { get; set; }

        /// <summary>
        /// Имя состояния счета
        /// </summary>
        public abstract string StateName { get; }
      
        /// <summary>
        /// Дата открытия счета
        /// </summary>
        public abstract DateTime OpenDate { get; set; }

        /// <summary>
        /// Остаток на счету
        /// </summary>
        public abstract decimal Cash { get; set; }
        /// <summary>
        /// Номер счета
        /// </summary>
        public abstract long AccountNumber { get; set; }
        /// <summary>
        /// Имя клиента
        /// </summary>
        public abstract string ClientName { get;  }
        /// <summary>
        /// ИД Клиента
        /// </summary>
        public abstract int ClientId { get; set; }
        /// <summary>
        /// ИД - Статус Клиента
        /// </summary>
        public abstract  int ClientStatus { get; }
        /// <summary>
        /// Добавить сумму
        /// </summary>
        /// <param name="money">сумма</param>
        public virtual void Put(decimal money)
        {
            this.Cash += money;
            OperationHistory operation = new OperationHistory
            {
                AccountNumber = this.AccountNumber,
                Money = money,
                OperationId = 3
            };
            operation.Create(operation);
        }
        /// <summary>
        /// Снять сумму
        /// </summary>
        /// <param name="money">сумма</param>
        public virtual void Take(decimal money)
        {
            this.Cash -= money;
            OperationHistory operation = new OperationHistory
            {
                AccountNumber = this.AccountNumber,
                Money = money,
                OperationId = 4
            };
            operation.Create(operation);
        }
        /// <summary>
        /// Закрыть счет
        /// </summary>
        /// <param name="result">false если закрыть или true если открыть</param>
        public virtual void Close(bool result)
        {
            this.State = result;
        }

        public int CompareTo(Accounts other)
        {
            if (this.Id > other.Id) return 1;
            else if (this.Id < other.Id) return -1;
            else return 0;
        }
    }
}

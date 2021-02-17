using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountData.Model
{
    public class AccountType : INotifyPropertyChanged
    {
        public AccountType() { }
        /// <summary>
        /// Конструкток - тип счета
        /// </summary>
        /// <param name="Id">ИД</param>
        /// <param name="Types">Тип счета</param>
        public AccountType(int Id, string Types)
        {
            this.Id = Id;
            this.Types = Types;
        }


        /// <summary>
        /// ИД
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(nameof(this.id)); }
        }
        /// <summary>
        /// Тип счета
        /// </summary>
        public string Types
        {
            get { return this.types; }
            set { this.types = value; OnPropertyChanged(nameof(this.types)); }
        }

        /// <summary>
        /// Поле - Ид
        /// </summary>
        private int id;
        /// <summary>
        /// Тип счета
        /// </summary>
        private string types;

       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

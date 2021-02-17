using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountData.Model
{
    public class Operations : INotifyPropertyChanged
    {
        /// <summary>
        /// Конструктор - виды операции
        /// </summary>
        /// <param name="Id">ИД</param>
        /// <param name="OperationName">Название операции</param>
        public Operations(int Id, string OperationName)
        {
            this.Id = Id;
            this.OperationName = OperationName;
        }

        /// <summary>
        /// Поле - ИД
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.id = value; OnPropertyChanged(nameof(this.id)); }
        }
        /// <summary>
        /// Поле - Название операции
        /// </summary>
        public string OperationName
        {
            get { return this.operationname; }
            set { this.operationname = value; OnPropertyChanged(nameof(this.operationname)); }
        }
        /// <summary>
        /// ИД
        /// </summary>
        private int id;
        /// <summary>
        /// Название операции
        /// </summary>
        private string operationname;

       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreDatabase
{
    public class ErrorMes
    {
        public static event Action<string> ErrorSqlMessage;

        public  void ErrorSQL(string msg)
        {
            Task<string>.Factory.StartNew(StartMess, msg);
        }


        private string StartMess(object msg)
        {
            Thread.Sleep(5000);
            string m = (string)msg;
            ErrorSqlMessage?.Invoke(m);
            return m;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientBank.Helper
{
    /// <summary>
    /// Сообщения
    /// </summary>
    public static class BankMessageBox
    {
        /// <summary>
        /// Сообщения с запросом Да или Нет
        /// </summary>
        /// <param name="name">Сообщение</param>
        /// <returns>true если Да - в противном случае false</returns>
        public static bool MessYesNo(string name)
        {
            bool result = false;
            MessageBoxResult resultM = MessageBox.Show(name, "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultM == MessageBoxResult.Yes) result = true;
            return result;
        }
    }
}

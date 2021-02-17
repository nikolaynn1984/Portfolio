using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    [Flags]
    public enum Path
    {
        Personal = 1, // Обычные счета
        BankAccount = 2, // Все счета
        Cities = 3,      // Города
        Customers = 4,   //Клиенты
        Business = 5,  // Юр.лица - клиенты
        PhysicalClient = 6,   // Физ.лица - клиенты
        AccountType = 7,   //Тип счета
        Operations = 8, // Виды операций
        Repayment = 9,   // Погашения кредитов
        Deposit = 10,  // Вклады
        Credit = 11, //Кредиты
        Statuses = 12,   //Статусы клиентов
        OperationHIstory = 13    // История операций
    }
}

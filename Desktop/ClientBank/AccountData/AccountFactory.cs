using AccountData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountData
{
    static class AccountFactory
    {
        public static Accounts GetAccount<T>(T model)
        {
            if (model is Personal) { var personal = model as Personal; return new Personal(personal); }
            else if (model is Credits) { var credit = model as Credits; return new Credits(credit); }
            else if (model is Deposit) { var deposit = model as Deposit; return new Deposit(deposit); }
            else return null;

        }
    }
}

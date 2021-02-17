using AccountData.DataWork;
using AccountData.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountData.Credit
{
    public class CreditRepository
    {
        internal static List<Repayment> RepaymentsList;
        public CreditRepository()
        {
            RepaymentsList = new List<Repayment>();
            DataWork.LoadingData.LoadRepayment();
            LoadRepayment();
        }
        public List<Repayment> LoadRepayment()
        {
            return RepaymentsList;
        }
        public List<Repayment> LoadRepayment(Credits credits)
        {
            List<Repayment> item = RepaymentsList.Where(s => s.CreditId == credits.Id).ToList();
            if (item.Count == 0)
            {
                AddRepaymentItem(credits);
                RepaymentsList = LoadRepayment();
                item = RepaymentsList.Where(s => s.CreditId == credits.Id).ToList();
            }
            return new List<Repayment>(item);
        }
        public void SaveChanged(Credits credits, decimal money)
        {
            var item = (Credits)Repository.GetAccountsId(credits.Id);
            item.NextDate = item.NextDate.AddMonths(1);
            item.RepayALoan -= money;
            item.EditCredits();
            SaveRepaymentChanged(credits, money);
        }
        private void SaveRepaymentChanged(Credits credits, decimal money)
        {
            var item = RepaymentsList.Single(s => s.PlanningDate == credits.NextDate);
            item.RepaymentDate = DateTime.Now;
            item.Amount = money;
            item.Performed = true;
            item.EditRepayment();
        }

        /// <summary>
        /// Добавление списка графика платежей
        /// </summary>
        /// <param name="credits"></param>
        private void AddRepaymentItem(Credits credits)
        {
            int idcount = RepaymentId();
            if (credits != null)
            {
                for (int i = 0; i < credits.MonthsPeriod; i++)
                {
                    DateTime date = credits.OpenDate.AddMonths(i + 1);
                    Repayment repayment = new Repayment()
                    {
                        Id = idcount,
                        CreditId = credits.Id,
                        RepaymentDate = date,
                        PlanningDate = date,
                        Performed = false,
                        Amount = credits.MonthlyPayment
                    };
                    RepaymentsList.Add(repayment);
                    repayment.AddRepayment();
                    idcount += 1;
                }
            }

        }

        /// <summary>
        /// Генератор ИД
        /// </summary>
        /// <returns></returns>
        private int RepaymentId()
        {
            int id = 0;
            if (RepaymentsList.Count != 0) id = RepaymentsList.OrderBy(s => s.Id).LastOrDefault().Id;
            return id + 1;
        }
    }
}

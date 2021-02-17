using AccountData.Model;

namespace AccountData
{
    /// <summary>
    /// Подсчет кредита и вклада
    /// </summary>
    public static class Interest
    {
        /// <summary>
        /// Подсчет вклада
        /// </summary>
        /// <param name="deposit">Модель данных Deposit</param>
        /// <returns>Возвращает сумму зароботка</returns>
        public static decimal DepositeRate(Deposit deposit)
        {
            if (deposit != null)
            {
                double month = 12;
                decimal money = deposit.DepositAmount;
                decimal rate = deposit.Rate / (decimal)month;
                if (!deposit.Capitalization) money = (money / 100) * (rate * deposit.MonthsPeriod);
                else
                {
                    for (int i = 0; i < deposit.MonthsPeriod; i++)
                    {
                        money = (money / 100) * rate + money;

                    }
                    money -= deposit.DepositAmount;
                }
                return money;
            }
            else return 0;

        }
        /// <summary>
        /// Подсчет кредита
        /// </summary>
        /// <param name="credits">Модель данных Credits</param>
        /// <returns>Возвращает сумму ежемесячного платежа</returns>
        public static decimal CreditRate(Credits credits)
        {
            if (credits != null)
            {
                double month = 12;
                int monthlycount = credits.MonthsPeriod;
                decimal monthly = credits.MonthsPeriod;
                decimal money = credits.AmountIssue / monthlycount;
                decimal rate = credits.Rate / (decimal)month;
                monthly = (credits.AmountIssue / 100) * (rate * monthlycount) / monthlycount + money;
                return monthly;
            }
            else return 0;
        }

    }
}

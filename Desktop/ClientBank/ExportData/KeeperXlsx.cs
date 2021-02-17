using AccountData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using AccountData;

namespace ExportData
{
    public class KeeperXlsx : IBookSave
    {

        private static Excel.Application excelApp = new Excel.Application();
        //public Excel.Workbook excelDocument { get; set; }
        private static int StatusId;
        private List<Accounts> GetAccounts;
        private int CountFull;
        private int Count = 0;
        public KeeperXlsx(int statusid)
        {
            StatusId = statusid;
            excelApp.SheetsInNewWorkbook = 3;
            excelApp.Workbooks.Add();
            this.GetAccounts = Repository.ListAccount.ToList().FindAll(FilterAccount);
            this.CountFull = GetAccounts.Count();
        }
        /// <summary>
        /// Фильтр данных 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool FilterAccount(Accounts arg)
        {
            if (StatusId != 1) return arg.ClientStatus == StatusId;
            else return true;
        }
        /// <summary>
        /// Сохранение данных
        /// </summary>
        public void SaveBookPages()
        {
            Task.Factory.StartNew(GetSaved);
        }
        /// <summary>
        /// Запуск процесса экспорта в таблицу
        /// </summary>
        private void GetSaved()
        {
            
            var t2 = Task.Factory.StartNew(DisplayInExcelDeposit);
            var t3 = Task.Factory.StartNew(DisplayInExcelCredit);
            Task.WaitAll(t2, t3);
            var t1 = Task.Factory.StartNew(DisplayInExcelPerson);
            Task.WaitAll(t1, t2, t3);
            excelApp.Visible = true;

            
            //excelDocument.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, "Account.pdf");

            //excelDocument.Close();
            //excelApp.Quit();

        }
        /// <summary>
        /// Экспорт обычных счетов в таблицу
        /// </summary>
        private  void DisplayInExcelPerson()
        {
            var account = GetAccounts.Where(t => t.TypeId == 2).ToList();
            List<Personal> person = new List<Personal>();

            foreach(var item in account) { person.Add((Personal)item); }

            Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.Worksheets.get_Item(1);
            worksheet.Name = "Счета";
            worksheet.Cells[1, "A"] = "№";
            worksheet.Cells[1, "B"] = "Номер счета";
            worksheet.Cells[1, "C"] = "Остаток";
            worksheet.Cells[1, "D"] = "Клиент";
            worksheet.Cells[1, "E"] = "Дата открытия";
            worksheet.Cells[1, "F"] = "Соcтояние";

            var row = 1;

            foreach (var item in person)
            {
                Count++;
                row++;
                worksheet.Cells[row, "A"] = item.Id;
                worksheet.Cells[row, "B"] = item.AccountNumber;
                worksheet.Cells[row, "C"] = item.Cash;
                worksheet.Cells[row, "D"] = item.ClientName;
                worksheet.Cells[row, "E"] = item.OpenDate;
                worksheet.Cells[row, "F"] = item.StateName;

                ProgressBar.GetValue("Экспорт Xlsx", Count, CountFull);
            }
            string date = $"{DateTime.Now}";
            string[] dates = date.Split(' ', ',', '.', ':', ';');
            string names = String.Empty;
            foreach (var mas in dates) names += String.Join(" ", mas);

            string name = $"Счета-{names}";

            worksheet.SaveAs(name);
            //excelDocument = excelApp.Workbooks.Open(name, ReadOnly: true);


        }
        /// <summary>
        /// Экспорт данных по вкладам в таблицу
        /// </summary>
        private void DisplayInExcelDeposit()
        {
            var account = GetAccounts.Where(t => t.TypeId == 3).ToList();

            List<Deposit> deposit = new List<Deposit>();

            foreach (var item in account) { deposit.Add((Deposit)item); }

            Excel._Worksheet worksheetD = (Excel.Worksheet)excelApp.Worksheets.get_Item(2);
            worksheetD.Name = "Вклады";
            worksheetD.Cells[1, "A"] = "Соcтояние";
            worksheetD.Cells[1, "B"] = "Клиент";
            worksheetD.Cells[1, "C"] = "Номер счета";
            worksheetD.Cells[1, "D"] = "Остаток";
            worksheetD.Cells[1, "E"] = "Вложено";
            worksheetD.Cells[1, "F"] = "Ставка";
            worksheetD.Cells[1, "G"] = "Капитализация";
            worksheetD.Cells[1, "H"] = "Период";
            worksheetD.Cells[1, "I"] = "Заработок";
            worksheetD.Cells[1, "J"] = "Дата открытия";
            worksheetD.Cells[1, "K"] = "Дата закрытия";

            var row = 1;

            foreach (var item in deposit)
            {
                Count++;
                row++;
                worksheetD.Cells[row, "A"] = item.StateName;
                worksheetD.Cells[row, "B"] = item.ClientName;
                worksheetD.Cells[row, "C"] = item.AccountNumber;
                worksheetD.Cells[row, "D"] = item.Cash;
                worksheetD.Cells[row, "E"] = item.DepositAmount;
                worksheetD.Cells[row, "F"] = item.Rate;
                worksheetD.Cells[row, "G"] = item.Capitalization;
                worksheetD.Cells[row, "H"] = item.MonthsPeriod;
                worksheetD.Cells[row, "I"] = item.MoneyEarned;
                worksheetD.Cells[row, "J"] = item.OpenDate.ToShortDateString();
                worksheetD.Cells[row, "K"] = item.DateClose.ToShortDateString();
                ProgressBar.GetValue("Экспорт Xlsx", Count, CountFull);
            }
        }
        /// <summary>
        /// Экспорт данных по кредитам в таблицу
        /// </summary>
        private void DisplayInExcelCredit()
        {
            var account = GetAccounts.Where(t => t.TypeId == 4).ToList();

            List<Credits> credits = new List<Credits>();

            foreach (var item in account) { credits.Add((Credits)item); }

            Excel._Worksheet worksheetС = (Excel.Worksheet)excelApp.Worksheets.get_Item(3);
            worksheetС.Name = "Кредиты";
            worksheetС.Cells[1, "A"] = "Соcтояние";
            worksheetС.Cells[1, "B"] = "Клиент";
            worksheetС.Cells[1, "C"] = "Номер счета";
            worksheetС.Cells[1, "D"] = "Остаток";
            worksheetС.Cells[1, "E"] = "Сумма кредита";
            worksheetС.Cells[1, "F"] = "К погашению";
            worksheetС.Cells[1, "G"] = "Ставка";
            worksheetС.Cells[1, "H"] = "Ежемесячный платеж";
            worksheetС.Cells[1, "I"] = "Период - месяцев";
            worksheetС.Cells[1, "J"] = "Дата открытия";
            worksheetС.Cells[1, "K"] = "Следующая дата платежа";

            var row = 1;

            foreach (var item in credits)
            {
                Count++;
                row++;
                worksheetС.Cells[row, "A"] = item.StateName;
                worksheetС.Cells[row, "B"] = item.ClientName;
                worksheetС.Cells[row, "C"] = item.AccountNumber;
                worksheetС.Cells[row, "D"] = item.Cash;
                worksheetС.Cells[row, "E"] = item.AmountIssue;
                worksheetС.Cells[row, "F"] = item.RepayALoan;
                worksheetС.Cells[row, "G"] = item.Rate;
                worksheetС.Cells[row, "H"] = item.MonthlyPayment;
                worksheetС.Cells[row, "I"] = item.MonthsPeriod;
                worksheetС.Cells[row, "J"] = item.OpenDate;
                worksheetС.Cells[row, "K"] = item.NextDate;
                ProgressBar.GetValue("Экспорт Xlsx", Count, CountFull);
            }
        }
    }
}

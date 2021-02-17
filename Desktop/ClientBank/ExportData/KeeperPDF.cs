using AccountData.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountData;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;

namespace ExportData
{
    public class KeeperPDF : IBookSave
    {

        private static int StatusId;
        private List<Accounts> GetAccounts;
        private int CountFull;
        private int Count = 0;
        private Document doc;
        private BaseFont baseFont;
        private Font font;
        private Font fonth;
        public KeeperPDF(int statusid)
        {
            StatusId = statusid;
            this.GetAccounts = Repository.ListAccount.ToList().FindAll(FilterAccount);
            this.CountFull = GetAccounts.Count();
            this.doc = new Document(PageSize.A4, 10, 10, 3, 3);
            this.baseFont = BaseFont.CreateFont("C:/Windows/Fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            this.font = new Font(baseFont, 6, Font.NORMAL);
            this.fonth = new Font(baseFont, 7, Font.BOLD);
        }

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
        private static object o = new object();
        /// <summary>
        /// Запуск процеса экспорта в фаил PDF
        /// </summary>
        private void GetSaved()
        {
            PdfWriter.GetInstance(doc, new FileStream("AccountTable.pdf", FileMode.Create));
            doc.Open();
            var t1= Task.Factory.StartNew(DisplayInPDFPerson);
            var t2 = Task.Factory.StartNew(DisplayInPDFDeposit);
            var t3 = Task.Factory.StartNew(DisplayInPDFCredit);
            Task.WaitAll(t1, t2, t3);
            doc.Close();
            doc.CloseDocument();
            doc.Dispose();
        }
        /// <summary>
        /// Экспорт обычных счетов в таблицу
        /// </summary>
        private  void DisplayInPDFPerson()
        {
            var account = GetAccounts.Where(t => t.TypeId == 2).ToList();
            List<Personal> person = new List<Personal>();

            foreach(var item in account) { person.Add((Personal)item); }
            lock (o)
            {
                
                float[] columnDefinitionSize = { 2F, 5F, 3F, 5F, 3F, 3F };
                PdfPCell cell = new PdfPCell(new Phrase("Счета", font));
                cell.Colspan = 7;
                cell.HorizontalAlignment = 1;
                cell.Border = 0;

                PdfPTable table = new PdfPTable(columnDefinitionSize) { WidthPercentage = 50 };
                table.AddCell(cell);
                table.AddCell(new Phrase("№", fonth));
                table.AddCell(new Phrase("Номер счета", fonth));
                table.AddCell(new Phrase("Остаток", fonth));
                table.AddCell(new Phrase("Клиент", fonth));
                table.AddCell(new Phrase("Дата открытия", fonth));
                table.AddCell(new Phrase("Сотояние", fonth));

                foreach (var item in person)
                {
                    Count++;
                    table.AddCell(new Phrase(item.Id.ToString(), font));
                    table.AddCell(new Phrase(item.AccountNumber.ToString(), font));
                    table.AddCell(new Phrase(item.Cash.ToString("0,0"), font));
                    table.AddCell(new Phrase(item.ClientName.ToString(), font));
                    table.AddCell(new Phrase(item.OpenDate.ToShortDateString(), font));
                    table.AddCell(new Phrase(item.StateName.ToString(), font));
                    ProgressBar.GetValue("Экспорт Pdf", Count, CountFull);
                }
                doc.Add(table);
            }
            
        }
        /// <summary>
        /// Экспорт Вкладов в таблицу
        /// </summary>
        private void DisplayInPDFDeposit()
        {
            var account = GetAccounts.Where(t => t.TypeId == 3).ToList();

            List<Deposit> deposit = new List<Deposit>();

            foreach (var item in account) { deposit.Add((Deposit)item); }
            lock (o)
            {
                float[] columnDefinitionSize = { 2F, 5F, 5F, 3F, 3F, 3F, 2F, 3F, 3F, 3F, 3F };
                PdfPCell cell = new PdfPCell(new Phrase("Вклады", font));
                cell.Colspan = 11;
                cell.HorizontalAlignment = 1;
                cell.Border = 0;

                PdfPTable tabled = new PdfPTable(columnDefinitionSize) { WidthPercentage = 100 };
                tabled.AddCell(cell);
                tabled.AddCell(new Phrase("Сотояние", fonth));
                tabled.AddCell(new Phrase("Клиент", fonth));
                tabled.AddCell(new Phrase("Номер счета", fonth));
                tabled.AddCell(new Phrase("Остаток", fonth));
                tabled.AddCell(new Phrase("Вложено", fonth));
                tabled.AddCell(new Phrase("Ставка %", fonth));
                tabled.AddCell(new Phrase("Капитализация", fonth));
                tabled.AddCell(new Phrase("Период", fonth));
                tabled.AddCell(new Phrase("Заработок", fonth));
                tabled.AddCell(new Phrase("Дата открытия", fonth));
                tabled.AddCell(new Phrase("Дата закрытия", fonth));


                foreach (var item in deposit)
                {
                    Count++;
                    tabled.AddCell(new Phrase(item.StateName.ToString(), font));
                    tabled.AddCell(new Phrase(item.ClientName.ToString(), font));
                    tabled.AddCell(new Phrase(item.AccountNumber.ToString(), font));
                    tabled.AddCell(new Phrase(item.Cash.ToString("0,0"), font));
                    tabled.AddCell(new Phrase(item.DepositAmount.ToString("0,0"), font));
                    tabled.AddCell(new Phrase(item.Rate.ToString(), font));
                    tabled.AddCell(new Phrase(item.Capitalization.ToString(), font));
                    tabled.AddCell(new Phrase(item.MonthsPeriod.ToString("0 Мес."), font));
                    tabled.AddCell(new Phrase(item.MoneyEarned.ToString("0,0"), font));
                    tabled.AddCell(new Phrase(item.OpenDate.ToShortDateString(), font));
                    tabled.AddCell(new Phrase(item.DateClose.ToShortDateString(), font));
                    ProgressBar.GetValue("Экспорт Pdf", Count, CountFull);
                }
                doc.Add(tabled);
                
            }
            

        }
        /// <summary>
        /// Экспорт кредом в таблицу
        /// </summary>
        private void DisplayInPDFCredit()
        {
            var account = GetAccounts.Where(t => t.TypeId == 4).ToList();

            List<Credits> credits = new List<Credits>();

            foreach (var item in account) { credits.Add((Credits)item); }

            lock (o)
            {
                float[] columnDefinitionSize = { 2F, 5F, 5F, 3F, 3F, 3F, 2F, 3F, 3F, 3F, 3F };
                PdfPCell cell = new PdfPCell(new Phrase("Кредиты", font));
                cell.Colspan = 11;
                cell.HorizontalAlignment = 1;
                cell.Border = 0;

                PdfPTable tablec = new PdfPTable(columnDefinitionSize) { WidthPercentage = 100 };
                tablec.AddCell(cell);
                tablec.AddCell(new Phrase("Сотояние", fonth));
                tablec.AddCell(new Phrase("Клиент", fonth));
                tablec.AddCell(new Phrase("Номер счета", fonth));
                tablec.AddCell(new Phrase("Остаток", fonth));
                tablec.AddCell(new Phrase("Сумма кредита", fonth));
                tablec.AddCell(new Phrase("К погашению", fonth));
                tablec.AddCell(new Phrase("Ставка %", fonth));
                tablec.AddCell(new Phrase("Ежемесячный платеж", fonth));
                tablec.AddCell(new Phrase("Период", fonth));
                tablec.AddCell(new Phrase("Дата открытия", fonth));
                tablec.AddCell(new Phrase("Следующая дата платежа", fonth));


                foreach (var item in credits)
                {
                    Count++;
                    tablec.AddCell(new Phrase(item.StateName.ToString(), font));
                    tablec.AddCell(new Phrase(item.ClientName.ToString(), font));
                    tablec.AddCell(new Phrase(item.AccountNumber.ToString(), font));
                    tablec.AddCell(new Phrase(item.Cash.ToString("0,0"), font));
                    tablec.AddCell(new Phrase(item.AmountIssue.ToString("0,0"), font));
                    tablec.AddCell(new Phrase(item.RepayALoan.ToString("0,0"), font));
                    tablec.AddCell(new Phrase(item.Rate.ToString(), font));
                    tablec.AddCell(new Phrase(item.MonthlyPayment.ToString(), font));
                    tablec.AddCell(new Phrase(item.MonthsPeriod.ToString("0 Мес."), font));
                    tablec.AddCell(new Phrase(item.OpenDate.ToShortDateString(), font));
                    tablec.AddCell(new Phrase(item.NextDate.ToShortDateString(), font));
                    ProgressBar.GetValue("Экспорт Pdf", Count, CountFull);
                }
                doc.Add(tablec);
            }


        }
    }
}

using AccountData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDatabase;
using System.Data.SqlClient;
using System.Windows;
using System.Diagnostics;
using AccountData.History;
using AccountData.Credit;

namespace AccountData.DataWork
{
    internal static class AddData
    {
        static ErrorMes errorMes;
        static AddData() { errorMes = new ErrorMes(); }

        internal static void Adds()
        {
            foreach (var item in CreditRepository.RepaymentsList)
            {
                DateTime date = DateTime.MinValue;
                if (item.RepaymentDate <= date) item.RepaymentDate = item.PlanningDate;
               // AddRepayment(item);
            }
            Debug.WriteLine("Добавление готово....");
        }
       
        /// <summary>
        /// Добавление обычного счета
        /// </summary>
        /// <param name="item">Модель данных Personal</param>
        internal static void AddPersonal(this Personal item)
        {
            try
            {
                string sql = "INSERT INTO Personal (Id, AccountNumber, Cash, ClientId, OpenDate, State, TypeId)" +
                " VALUES (@Id, @AccountNumber, @Cash, @ClientId, @OpenDate, @State, @TypeId)";
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@AccountNumber", item.AccountNumber);
                    cmd.Parameters.AddWithValue("@Cash", item.Cash);
                    cmd.Parameters.AddWithValue("@ClientId", item.ClientId);
                    cmd.Parameters.AddWithValue("@OpenDate", item.OpenDate);
                    cmd.Parameters.AddWithValue("@State", item.State);
                    cmd.Parameters.AddWithValue("@TypeId", item.TypeId);
      
                    cmd.ExecuteNonQuery();

                    ConnectionClient.ConClose();
                }
            }
            catch(SqlException e)
            {
                errorMes.ErrorSQL("Personal new " + e.Message); ConnectionClient.ConClose();
            }
            catch(Exception e)
            {
                errorMes.ErrorSQL("Personal new " + e.Message); ConnectionClient.ConClose();
            }
            
        }
        /// <summary>
        /// Добавление нового кредита
        /// </summary>
        /// <param name="item">Модель данных Credits</param>
        internal static void AddCredits(this Credits item)
        {
            
            try
            {
                string sql = "INSERT INTO Credits (Id, AccountNumber, Cash, RepayALoan, AmountIssue, ClientId, Rate, MonthlyPayment, State, MonthsPeriod, OpenDate, NextDate, TypeId)" +
                " VALUES (@Id, @AccountNumber, @Cash, @RepayALoan, @AmountIssue, @ClientId, @Rate, @MonthlyPayment, @State, @MonthsPeriod, @OpenDate, @NextDate, @TypeId)";
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@AccountNumber", item.AccountNumber);
                    cmd.Parameters.AddWithValue("@Cash", item.Cash);
                    cmd.Parameters.AddWithValue("@RepayALoan", item.RepayALoan);
                    cmd.Parameters.AddWithValue("@AmountIssue", item.AmountIssue);
                    cmd.Parameters.AddWithValue("@ClientId", item.ClientId);
                    cmd.Parameters.AddWithValue("@Rate", item.Rate);
                    cmd.Parameters.AddWithValue("@MonthlyPayment", item.MonthlyPayment);
                    cmd.Parameters.AddWithValue("@State", item.State);
                    cmd.Parameters.AddWithValue("@MonthsPeriod", item.MonthsPeriod);
                    cmd.Parameters.AddWithValue("@OpenDate", item.OpenDate);
                    cmd.Parameters.AddWithValue("@NextDate", item.NextDate);
                    cmd.Parameters.AddWithValue("@TypeId", item.TypeId);
                    cmd.ExecuteNonQuery();


                    ConnectionClient.ConClose();
                }
            }
            catch (SqlException e)
            {
                errorMes.ErrorSQL("Credit new" + e.Message); ConnectionClient.ConClose();
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
        }
        /// <summary>
        /// Добавление нового вклада
        /// </summary>
        /// <param name="item">Модель данных Deposit</param>
        internal static void AddDeposit(this Deposit item)
        {
            
            try
            {
                string sql = "INSERT INTO Deposit (Id, AccountNumber, Cash, DepositAmount, ClientId, State, MonthsPeriod, Rate, OpenDate, DateClose, Capitalization, TypeId, MoneyEarned)" +
               " VALUES (@Id, @AccountNumber, @Cash, @DepositAmount, @ClientId, @State, @MonthsPeriod, @Rate, @OpenDate, @DateClose, @Capitalization, @TypeId, @MoneyEarned)";
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@AccountNumber", item.AccountNumber);
                    cmd.Parameters.AddWithValue("@Cash", item.Cash);
                    cmd.Parameters.AddWithValue("@DepositAmount", item.DepositAmount);
                    cmd.Parameters.AddWithValue("@ClientId", item.ClientId);
                    cmd.Parameters.AddWithValue("@State", item.State);
                    cmd.Parameters.AddWithValue("@MonthsPeriod", item.MonthsPeriod);
                    cmd.Parameters.AddWithValue("@Rate", item.Rate);
                    cmd.Parameters.AddWithValue("@OpenDate", item.OpenDate);
                    cmd.Parameters.AddWithValue("@DateClose", item.DateClose);
                    cmd.Parameters.AddWithValue("@Capitalization", item.Capitalization);
                    cmd.Parameters.AddWithValue("@TypeId", item.TypeId);
                    cmd.Parameters.AddWithValue("@MoneyEarned", item.MoneyEarned);
                    cmd.ExecuteNonQuery();

                }
                ConnectionClient.ConClose();

            }
            catch (SqlException e)
            {
                errorMes.ErrorSQL("Deposit new " + e.Message); ConnectionClient.ConClose();
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
        }
        /// <summary>
        /// Добавление истории
        /// </summary>
        /// <param name="item"></param>
        internal static void AddHistory(this OperationHistory item)
        {
            
            try
            {
                string sql = "INSERT INTO OperationHistory ( OperationId, Money, OperationDate, AccountNumber)" +
              " VALUES ( @OperationId, @Money, @OperationDate, @AccountNumber)";
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {

                    cmd.Parameters.AddWithValue("@OperationId", item.OperationId);
                    cmd.Parameters.AddWithValue("@Money", item.Money);
                    cmd.Parameters.AddWithValue("@OperationDate", item.OperationDate);
                    cmd.Parameters.AddWithValue("@AccountNumber", item.AccountNumber);

                    cmd.ExecuteNonQuery();

                    ConnectionClient.ConClose();
                }
            }
            catch (SqlException e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
        }
        internal static void AddRepayment(this Repayment item)
        {
            
            try
            {
                string sql = "INSERT INTO Repayment (Id, CreditId, RepaymentDate, PlanningDate, Performed, Amount)" +
              " VALUES ( @Id, @CreditId, @RepaymentDate, @PlanningDate, @Performed, @Amount)";
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@CreditId", item.CreditId);
                    cmd.Parameters.AddWithValue("@RepaymentDate", item.RepaymentDate);
                    cmd.Parameters.AddWithValue("@PlanningDate", item.PlanningDate);
                    cmd.Parameters.AddWithValue("@Performed", item.Performed);
                    cmd.Parameters.AddWithValue("@Amount", item.Amount);
                    cmd.ExecuteNonQuery();

                    ConnectionClient.ConClose();
                }
            }
            catch (SqlException e)
            {
                errorMes.ErrorSQL(e.Message);
                ConnectionClient.ConClose();
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message);
                ConnectionClient.ConClose();
            }
        }
    }
}

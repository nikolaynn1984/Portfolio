using AccountData.Model;
using StoreDatabase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccountData.DataWork
{
    internal static class EditData
    {
        static ErrorMes errorMes;
        static EditData() { errorMes = new ErrorMes(); }
        /// <summary>
        /// Обновление данных 
        /// </summary>
        /// <param name="item"></param>
        internal static void EditRepayment(this Repayment item)
        {
            try
            {
                string sql = "UPDATE Repayment SET CreditId = @CreditId , RepaymentDate = @RepaymentDate, PlanningDate = @PlanningDate, Performed = @Performed, Amount = @Amount WHERE Id = @Id";

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
            }catch(Exception e)
            {
                errorMes.ErrorSQL(e.Message);
            }
            finally
            {
                ConnectionClient.ConClose();
            }

        }
        /// <summary>
        /// Обновление данных по кредитам
        /// </summary>
        /// <param name="item">Обновленная модель данных</param>
        internal static void EditCredits(this Credits item)
        {
            try
            {
                string sql = "UPDATE Credits SET    RepayALoan = @RepayALoan, Cash = @Cash, NextDate = @NextDate WHERE Id = @Id";
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {

                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Cash", item.Cash);
                        cmd.Parameters.AddWithValue("@RepayALoan", item.RepayALoan);
                        cmd.Parameters.AddWithValue("@NextDate", item.NextDate);
                        cmd.ExecuteNonQuery();

                    ConnectionClient.ConClose();

                }
            }
            catch (SqlException e)
            {
                errorMes.ErrorSQL(e.Message);
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message);
            }
            finally
            {
                ConnectionClient.ConClose();
            }
           
        }
        
       
        /// <summary>
        /// Обновление данных по вкладам
        /// </summary>
        /// <param name="item">Обновленная модель данных</param>
        internal static void EditDeposit(this Deposit item)
        {
           
            try
            {
                string sql = "UPDATE Deposit SET DepositAmount = @DepositAmount, Cash = @Cash, MoneyEarned = @MoneyEarned,   DateClose = @DateClose  WHERE Id = @Id";
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@DepositAmount", item.DepositAmount);
                    cmd.Parameters.AddWithValue("@Cash", item.Cash);
                    cmd.Parameters.AddWithValue("@MoneyEarned", item.MoneyEarned);
                    cmd.Parameters.AddWithValue("@DateClose", item.DateClose);
                    cmd.ExecuteNonQuery();


                    ConnectionClient.ConClose();
                }
            }
            catch (SqlException e)
            {
                errorMes.ErrorSQL(e.Message);
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message);
            }
            finally
            {
                ConnectionClient.ConClose();
            }
        }

        /// <summary>
        /// Обновление данных по счетам
        /// </summary>
        /// <param name="item">Обновленная модель данных</param>
        internal static void EditPersonal(this Personal item)
        {

            try
            {
                string sql = "UPDATE Personal SET  Cash = @Cash   WHERE Id = @Id";
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {

                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Cash", item.Cash);
                    cmd.ExecuteNonQuery();


                    ConnectionClient.ConClose();
                }
            }
            catch (SqlException e)
            {
                errorMes.ErrorSQL(e.Message);
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message);
            }
            finally
            {
                ConnectionClient.ConClose();
            }
        }
    }
}

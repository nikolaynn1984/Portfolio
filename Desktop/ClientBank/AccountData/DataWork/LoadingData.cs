using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AccountData.Credit;
using AccountData.History;
using AccountData.Model;
using StoreDatabase;

namespace AccountData.DataWork
{
    internal static class LoadingData
    {
        static ErrorMes errorMes;
        static LoadingData() { errorMes = new ErrorMes(); }
        private static List<Accounts> accounts = new List<Accounts>();
        private static int countAccount = 0;
        internal static void LoadData()
        {
            LoadType();
            
        }       
        static object o = new object();
        /// <summary>
        /// Азинхронная загрузка счетов
        /// </summary>
        internal static void LoadAccount()
        {
            var t2 = Task.Factory.StartNew(LoadCredit);
            var t3 = Task.Factory.StartNew(LoadDeposit);
            var t4 = Task.Factory.StartNew(LoadPersonal);
            Task.WaitAll(t2, t3, t4);
            var t1 = Task.Factory.StartNew(LoadAccounts);
        }
        /// <summary>
        /// Загрузка дынных AccountType
        /// </summary>
        private static void LoadType()
        {
            try
            {
                string sql = "SELECT * FROM AccountType";
                ConnectionClient.ConOpen();
                using (SqlCommand commmand = new SqlCommand(sql, ConnectionClient.connection))
                {
                        SqlDataReader reader = commmand.ExecuteReader();
                        while (reader.Read())
                        {
                            AccountType type = new AccountType((int)reader["Id"], (string)reader["Types"]);
                            Repository.GetTypes.Add(type);
                        }
                   
                }
                ConnectionClient.ConClose();
            }
            catch(SqlException e)
            {

                errorMes.ErrorSQL(e.Message);
            }
            catch(Exception e)
            {

                errorMes.ErrorSQL(e.Message);
            }
            finally
            {
                ConnectionClient.ConClose();
            }

        }
        /// <summary>
        /// Загрузка данных по всем счетам
        /// </summary>
        private static void LoadAccounts()
        {
            try
            {
                
                List<Accounts> list = null;
                int con = 0;
                int itemcount = countAccount;
                lock (o)
                {
                    Debug.WriteLine("Поток Bank");
                   
                            int count = 0;
                    accounts.Sort();

                            foreach (var item in accounts)
                            {

                                con++;
                                if (list == null) list = new List<Accounts>(100);
                                list.Add(item);
                                count++;

                                ProgressBar.GetValue("Загрузка", itemcount, count);
                                if (con % 100 == 0 || con == itemcount)
                                {

                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        list.ForEach(acc => Repository.ListAccount.Add(acc));
                                    });
                                    list = null;
                                   ProgressBar.GetValue("Загрузка", count, countAccount);
                                }
                            }
                        Debug.WriteLine("Поток Bank звершился"); 
                }
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message);
            }

        }
        /// <summary>
        /// Загрузка данных по всем вкладам
        /// </summary>
        private static void LoadDeposit()
        {
            try
            {
                string sql = "SELECT * FROM Deposit";
                int itemcount = 0;
                lock (o)
                {
                    ConnectionClient.ConOpen();
                    using (SqlCommand commmand = new SqlCommand(sql, ConnectionClient.connection))
                    {

                            SqlDataReader reader1 = commmand.ExecuteReader();
                            itemcount = reader1.Cast<object>().Count();
                            reader1.Close();
                            int count = 0;
                            SqlDataReader reader = commmand.ExecuteReader();
                            while (reader.Read())
                            {
                                 Deposit deposit = new Deposit((int)reader["Id"], (long)reader["AccountNumber"], (decimal)reader["Cash"], (decimal)reader["DepositAmount"], (int)reader["ClientId"],
                                                          (bool)reader["State"], (int)reader["MonthsPeriod"], (int)reader["Rate"], (DateTime)reader["OpenDate"],
                                                          (DateTime)reader["DateClose"], (bool)reader["Capitalization"], (int)reader["TypeId"], (decimal)reader["MoneyEarned"]);
                          //  Repository.GetDeposits.Add(deposit);
                                 accounts.Add(AccountFactory.GetAccount(deposit));
                                count++;
                                countAccount++;

                                ProgressBar.GetValue("Загрузка вкладов", itemcount, count);

                            }

                        
                    }
                    ConnectionClient.ConClose();

                }
            }
            catch(SqlException e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
            finally { ConnectionClient.ConClose(); }

        }
        /// <summary>
        /// Загрузка данных по обычным счетам
        /// </summary>
        private static void LoadPersonal()
        {
            try
            {
                string sql = "SELECT * FROM Personal";
                int itemcount = 0;
                lock (o)
                {
                    ConnectionClient.ConOpen();
                    using (SqlCommand commmand = new SqlCommand(sql, ConnectionClient.connection))
                    {

                       SqlDataReader reader1 = commmand.ExecuteReader();
                       itemcount = reader1.Cast<object>().Count();
                       reader1.Close();
                       int count = 0;
                       SqlDataReader reader = commmand.ExecuteReader();
                       while (reader.Read())
                       {
                            Personal personal = new Personal((int)reader["Id"], (long)reader["AccountNumber"], (decimal)reader["Cash"], 
                                    (int)reader["ClientId"], (DateTime)reader["OpenDate"], (bool)reader["State"], (int)reader["TypeId"]);
                            Repository.GetPersonals.Add(personal);
                            accounts.Add(AccountFactory.GetAccount(personal));
                            count++;
                            countAccount++;
                            ProgressBar.GetValue("Загрузка счетов", itemcount, count);
                       }

                        
                    }
                    ConnectionClient.ConClose();
                }
            }
            catch(SqlException e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
            finally { ConnectionClient.ConClose(); }


        }
        /// <summary>
        /// Загрузка данных по всем кредитам
        /// </summary>
        private static void LoadCredit()
        {
            try
            {
                string sql = "SELECT * FROM Credits";
                int itemcount = 0;
                Debug.WriteLine("Поток Крдиты");
                lock (o)
                {
                    ConnectionClient.ConOpen();
                    using (SqlCommand commmand = new SqlCommand(sql, ConnectionClient.connection))
                    {

                       SqlDataReader reader1 = commmand.ExecuteReader();
                       itemcount = reader1.Cast<object>().Count();
                       reader1.Close();
                       int count = 0;
                       SqlDataReader reader = commmand.ExecuteReader();
                       while (reader.Read())
                       {
                            Credits credits = new Credits((int)reader["Id"], (long)reader["AccountNumber"], (decimal)reader["Cash"], (decimal)reader["RepayALoan"],
                                                         (decimal)reader["AmountIssue"], (int)reader["ClientId"], (int)reader["Rate"], (decimal)reader["MonthlyPayment"], (bool)reader["State"],
                                                         (int)reader["MonthsPeriod"], (DateTime)reader["OpenDate"], (DateTime)reader["NextDate"], (int)reader["TypeId"]);
                           // Repository.GetCredits.Add(credits);
                            accounts.Add(AccountFactory.GetAccount(credits));
                            count++;
                            countAccount++;
                                ProgressBar.GetValue("Загрузка счетов", itemcount, count);
                       }
                    }
                    ConnectionClient.ConClose();
                }
            }
            catch(SqlException e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
            finally { ConnectionClient.ConClose(); }
           
            
        }
        /// <summary>
       /// Загрузка истории
       /// </summary>
        internal static void LoadHistory()
        {
            try
            {
                string sql = "SELECT * FROM OperationHistory";
                lock (o)
                {
                    ConnectionClient.ConOpen();
                    using (SqlCommand commmand = new SqlCommand(sql, ConnectionClient.connection))
                    {

                            SqlDataReader reader = commmand.ExecuteReader();
                            while (reader.Read())
                            {
                                OperationHistory oper = new OperationHistory((int)reader["Id"], (int)reader["OperationId"], (decimal)reader["Money"], (DateTime)reader["OperationDate"], (long)reader["AccountNumber"]);
                                HIstoryRepository.OperationHistories.Add(oper);
                            }

                    }
                    ConnectionClient.ConClose();
                }
            }
            catch(SqlException e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
            finally { ConnectionClient.ConClose(); }


        }
        /// <summary>
        /// Загрузка списка операций
        /// </summary>
        internal static void LoadOperation()
        {
            try
            {
                string sql = "SELECT * FROM Operations";
                lock (o)
                {
                    ConnectionClient.ConOpen();
                    using (SqlCommand commmand = new SqlCommand(sql, ConnectionClient.connection))
                    {

                            SqlDataReader reader = commmand.ExecuteReader();
                            while (reader.Read())
                            {
                                Operations oper = new Operations((int)reader["Id"], (string)reader["OperationName"]);
                                HIstoryRepository.OperationsList.Add(oper);
                            }

                    }
                    ConnectionClient.ConClose();
                }
            }
            catch(SqlException e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message); ConnectionClient.ConClose();
            }
            finally { ConnectionClient.ConClose(); }
            
           
        }
        internal static void LoadRepayment()
        {
            try
            {
                string sql = "SELECT * FROM Repayment";
                lock (o)
                {
                    ConnectionClient.ConOpen();
                    using (SqlCommand commmand = new SqlCommand(sql, ConnectionClient.connection))
                    {

                            SqlDataReader reader = commmand.ExecuteReader();
                            while (reader.Read())
                            {
                                Repayment repay = new Repayment((int)reader["Id"], (int)reader["CreditId"], (DateTime)reader["RepaymentDate"], (DateTime)reader["PlanningDate"],
                                    (bool)reader["Performed"], (decimal)reader["Amount"]);
                                CreditRepository.RepaymentsList.Add(repay);
                            }

                    }
                    ConnectionClient.ConClose();
                }
            }
            catch(SqlException e)
            {
                errorMes.ErrorSQL(e.Message);
            }
            catch (Exception e)
            {
                errorMes.ErrorSQL(e.Message);
            }
            finally { ConnectionClient.ConClose(); }
           
        }
    }
}

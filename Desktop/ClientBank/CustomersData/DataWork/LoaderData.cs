using CustomersData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDatabase;
using System.Data.SqlClient;

namespace CustomersData.DataWork
{
    internal static class LoaderData
    {

        static ErrorMes errorMes;
        private static List<Customers> customersLoad = new List<Customers>();
        static LoaderData()
        {
            errorMes = new ErrorMes();
        }
        /// <summary>
        /// Загрузчик данных
        /// </summary>
        internal static void Run()
        {
            DataCities();
            DataStatuses();
            DataPhysicals();
            DataBusines();
            DataCustoms();
        }

        /// <summary>
        /// Загрузка списка городов
        /// </summary>
        private static void DataCities()
        {
            try
            {
                string sql = "SELECT * FROM Cities";
                ConnectionClient.ConOpen();
                using (SqlCommand commmand = new SqlCommand(sql, ConnectionClient.connection))
                {

                        SqlDataReader reader = commmand.ExecuteReader();
                        while (reader.Read())
                        {
                            Cities cities = new Cities((int)reader["Id"], (string)reader["City"]);
                            Repository.Citiess.Add(cities);
                        }

                    ConnectionClient.ConClose();
                }
            }
            catch(SqlException e) { errorMes.ErrorSQL(e.Message);  }
            catch(Exception e)  {  errorMes.ErrorSQL(e.Message);  }
            finally { ConnectionClient.ConClose(); }
           
        }
        /// <summary>
        /// Загрузка списка статусов
        /// </summary>
        private static void DataStatuses()
        {
            
            try
            {
                string sql = "SELECT * FROM Statuses";
                ConnectionClient.ConOpen();
                using (SqlCommand commmand = new SqlCommand(sql, ConnectionClient.connection))
                {

                        SqlDataReader reader = commmand.ExecuteReader();
                        while (reader.Read())
                        {
                            Statuses statuses = new Statuses((int)reader["Id"], (int)reader["DepositRate"], (string)reader["Status"], (int)reader["CreditRate"]);
                            Repository.statuses.Add(statuses);
                        }

                    ConnectionClient.ConClose();
                }
            }
            catch (SqlException e) { errorMes.ErrorSQL(e.Message); }
            catch (Exception e) { errorMes.ErrorSQL(e.Message); }
            finally { ConnectionClient.ConClose(); }
        }
        /// <summary>
        /// Загрузка списка физическх лиц
        /// </summary>
        private static void DataPhysicals()
        {
            
            try
            {
                string sql = "SELECT * FROM PhysicalClient";
                ConnectionClient.ConOpen();
                using (SqlCommand commmand = new SqlCommand(sql, ConnectionClient.connection))
                {


                        SqlDataReader reader = commmand.ExecuteReader();
                        while (reader.Read())
                        {
                            PhysicalClient physical = new PhysicalClient((int)reader["Id"], (string)reader["Name"], (string)reader["Surname"], (DateTime)reader["DateOfBirthday"], (int)reader["CityId"], (int)reader["StatusId"]);
                            Repository.physicalClients.Add(physical);
                        customersLoad.Add(CustomFactory.GetCustomerFactory(physical));
                        }

                    ConnectionClient.ConClose();
                }
            }
            catch (SqlException e) { errorMes.ErrorSQL(e.Message); }
            catch (Exception e) { errorMes.ErrorSQL(e.Message); }
            finally { ConnectionClient.ConClose(); }
        }
        /// <summary>
        /// Загрузка списка ую.лиц
        /// </summary>
        private static void DataBusines()
        {
            
            try
            {
                string sql = "SELECT * FROM Busines";
                ConnectionClient.ConOpen();
                using (SqlCommand commmand = new SqlCommand(sql, ConnectionClient.connection))
                {

                        SqlDataReader reader = commmand.ExecuteReader();
                        while (reader.Read())
                        {
                            Busines busines = new Busines((int)reader["Id"], (string)reader["OrganizationName"],
                                (long)reader["INNCode"], (int)reader["StatusId"], (int)reader["CityId"], (string)reader["FIO"], (string)reader["PositionName"]);
                            Repository.busines.Add(busines);
                        customersLoad.Add(CustomFactory.GetCustomerFactory(busines));

                        }

                    ConnectionClient.ConClose();
                }
            }
            catch (SqlException e) { errorMes.ErrorSQL(e.Message); }
            catch (Exception e) { errorMes.ErrorSQL(e.Message); }
            finally { ConnectionClient.ConClose(); }
        }
        /// <summary>
        /// Загрурка списка связей клиентов
        /// </summary>
        private static void DataCustoms()
        {
            
            try
            {
                customersLoad.Sort();
                foreach(var item in customersLoad)
                {
                    Repository.CustomersList.Add(item);
                }

            }
            catch (Exception e) { errorMes.ErrorSQL(e.Message); }
        }
        
    }
}

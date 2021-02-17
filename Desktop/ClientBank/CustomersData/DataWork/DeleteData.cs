using CustomersData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDatabase;
using System.Windows;

namespace CustomersData.DataWork
{
    internal static class DeleteData
    {
        static ErrorMes errorMes;
        static DeleteData() { errorMes = new ErrorMes(); }
        
        /// <summary>
        /// Удалить запись юр.лица
        /// </summary>
        /// <param name="Id">ИД Клиента</param>
        internal static void DeleteBusines(int Id)
        {
           
            try
            {
                string sql = string.Format($"DELETE FROM Busines WHERE Id = { Id}");
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {

                        cmd.ExecuteNonQuery();

                    ConnectionClient.ConClose();
                }
            }
            catch (SqlException e) { errorMes.ErrorSQL(e.Message); }
            catch (Exception e) { errorMes.ErrorSQL(e.Message); }
            finally { ConnectionClient.ConClose(); }
        }
        /// <summary>
        /// Удалить запись физ.лиза
        /// </summary>
        /// <param name="Id">ИД Клиента</param>
        internal static void DeletePhysical(int Id)
        {
           
            try
            {
                string sql = string.Format($"DELETE FROM PhysicalClient WHERE Id = { Id}");
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {

                        cmd.ExecuteNonQuery();

                    ConnectionClient.ConClose();
                }
            }
            catch (SqlException e) { errorMes.ErrorSQL(e.Message); }
            catch (Exception e) { errorMes.ErrorSQL(e.Message); }
            finally { ConnectionClient.ConClose(); }
        }
    }
}

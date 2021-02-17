using CustomersData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDatabase;
using System.Data.SqlClient;
using System.Windows;

namespace CustomersData.DataWork
{
    internal static class AddData
    {
        static ErrorMes errorMes;
        static AddData() { errorMes = new ErrorMes(); }
        
        /// <summary>
        /// Добавление новой записи Busines в базу 
        /// </summary>
        /// <param name="item">Модель данных</param>
        internal static void AddBusines(Busines item)
        {
            
            try
            {
                string sql = "INSERT INTO Busines (Id, OrganizationName, INNCode, StatusId, CityId, FIO, PositionName)" +
                " VALUES (@Id, @OrganizationName, @INNCode, @StatusId, @CityId, @FIO, @PositionName)";
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {

                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@OrganizationName", item.OrganizationName);
                        cmd.Parameters.AddWithValue("@INNCode", item.INNCode);
                        cmd.Parameters.AddWithValue("@StatusId", item.StatusId);
                        cmd.Parameters.AddWithValue("@CityId", item.CityId);
                        cmd.Parameters.AddWithValue("@FIO", item.FIO);
                        cmd.Parameters.AddWithValue("@PositionName", item.PositionName);
                        cmd.ExecuteNonQuery();

                        ConnectionClient.connection.Close();
                }
            }
            catch (SqlException e) { errorMes.ErrorSQL(e.Message); }
            catch (Exception e) { errorMes.ErrorSQL(e.Message); }
            finally { ConnectionClient.ConClose(); }
        }
        /// <summary>
        /// Добавление новой записи PhysicalClient в базу 
        /// </summary>
        /// <param name="item">Модель данных</param>
        internal static void AddPhysical(PhysicalClient item)
        {
            
            try
            {
                string sql = "INSERT INTO PhysicalClient (Id, Name, Surname, DateOfBirthday, CityId, StatusId) " +
                "VALUES (@Id, @Name, @Surname, @DateOfBirthday, @CityId, @StatusId)";
                ConnectionClient.ConOpen();
                using (SqlCommand cmd = new SqlCommand(sql, ConnectionClient.connection))
                {

                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@Surname", item.Surname);
                        cmd.Parameters.AddWithValue("@DateOfBirthday", item.DateOfBirthday);
                        cmd.Parameters.AddWithValue("@CityId", item.CityId);
                        cmd.Parameters.AddWithValue("@StatusId", item.StatusId);
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

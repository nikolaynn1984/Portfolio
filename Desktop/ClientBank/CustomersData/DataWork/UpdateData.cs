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
    internal static class UpdateData
    {
        static ErrorMes errorMes;
        static UpdateData() { errorMes = new ErrorMes(); }
       
        /// <summary>
        /// Обновление данных Busines
        /// </summary>
        /// <param name="item">Модель обновленных данных</param>
        internal static void UpdateBusines(Busines item)
        {
            
            try
            {
                string sql = string.Format($"UPDATE Busines SET StatusId = @StatusId, OrganizationName = @OrganizationName, INNCode = @INNCode, CityId = @CityId, FIO = @FIO, PositionName = @PositionName WHERE Id = @Id");
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

                    ConnectionClient.ConClose();


                }
            }
            catch (SqlException e) { errorMes.ErrorSQL(e.Message); }
            catch (Exception e) { errorMes.ErrorSQL(e.Message); }
            finally { ConnectionClient.ConClose(); }
        }
        /// <summary>
        /// Обновление данных PhysicalClient
        /// </summary>
        /// <param name="item">Модель обновленных данных</param>
        internal static void UpdatePhysical(PhysicalClient item)
        {
            
            try
            {
                string sql = string.Format($"UPDATE PhysicalClient SET StatusId = @StatusId, Name = @Name, Surname = @Surname,  CityId = @CityId, DateOfBirthday = @DateOfBirthday WHERE Id = @Id");
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

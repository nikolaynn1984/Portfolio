using Landing.Model.Data;
using Landing.Model.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Landing.Server.Database.Query
{
    public class BaseStatus
    {
        private string connectionString;
        public BaseStatus(string con)
        {
            this.connectionString = con;
        }

        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Список</returns>
        public async Task<List<RequisitionStatus>> GetItems()
        {
            List<RequisitionStatus> list = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "SELECT * FROM ApplicationStatus";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        RequisitionStatus item = new RequisitionStatus();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Description = (string)reader["Description"];

                        list.Add(item);
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка списка прошла успешно", "BaseStatus/GetItems");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки {ex.Message}", "BaseStatus/GetItems", ex);
                return null;
            }
            return list;
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<RequisitionStatus> GetItemById(int id)
        {
            RequisitionStatus item = new RequisitionStatus();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM ApplicationStatus WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        item = new RequisitionStatus();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Description = (string)reader["Description"];

                    }

                }
                await con.CloseAsync();

                Log.Info("Получение записи по идентификатору прошла успешно", "BaseStatus/GetItemById");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка получения записи по Id {ex.Message}", "BaseStatus/GetItemById", ex);
                return null;
            }
            return item;
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="model">Объект RequisitionStatus </param>
        /// <returns>Объект RequisitionStatus с заданым идентификатором</returns>
        public async Task<RequisitionStatus> Add(RequisitionStatus model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "INSERT INTO ApplicationStatus ( Name, Description )" +
                             " VALUES ( @Name, @Description)";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT @@IDENTITY";
                    int lastId = Convert.ToInt32(cmd.ExecuteScalar());
                    model.Id = lastId;
                }
                await con.CloseAsync();

                Log.Info("Добавление записи прошло успешно", "BaseStatus/Add");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка добавлении записи {ex.Message}", "BaseStatus/Add", ex);
                return null;
            }

            return model;
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект RequisitionStatus с обновлеными даными</param>
        /// <returns>RequisitionStatus если редактирование прошло успешно, в противном случа null</returns>
        public async Task<RequisitionStatus> Edit(RequisitionStatus model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "UPDATE ApplicationStatus SET Name = @Name , Description = @Description  WHERE Id = @Id";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Редактирование записи прошло успешно", "BaseStatus/Edit");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка редактирования записи - {ex.Message}", "BaseStatus/Edit", ex);
                return null;
            }
            return model;
        }
        /// <summary>
        /// Удаление записи 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>true - если редактирование прошло успешно, в противном случае false</returns>
        public async Task<bool> Remove(int id)
        {
            bool result = true;
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"DELETE from ApplicationStatus WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Удаление записи прошло успешно", "BaseStatus/Remove");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка удаления записи - {ex.Message}", "BaseStatus/Edit", ex);
                result = false;
            }
            return result;
        }
    }
}

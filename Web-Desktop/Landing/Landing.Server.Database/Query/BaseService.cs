using Landing.Model.Data;
using Landing.Model.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Landing.Server.Database.Query
{
    public class BaseService
    {
        private string connectionString;
        public BaseService(string con)
        {
            this.connectionString = con;
        }

        /// <summary>
        /// Получить все записи услун
        /// </summary>
        /// <returns>Список</returns>
        public async Task<List<Services>> GetItems()
        {
            List<Services> list = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "SELECT * FROM Services";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Services item = new Services();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Description = (string)reader["Description"];

                        list.Add(item);
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка списка услуг прошла успешно", "BaseService/GetItems");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки услуг {ex.Message}", "BaseService/GetItems", ex);
                return null;
            }
            return list;
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<Services> GetItemById(int id)
        {
            Services item = new Services();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM Services WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        item = new Services();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Description = (string)reader["Description"];

                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка услуги по идентификатору прошла успешно", "BaseService/GetItemById");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка получения услуги по Id {ex.Message}", "BaseService/GetItemById", ex);
                return null;
            }
            return item;
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="model">Объект Services </param>
        /// <returns>Объект Services с заданым идентификатором</returns>
        public async Task<Services> Add(Services model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "INSERT INTO Services ( Name, Description)" +
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

                Log.Info("Добавление услуги прошло успешно", "BaseService/Add");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка при добавлении услуги {ex.Message}", "BaseService/Add", ex);
                return null;
            }

            return model;
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект Services с обновлеными даными</param>
        /// <returns>Services если редактирование прошло успешно, в противном случа null</returns>
        public async Task<Services> Edit(Services model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "UPDATE Services SET Name = @Name , Description = @Description WHERE Id = @Id";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Редактирование услуги прошло успешно", "BaseService/Edit");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка редактирования записи - {ex.Message}", "BaseService/Edit", ex);
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
                string sql = $"DELETE from Services WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Удаление услуги прошло успешно", "BaseService/Remove");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка удаления записи - {ex.Message}", "BaseService/Edit", ex);
                result = false;
            }
            return result;
        }
    }
}

using Landing.Library.Model;
using Landing.Library.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Landing.Server.Database.Query
{
    public class BaseSocials
    {
        private string connectionString;
        public BaseSocials(string con)
        {
            this.connectionString = con;
        }
        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Список</returns>
        public async Task<List<Social>> GetItems()
        {
            List<Social> list = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "SELECT * FROM Social";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Social item = new Social();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Link = (string)reader["Link"];
                        item.ImageId = (int)reader["ImageId"];

                        list.Add(item);
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка списка прошла успешно", "BaseSocials/GetItems");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки {ex.Message}", "BaseSocials/GetItems", ex);
                return null;
            }
            return list;
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<Social> GetItemById(int id)
        {
            Social item = new Social();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM Social WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        item = new Social();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Link = (string)reader["Link"];
                        item.ImageId = (int)reader["ImageId"];

                    }

                }
                await con.CloseAsync();

                Log.Info("Получение записи по идентификатору прошло успешно", "BaseSocials/GetItemById");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка получения записи по Id {ex.Message}", "BaseSocials/GetItemById", ex);
                return null;
            }
            return item;
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="model">Объект Social </param>
        /// <returns>Объект Social с заданым идентификатором</returns>
        public async Task<Social> Add(Social model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "INSERT INTO Social ( Name, Link,ImageId )" +
                             " VALUES ( @Name, @Link, @ImageId)";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Link", model.Link);
                    cmd.Parameters.AddWithValue("@ImageId", model.ImageId);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT @@IDENTITY";
                    int lastId = Convert.ToInt32(cmd.ExecuteScalar());
                    model.Id = lastId;
                }
                await con.CloseAsync();

                Log.Info("Добавление записи прошло успешно", "BaseSocials/Add");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка добавлении записи {ex.Message}", "BaseSocials/Add", ex);
                return null;
            }

            return model;
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект Social с обновлеными даными</param>
        /// <returns>Social если редактирование прошло успешно, в противном случа null</returns>
        public async Task<Social> Edit(Social model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "UPDATE Social SET Name = @Name , Link = @Link, ImageId = @ImageId  WHERE Id = @Id";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Link", model.Link);
                    cmd.Parameters.AddWithValue("@ImageId", model.ImageId);
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Редактирование записи прошло успешно", "BaseService/Edit");
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
                string sql = $"DELETE from Social WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Удаление записи прошло успешно", "BaseSocials/Remove");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка удаления записи - {ex.Message}", "BaseSocials/Edit", ex);
                result = false;
            }
            return result;
        }

    }
}

using Landing.Model.Data;
using Landing.Model.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landing.Server.Database.Query
{
    public class BaseBlog
    {
        private string connectionString;
        public BaseBlog(string con)
        {
            this.connectionString = con;
        }

        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Список</returns>
        public async Task<List<Blog>> GetItems()
        {
            List<Blog> list = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "SELECT * FROM Blog";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Blog item = new Blog();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Description = (string)reader["Description"];
                        item.CreadDate = (DateTime)reader["CreadDate"];
                        item.ImageId = (int)reader["ImageId"];

                        list.Add(item);
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка списка прошла успешно", "BaseBlog/GetItems");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки {ex.Message}", "BaseBlog/GetItems", ex);
                return null;
            }
            return list;
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<Blog> GetItemById(int id)
        {
            Blog item = new Blog();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM Blog WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        item = new Blog();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Description = (string)reader["Description"];
                        item.CreadDate = (DateTime)reader["CreadDate"];
                        item.ImageId = (int)reader["ImageId"];

                    }

                }
                await con.CloseAsync();

                Log.Info("Получение записи по идентификатору прошло успешно", "BaseBlog/GetItemById");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка получения записи по Id {ex.Message}", "BaseBlog/GetItemById", ex);
                return null;
            }
            return item;
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="model">Объект Blog </param>
        /// <returns>Объект Blog с заданым идентификатором</returns>
        public async Task<Blog> Add(Blog model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "INSERT INTO Blog ( Name, Description, CreadDate, ImageId )" +
                             " VALUES ( @Name, @Description, @CreadDate, @ImageId)";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@CreadDate", model.CreadDate);
                    cmd.Parameters.AddWithValue("@ImageId", model.ImageId);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT @@IDENTITY";
                    int lastId = Convert.ToInt32(cmd.ExecuteScalar());
                    model.Id = lastId;
                }
                await con.CloseAsync();

                Log.Info("Добавление записи прошло успешно", "BaseBlog/Add");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка добавлении записи {ex.Message}", "BaseBlog/Add", ex);
                return null;
            }

            return model;
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект Blog с обновлеными даными</param>
        /// <returns>Blog если редактирование прошло успешно, в противном случа null</returns>
        public async Task<Blog> Edit(Blog model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "UPDATE Blog SET Name = @Name , Description = @Description, CreadDate = @CreadDate, ImageId = @ImageId  WHERE Id = @Id";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@CreadDate", model.CreadDate);
                    cmd.Parameters.AddWithValue("@ImageId", model.ImageId);
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Редактирование записи прошло успешно", "BaseBlog/Edit");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка редактирования записи - {ex.Message}", "BaseBlog/Edit", ex);
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
                string sql = $"DELETE from Blog WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Удаление записи прошло успешно", "BaseBlog/Remove");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка удаления записи - {ex.Message}", "BaseBlog/Edit", ex);
                result = false;
            }
            return result;
        }
    }
}

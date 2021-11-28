using Landing.Model.Data;
using Landing.Model.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Landing.Server.Database.Query
{
    public class BaseImages
    {
        private string connectionString;
        public BaseImages(string con) 
        {
            this.connectionString = con;
        }

        /// <summary>
        /// Получить все записи файлов
        /// </summary>
        /// <returns>Список</returns>
        public async Task<List<Images>> GetItems()
        {
            List<Images> list = new();
            
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                    string sql = "SELECT * FROM Images";
                    await con.OpenAsync();
                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            Images item = new Images();
                            item.Id = (int)reader["Id"];
                            item.Name = (string)reader["Name"];
                            item.Location = (string)reader["Location"];
                            item.Extension = (string)reader["Extension"];
                            item.Size = (long)reader["Size"];
                            item.Description = (string)reader["Description"];
                            item.HashCode = (string)reader["HashCode"];


                            list.Add(item);
                        }

                    }
                    await con.CloseAsync();
                
                Log.Info("Загрузка списка изображений прошла успешно", "BaseImages/GetItems");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки изображений {ex.Message}", "BaseImages/GetItems", ex);
                return null;
            }
            return list;
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<Images> GetItemById(int id)
        {
            Images item = new Images();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM Images WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Location = (string)reader["Location"];
                        item.Extension = (string)reader["Extension"];
                        item.Size = (long)reader["Size"];
                        item.Description = (string)reader["Description"];
                        item.HashCode = (string)reader["HashCode"];

                    }

                }
                await con.CloseAsync();
                   
                Log.Info("Загрузка изображения по идентификатору прошла успешно", "BaseImages/GetItemById");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки изображения по Id {ex.Message}", "BaseImages/GetItemById", ex);
                return null;
            }
            return item;
        }
        /// <summary>
        /// Получаем данные по названию папки
        /// </summary>
        /// <param name="location">Папка хранения  </param>
        /// <returns>Модлеь данных</returns>
        public async Task<List<Images>> GetItemByLocation(string location)
        {
            List<Images> list = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM Images WHERE Location = '{location}'";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Images item = new Images();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Location = (string)reader["Location"];
                        item.Extension = (string)reader["Extension"];
                        item.Size = (long)reader["Size"];
                        item.Description = (string)reader["Description"];
                        item.HashCode = (string)reader["HashCode"];


                        list.Add(item);
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка изображения по названию папки прошла успешно", "BaseImages/GetItemByLocation");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки изображения по названию папки {ex.Message}", "BaseImages/GetItemByLocation", ex);
                return null;
            }
            return list;
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="model">Объект Images </param>
        /// <returns>Объект Images с заданым идентификатором</returns>
        public async Task<Images> Add(Images model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                 string sql = "INSERT INTO Images ( Name, Location, Extension, Size, Description, HashCode )" +
                               " VALUES ( @Name, @Location, @Extension, @Size, @Description, @HashCode)";
                 await con.OpenAsync();
                 using (SqlCommand cmd = new SqlCommand(sql, con))
                 {
                     cmd.Parameters.AddWithValue("@Name", model.Name);
                     cmd.Parameters.AddWithValue("@Location", model.Location);
                     cmd.Parameters.AddWithValue("@Extension", model.Extension);
                     cmd.Parameters.AddWithValue("@Size", model.Size);
                     cmd.Parameters.AddWithValue("@Description", model.Description);
                     cmd.Parameters.AddWithValue("@HashCode", model.HashCode);
                     cmd.ExecuteNonQuery();

                     cmd.CommandText = "SELECT @@IDENTITY";
                     int lastId = Convert.ToInt32(cmd.ExecuteScalar());
                     model.Id = lastId;
                 }

                 await con.CloseAsync();
               
                Log.Info("Добавление информации о файле прошло успешно", "BaseImages/Add");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка при добавлении информации о файле {ex.Message}", "BaseImages/Add", ex);
                return null;
            }

            return model;
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект Images с обновлеными даными</param>
        /// <returns>Images если редактирование прошло успешно, в противном случа null</returns>
        public async Task<Images> Edit(Images model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "UPDATE Images SET Name = @Name , Location = @Location, Extension = @Extension, Size = @Size, Description = @Description, HashCode = @HashCode WHERE Id = @Id";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Location", model.Location);
                    cmd.Parameters.AddWithValue("@Extension", model.Extension);
                    cmd.Parameters.AddWithValue("@Size", model.Size);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@HashCode", model.HashCode);
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();
                   
                Log.Info("Редактирование файла прошло успешно", "BaseImages/Edit");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка редактирования записи файла - {ex.Message}", "BaseImages/Edit", ex);
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

                string sql = $"DELETE from Images WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();
                   
                Log.Info("Удаление файла прошло успешно", "BaseImages/Remove");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка удаления записи - {ex.Message}", "BaseImages/Edit", ex);
                result = false;
            }
            return result;
        }
    }
}

using Landing.Model.Data;
using Landing.Model.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Landing.Server.Database.Query
{
    public class BasePortfolio
    {
        private string connectionString;
        public BasePortfolio(string con) 
        {
            this.connectionString = con;
        }
        /// <summary>
        /// Получить все записи портфолио
        /// </summary>
        /// <returns>Список</returns>
        public async  Task<List<Portfolio>> GetItems()
        {
            List<Portfolio> list = new();
            var cons = connectionString;
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "SELECT * FROM Portfolio";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Portfolio item = new Portfolio();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Description = (string)reader["Description"];
                        item.ImageId = (int)reader["ImageId"];

                        list.Add(item);
                    }

                }
                await con.CloseAsync();
                    
                Log.Info("Загрузка списка портфолио прошла успешно", "BasePortfolio/GetItems");
            }
            catch(Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки портфолио {ex.Message}", "BasePortfolio/GetItems", ex);
                return null;
            }
            return list;
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async  Task<Portfolio> GetItemById(int id)
        {
            Portfolio item = new Portfolio();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                 string sql = $"SELECT * FROM Portfolio WHERE Id = {id}";
                 await con.OpenAsync();
                 using (SqlCommand command = new SqlCommand(sql, con))
                 {
                     SqlDataReader reader = await command.ExecuteReaderAsync();
                     while (await reader.ReadAsync())
                     {
                         item = new Portfolio();
                         item.Id = (int)reader["Id"];
                         item.Name = (string)reader["Name"];
                         item.Description = (string)reader["Description"];
                         item.ImageId = (int)reader["ImageId"];

                     }

                 }
                 await con.CloseAsync();
                   
                Log.Info("Загрузка портфолио по идентификатору прошла успешно", "BasePortfolio/GetItemById");
            }
            catch(Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка полечения портфолио по Id {ex.Message}", "BasePortfolio/GetItemById", ex);
                return null;
            }
            return item;
        }
        /// <summary>
        /// Добавление новой записи в портфолио
        /// </summary>
        /// <param name="portfolio">Объект Portfolio </param>
        /// <returns>Объект Portfolio с заданым идентификатором</returns>
        public async Task<Portfolio> Add(Portfolio portfolio)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "INSERT INTO Portfolio ( Name, Description, ImageId)" +
                             " VALUES ( @Name, @Description, @ImageId)";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Name", portfolio.Name);
                    cmd.Parameters.AddWithValue("@Description", portfolio.Description);
                    cmd.Parameters.AddWithValue("@ImageId", portfolio.ImageId);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT @@IDENTITY";
                    int lastId = Convert.ToInt32(cmd.ExecuteScalar());
                    portfolio.Id = lastId;
                }
                await con.CloseAsync();
                  
                Log.Info("Добавление портфолио прошла успешно", "BasePortfolio/Add");
            }
            catch(Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка при добавлении портфолио {ex.Message}", "BasePortfolio/Add", ex) ;
                return null;
            }

            return portfolio;
        }
        /// <summary>
        /// Редактирование записи портфолио
        /// </summary>
        /// <param name="portfolio">Объект Portfolio с обновлеными даными</param>
        /// <returns>Portfolio если редактирование прошло успешно, в противном случа null</returns>
        public async Task<Portfolio> Edit(Portfolio portfolio)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "UPDATE Portfolio SET Name = @Name , Description = @Description, ImageId = @ImageId WHERE Id = @Id";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", portfolio.Id);
                    cmd.Parameters.AddWithValue("@Name", portfolio.Name);
                    cmd.Parameters.AddWithValue("@Description", portfolio.Description);
                    cmd.Parameters.AddWithValue("@ImageId", portfolio.ImageId);
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();
                   
                Log.Info("Редактирование портфолио прошла успешно", "BasePortfolio/Edit");
            }
            catch(Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка редактирования записи - {ex.Message}", "BasePortfolio/Edit", ex);
                return null;
            }
            return portfolio;
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
                string sql = $"DELETE from Portfolio WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();
                   
                Log.Info("Удаление портфолио прошла успешно", "BasePortfolio/Remove");
            }catch(Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка удаления записи - {ex.Message}", "BasePortfolio/Edit", ex);
                result = false;
            }
            return result;
        }
    }
}

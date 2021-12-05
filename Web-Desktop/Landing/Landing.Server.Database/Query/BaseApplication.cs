using Landing.Library.Model;
using Landing.Library.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Landing.Server.Database.Query
{
    public class BaseApplication
    {
        private string connectionString;
        public BaseApplication(string con)
        {
            this.connectionString = con;
        }
        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Список</returns>
        public async Task<List<Requisition>> GetItems()
        {
            List<Requisition> list = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "SELECT * FROM Application";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Requisition item = new Requisition();
                        item.Id = (int)reader["Id"];
                        item.StatusId = (int)reader["StatusId"];
                        item.Name = (string)reader["Name"];
                        item.Email = (string)reader["Email"];
                        item.Date = (DateTime)reader["Date"];
                        item.OrderNumber = (int)reader["OrderNumber"];
                        item.Agree = (bool)reader["Agree"];
                        item.Description = (string)reader["Description"];

                        list.Add(item);
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка списка прошла успешно", "BaseApplication/GetItems");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки {ex.Message}", "BaseApplication/GetItems", ex);
                return null;
            }
            return list;
        }
        /// <summary>
        /// Получаем количество записей
        /// </summary>
        /// <returns>Количество записей</returns>
        public async Task<int> GetCount()
        {
            SqlConnection con = new SqlConnection(connectionString);
            int count = 0;
            try
            {
                string sql = "SELECT COUNT(*) FROM Application";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    count = (int)command.ExecuteScalar();
                }
                await con.CloseAsync();

                Log.Info("Получение количесива записей прошло успешно", "BaseApplication/GetCount");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки количества записей {ex.Message}", "BaseApplication/GetCount", ex);
                return 0;
            }
            return count;
        }
        /// <summary>
        /// Получаем данные по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор </param>
        /// <returns>Модлеь данных</returns>
        public async Task<Requisition> GetItemById(int id)
        {
            Requisition item = new Requisition();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM Application WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        item = new Requisition();
                        item.Id = (int)reader["Id"];
                        item.StatusId = (int)reader["StatusId"];
                        item.Name = (string)reader["Name"];
                        item.Email = (string)reader["Email"];
                        item.Date = (DateTime)reader["Date"];
                        item.OrderNumber = (int)reader["OrderNumber"];
                        item.Agree = (bool)reader["Agree"];
                        item.Description = (string)reader["Description"];

                    }

                }
                await con.CloseAsync();

                Log.Info("Получение записи по идентификатору прошло успешно", "BaseApplication/GetItemById");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка получения записи по Id {ex.Message}", "BaseApplication/GetItemById", ex);
                return null;
            }
            return item;
        }
        /// <summary>
        /// Добавление новой записи
        /// </summary>
        /// <param name="model">Объект Requisition </param>
        /// <returns>Объект Requisition с заданым идентификатором</returns>
        public async Task<Requisition> Add(Requisition model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            model.StatusId = 1;
            model.Date = DateTime.Now;
            try
            {
                string sql = "INSERT INTO Application ( StatusId, Description, Name, Email, Date, Agree, OrderNumber )" +
                             " VALUES (  @StatusId, @Description, @Name, @Email, @Date, @Agree, @OrderNumber)";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@StatusId", model.StatusId);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Date", model.Date);
                    cmd.Parameters.AddWithValue("@Agree", model.Agree);
                    cmd.Parameters.AddWithValue("@OrderNumber", model.OrderNumber);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT @@IDENTITY";
                    int lastId = Convert.ToInt32(cmd.ExecuteScalar());
                    model.Id = lastId;
                }
                await con.CloseAsync();

                Log.Info("Добавление записи прошло успешно", "BaseApplication/Add");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка добавлении записи {ex.Message}", "BaseApplication/Add", ex);
                return null;
            }

            return model;
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект Requisition с обновлеными даными</param>
        /// <returns>Requisition если редактирование прошло успешно, в противном случа null</returns>
        public async Task<Requisition> Edit(Requisition model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "UPDATE Application SET StatusId = @StatusId , Description = @Description, Name = @Name," +
                             "Email = @Email, Date = @Date, Agree = @Agree, OrderNumber = @OrderNumber  WHERE Id = @Id";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@StatusId", model.StatusId);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Date", model.Date);
                    cmd.Parameters.AddWithValue("@Agree", model.Agree);
                    cmd.Parameters.AddWithValue("@OrderNumber", model.OrderNumber);
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Редактирование записи прошло успешно", "BaseApplication/Edit");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка редактирования записи - {ex.Message}", "BaseApplication/Edit", ex);
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
                string sql = $"DELETE from Application WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Удаление записи прошло успешно", "BaseApplication/Remove");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка удаления записи - {ex.Message}", "BaseApplication/Edit", ex);
                result = false;
            }
            return result;
        }
    }
}

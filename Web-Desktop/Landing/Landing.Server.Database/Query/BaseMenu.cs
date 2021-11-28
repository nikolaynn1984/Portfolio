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
    public class BaseMenu
    {
        private string connectionString;
        public BaseMenu(string con)
        {
            this.connectionString = con;
        }
        /// <summary>
        /// Получить все записи меню
        /// </summary>
        /// <returns>Список</returns>
        public async Task<List<Menu>> GetItems()
        {
            List<Menu> list = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "SELECT * FROM Menu";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        Menu item = new Menu();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Contoller = (string)reader["Contoller"];
                        item.Method =  (string)reader["Method"];

                        list.Add(item);
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка списка меню прошла успешно", "BaseMenu/GetItems");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки меню {ex.Message}", "BaseMenu/GetItems", ex);
                return null;
            }
            return list;
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект Menu с обновлеными даными</param>
        /// <returns>Menu если редактирование прошло успешно, в противном случа null</returns>
        public async Task<bool> Edit(Menu model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "UPDATE Menu SET Name = @Name , Contoller = @Contoller, Method = @Method  WHERE Id = @Id";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Contoller", model.Contoller);
                    cmd.Parameters.AddWithValue("@Method", model.Method);
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Редактирование записи прошло успешно", "BaseMenu/Edit");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка редактирования записи - {ex.Message}", "BaseMenu/Edit", ex);
                return false;
            }
            return true;
        }
    }
}

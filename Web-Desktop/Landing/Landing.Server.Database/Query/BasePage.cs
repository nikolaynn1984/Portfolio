using Landing.Model.Data;
using Landing.Model.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Landing.Server.Database.Query
{

    public class BasePage
    {
        private string connectionString;
        public BasePage(string con)
        {
            this.connectionString = con;
        }

        /// <summary>
        /// Получаем данные страницы
        /// </summary>
        /// <returns>Модлеь данных</returns>
        public async Task<PageView> Get(int id)
        {
            PageView item = new PageView();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM PageView WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        item = new PageView();
                        item.Id = (int)reader["Id"];
                        item.HeaderName = (string)reader["HeaderName"];
                        item.HeaderButtton = (string)reader["HeaderButtton"];
                        item.ApplicationHead = (string)reader["ApplicationHead"];
                        item.Coordinates = (string)reader["Coordinates"];
                        item.LabelColor = (string)reader["LabelColor"];
                        item.ContactInfo = (string)reader["ContactInfo"];
                        item.HeaderImageId = (int)reader["HeaderImageId"];
                        item.TelegramToken = (string)reader["TelegramToken"];

                    }

                }
                await con.CloseAsync();

                Log.Info("Получение данных страницы успешно загружены", "BasePage/Get");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка получения записи страницы {ex.Message}", "BasePage/Get", ex);
                return null;
            }
            return item;
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="model">Объект PageView с обновлеными даными</param>
        /// <returns>PageView если редактирование прошло успешно, в противном случа null</returns>
        public async Task<PageView> Edit(PageView model)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "UPDATE PageView SET HeaderName = @HeaderName , HeaderButtton = @HeaderButtton, ApplicationHead = @ApplicationHead, " +
                              "Coordinates = @Coordinates, LabelColor = @LabelColor, ContactInfo = @ContactInfo, HeaderImageId = @HeaderImageId, TelegramToken = @TelegramToken " +
                             "WHERE Id = @Id";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", model.Id);
                    cmd.Parameters.AddWithValue("@HeaderName", model.HeaderName);
                    cmd.Parameters.AddWithValue("@HeaderButtton", model.HeaderButtton);
                    cmd.Parameters.AddWithValue("@ApplicationHead", model.ApplicationHead);
                    cmd.Parameters.AddWithValue("@Coordinates", model.Coordinates);
                    cmd.Parameters.AddWithValue("@LabelColor", model.LabelColor);
                    cmd.Parameters.AddWithValue("@ContactInfo", model.ContactInfo);
                    cmd.Parameters.AddWithValue("@HeaderImageId", model.HeaderImageId);
                    cmd.Parameters.AddWithValue("@TelegramToken", model.TelegramToken);
                    cmd.ExecuteNonQuery();
                }
                await con.CloseAsync();

                Log.Info("Редактирование записи прошло успешно", "BasePage/Edit");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка редактирования записи - {ex.Message}", "BasePage/Edit", ex);
                return null;
            }
            return model;
        }
    }
}

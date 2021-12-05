using Landing.Library.Model;
using Landing.Library.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Landing.Server.Database.Query
{
    public class BaseMessagesBot
    {
        private string connectionString;
        public BaseMessagesBot(string con)
        {
            this.connectionString = con;
        }
        /// <summary>
        /// Получаем список сообщений
        /// </summary>
        /// <returns>Список сообщений</returns>
        public async Task<List<MessagesBot>> GetMessages()
        {
            List<MessagesBot> list = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "SELECT * FROM MessagesBot";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        MessagesBot item = new MessagesBot();
                        item.Id = (int)reader["Id"];
                        item.UserMessageId = (int)reader["UserMessageId"];
                        item.FromIdUser = (long)reader["FromIdUser"];
                        item.SendType = (string)reader["SendType"];
                        item.CreateDate = (DateTime)reader["CreateDate"];
                        item.MessgeType = (string)reader["MessgeType"];
                        item.MessageText = (string)reader["MessageText"];
                        item.ImageId = (int)reader["ImageId"];
                        item.MessageCaption = (string)reader["MessageCaption"];

                        list.Add(item);
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка списка сообщений прошла успешно", "BaseMessagesBot/GetMessages");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки всех сообщений {ex.Message}", "BaseMessagesBot/GetMessages", ex);
                return null;
            }
            return list;
        }
        /// <summary>
        /// Получаем список пользователей
        /// </summary>
        /// <returns>Список пользователей бота</returns>
        public async Task<List<MessageUser>> GetUsers()
        {
            List<MessageUser> list = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "SELECT * FROM MessageUser";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        MessageUser item = new MessageUser();
                        item.Id = (int)reader["Id"];
                        item.UserName = (string)reader["UserName"];
                        item.UserId = (long)reader["UserId"];
                        item.AddDate = (DateTime)reader["AddDate"];

                        list.Add(item);
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка списка пользователей прошла успешно", "BaseMessagesBot/GetUsers");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки всех пользователей {ex.Message}", "BaseMessagesBot/GetUsers", ex);
                return null;
            }
            return list;
        }
        /// <summary>
        /// Получаем информацию пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Объект MessageUser </returns>
        public async Task<MessageUser> GetUsersByUserId(long id)
        {
            MessageUser model = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM MessageUser WHERE UserId = {id}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        model = new MessageUser();
                        model.Id = (int)reader["Id"];
                        model.UserName = (string)reader["UserName"];
                        model.UserId = (long)reader["UserId"];
                        model.AddDate = (DateTime)reader["AddDate"];
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка пользователя по Id прошла успешно", "BaseMessagesBot/GetUsersById");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки пользователя по Id {ex.Message}", "BaseMessagesBot/GetUsersById", ex);
                return null;
            }
            return model;
        }
        /// <summary>
        /// Получаем информацию пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Объект MessageUser </returns>
        public async Task<MessageUser> GetUsersById(long id)
        {
            MessageUser model = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM MessageUser WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        model = new MessageUser();
                        model.Id = (int)reader["Id"];
                        model.UserName = (string)reader["UserName"];
                        model.UserId = (long)reader["UserId"];
                        model.AddDate = (DateTime)reader["AddDate"];
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка пользователя по Id прошла успешно", "BaseMessagesBot/GetUsersById");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки пользователя по Id {ex.Message}", "BaseMessagesBot/GetUsersById", ex);
                return null;
            }
            return model;
        }
        /// <summary>
        /// Получаем сообщение по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сообщение</returns>
        public async Task<MessagesBot> GetMessagesById(int id)
        {
            MessagesBot item = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM MessagesBot  WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        item = new MessagesBot();
                        item.Id = (int)reader["Id"];
                        item.UserMessageId = (int)reader["UserMessageId"];
                        item.FromIdUser = (long)reader["FromIdUser"];
                        item.CreateDate = (DateTime)reader["CreateDate"];
                        item.MessgeType = (string)reader["MessgeType"];
                        item.MessageText = (string)reader["MessageText"];
                        item.ImageId = (int)reader["ImageId"];
                        item.MessageCaption = (string)reader["MessageCaption"];

                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка сообщения по Id прошла успешно", "BaseMessagesBot/GetMessagesById");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки сообщения по Id {ex.Message}", "BaseMessagesBot/GetMessagesById", ex);
                return null;
            }
            return item;
        }
        /// <summary>
        /// Загрузка списка сообщений по идентификатору пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Список сообщений</returns>
        public async Task<List<MessagesBot>> GetMessagesByUserId(int userId)
        {
            List<MessagesBot> list = new();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM MessagesBot WHERE UserMessageId = {userId}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        MessagesBot item = new MessagesBot();
                        item.Id = (int)reader["Id"];
                        item.UserMessageId = (int)reader["UserMessageId"];
                        item.FromIdUser = (long)reader["FromIdUser"];
                        item.SendType = (string)reader["SendType"];
                        item.CreateDate = (DateTime)reader["CreateDate"];
                        item.MessgeType = (string)reader["MessgeType"];
                        item.MessageText = (string)reader["MessageText"];
                        item.ImageId = (int)reader["ImageId"];
                        item.MessageCaption = (string)reader["MessageCaption"];

                        list.Add(item);
                    }

                }
                await con.CloseAsync();

                Log.Info("Загрузка списка сообщений по Id пользователя прошла успешно", "BaseMessagesBot/GetMessagesByUserId");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка загрузки сообщений по Id пользователя {ex.Message}", "BaseMessagesBot/GetMessagesByUserId", ex);
                return null;
            }
            return list;
        }


        /// <summary>
        /// Добавляем сообщение 
        /// </summary>
        /// <param name="messages">Сообщение</param>
        /// <returns>MessagesBot с присвоеным Id если добалвение прошло успешно, в противном случае false</returns>
        public async Task<MessagesBot> AddMessage(MessagesBot messages)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "INSERT INTO MessagesBot ( UserMessageId, CreateDate, MessgeType, MessageText, ImageId, MessageCaption, FromIdUser, SendType )" +
                             " VALUES ( @UserMessageId, @CreateDate, @MessgeType, @MessageText, @ImageId, @MessageCaption, @FromIdUser,  @SendType)";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserMessageId", messages.UserMessageId);
                    cmd.Parameters.AddWithValue("@CreateDate", messages.CreateDate);
                    cmd.Parameters.AddWithValue("@FromIdUser", messages.FromIdUser);
                    cmd.Parameters.AddWithValue("@MessgeType", messages.MessgeType);
                    cmd.Parameters.AddWithValue("@MessageText", messages.MessageText);
                    cmd.Parameters.AddWithValue("@ImageId", messages.ImageId);
                    cmd.Parameters.AddWithValue("@MessageCaption", messages.MessageCaption);
                    cmd.Parameters.AddWithValue("@SendType", messages.SendType);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT @@IDENTITY";
                    int lastId = Convert.ToInt32(cmd.ExecuteScalar());
                    messages.Id = lastId;
                }
                await con.CloseAsync();

                Log.Info("Добавление записи прошло успешно", "BaseMessagesBot/AddMessage");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка добавлении записи {ex.Message}", "BaseMessagesBot/AddMessage", ex);
                return null;
            }

            return messages;
        }
        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="user">Объект MessageUser - Пользователь</param>
        /// <returns>Объект MessageUser если добавление прошло успешно, в противном случае null</returns>
        public async Task<MessageUser> AddUser(MessageUser user)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = "INSERT INTO MessageUser ( UserName, UserId, AddDate )" +
                             " VALUES ( @UserName, @UserId, @AddDate)";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    cmd.Parameters.AddWithValue("@AddDate", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT @@IDENTITY";
                    int lastId = Convert.ToInt32(cmd.ExecuteScalar());
                    user.Id = lastId;
                }
                await con.CloseAsync();

                Log.Info("Добавление записи прошло успешно", "BaseMessagesBot/AddUser");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка добавлении записи {ex.Message}", "BaseMessagesBot/AddUser", ex);
                return null;
            }

            return user;
        }
    }
}

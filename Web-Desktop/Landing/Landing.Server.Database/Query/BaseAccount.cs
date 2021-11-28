using Landing.Model.Data;
using Landing.Model.Logging;
using Landing.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landing.Server.Database.Query
{
    public class BaseAccount
    {
        private string connectionString;
        public BaseAccount(string con)
        {
            this.connectionString = con;
        }

        /// <summary>
        /// Получаем роль по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Роль</returns>
        public async Task<string> GetRoleById(int id)
        {
            UserRole item = new UserRole();
            string role = string.Empty;
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                string sql = $"SELECT * FROM RolesLanding WHERE Id = {id}";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        item = new UserRole()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"]
                        };
                    }
                    role = item.Name;
                }
                await con.CloseAsync();

                Log.Info("Получение роли пользователя по идентификатору прошло успешно", "BaseAccount/GetRoleById");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка получения роли по Id {ex.Message}", "BaseAccount/GetRoleById", ex);
                return null;
            }
            return role;
        }
        /// <summary>
        /// Получаем информацию о пользователе 
        /// </summary>
        /// <param name="email">Емаил</param>
        /// <param name="password">пароль</param>
        /// <returns>Модель UserCheck</returns>
        public async Task<UserCheck> GetUserByEmailAndPassword(string email, string password)
        {
            UserCheck item = new UserCheck();
            item.Succeeded = false;
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                //string sql = $"SELECT * FROM UserLanding";
                string sql = $"Select UserLanding.Id, UserLanding.UserName, UserLanding.Name, UserLanding.Surname, UserLanding.Email, UserLanding.Password, UserLanding.ImageId, RolesLanding.Name as Role " +
                            $"FROM UserLanding " +
                            $"INNER JOIN RolesLanding on RolesLanding.Id = UserLanding.RoleId " +
                            $"WHERE Email = '{email}' AND Password = '{password}'";
                await con.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        item = new UserCheck();
                        item.Id = (int)reader["Id"];
                        item.Name = (string)reader["Name"];
                        item.Email = (string)reader["Email"];
                        item.UserName = (string)reader["UserName"];
                        item.ImageId = (int)reader["ImageId"];
                        item.Role = (string)reader["Role"];

                        
                    }
                    if (!string.IsNullOrEmpty(item.Name)) item.Succeeded = true;
                    else item.Succeeded = false;
                }
                await con.CloseAsync();

                Log.Info($"Идентификация пользователя в базе данных {item.Succeeded}", "BaseAccount/GetUserByEmailAndPassword");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка идентификации пользователя в базе {ex.Message}", "BaseAccount/GetUserByEmailAndPassword", ex);
                return null;
            }
            return item;
        }
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="register">Модель с данными</param>
        /// <returns></returns>
        public async Task<UserCheck> RegisterUser(RegisterViewModel register)
        {
            SqlConnection con = new SqlConnection(connectionString);
            UserCheck model = new UserCheck();
            try
            {
                string sql = "INSERT INTO UserLanding ( UserName, Name ,Surname ,Email ,Password ,RegisterDate ,ImageId , RoleId)" +
                             " VALUES ( @UserName, @Name ,@Surname ,@Email ,@Password ,@RegisterDate ,@ImageId , @RoleId)";
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserName", register.Email);
                    cmd.Parameters.AddWithValue("@Name", "");
                    cmd.Parameters.AddWithValue("@Surname", "");
                    cmd.Parameters.AddWithValue("@Email", register.Email);
                    cmd.Parameters.AddWithValue("@Password", register.Password);
                    cmd.Parameters.AddWithValue("@RegisterDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ImageId", 0);
                    cmd.Parameters.AddWithValue("@RoleId", 2);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT @@IDENTITY";
                    int lastId = Convert.ToInt32(cmd.ExecuteScalar());
                    model.Id = lastId;
 
                }
                await con.CloseAsync();
                model.Name = "";
                model.UserName = register.Email;
                model.Email = register.Email;
                model.ImageId = 0;
                model.Role = "Guest";
                model.Succeeded = true;

                Log.Info($"Регистрация пользователя {register.Email}", "BaseAccount/RegisterUser");
            }
            catch (Exception ex)
            {
                await con.CloseAsync();
                Log.Error($"Ошибка регистрации пользователя {ex.Message}", "BaseAccount/RegisterUser", ex);
                return null;
            }

            return model;
        }
    }
}

using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace StoreDatabase
{
    public static class ConnectionClient
    {
        private static ErrorMes errorMes;
        static ConnectionClient()
        {
            connection.StateChange += StateChange_Con;
            errorMes = new ErrorMes();
        }
        /// <summary>
        /// Отлавливает состояние к базе данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void StateChange_Con(object sender, StateChangeEventArgs e)
        {
            Debug.WriteLine(connection.State.ToString());
        }
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        private static SqlConnectionStringBuilder sqlCon = new SqlConnectionStringBuilder()
        {
            DataSource = @"(LocalDB)\MSSQLLocalDB",
            InitialCatalog = "ClientDatabase",
            IntegratedSecurity = true,
            Pooling = false
        };
        /// <summary>
        /// Соединение к базе данных
        /// </summary>
        public static SqlConnection connection = new SqlConnection() { ConnectionString = sqlCon.ConnectionString };

        /// <summary>
        /// Открывает подключение к базе данных
        /// </summary>
        public static void ConOpen() 
        {
            try
            {
                connection.Open();
            }
            catch(SqlException e) { errorMes.ErrorSQL(e.Message); }
            
        }
        /// <summary>
        /// Закрывает подключение к базе данных
        /// </summary>
        public static void ConClose() 
        {
            try
            {
                connection.Close();
            }catch(SqlException e) { errorMes.ErrorSQL(e.Message); }
             
        }
        /// <summary>
        /// Проверка подключения к базе данных
        /// </summary>
        /// <returns>true - если соединение установлено, в противном случае - false</returns>
        public static bool ConnectionTest()
        {
            bool result = true;
            try
            {
                connection.Open();
            }
            catch
            {
                result = false;
            }
            finally { connection.Close(); }
            return result;
        }
      
    }
}

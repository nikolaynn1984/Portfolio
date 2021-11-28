namespace Landing.Server.Intarfase
{
    public interface IConnection
    {
        /// <summary>
        /// Строка соединения
        /// </summary>
        /// <returns>Информация</returns>
        string GetConnectionString();
    }
}

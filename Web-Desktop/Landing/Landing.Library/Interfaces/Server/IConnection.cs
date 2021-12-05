namespace Landing.Library.Interfaces.Server
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

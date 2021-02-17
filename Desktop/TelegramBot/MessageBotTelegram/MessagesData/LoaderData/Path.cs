using System;

namespace MessagesData.LoaderData
{
    [Flags]
    internal enum Path
    {
        Users = 1, // Пользователи
        MessagesLog = 2, // Сообщения
    }
}

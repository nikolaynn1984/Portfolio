# Portfolio
Niko Portfolio

# Хранилище
Приложение MVC

Для работы с приложением ClientBank требуется выполнить следующее!

1. Требуется создать базу данных `Microsoft SQL Server`
2. Откройте файл `.sql` с названием `SQLCreateDatandTables.sql`, 
   файл содержит в себе информацию о таблицах и данных к ним которые требуется 
   создать в созданой вами базе  `Microsoft SQL Server`.
3. Зайти в фаил `Web.config` изменить строку `connectionStrings` для подключения к вашей базе,
    ```C#
      <connectionStrings>
    <add name="StorageDatabase" connectionString="Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=TreeViewData;Integrated Security=True;" providerName="System.Data.SqlClient" />
  </connectionStrings>
    ```
5. Запускайте приложение!


`Скрин`

<img src="https://github.com/nikolaynn1984/Portfolio/blob/main/MVC/scrin.png"  max-width="500" alt="Скрин хранилище">


# TelegramBot

1. Для запуска приложения требуется создать свой чат бот по инструкции https://core.telegram.org/bots#6-botfather
2. Открыть проект `MessageBotTelegram` в папке `ViewModel` открыть файл `BotWindowViewModel.cs` 
   в строке кода вставить полученный при создании бота - токен
     
```C#
       static string token = "ваш токен";//Токен Бота
```


# Drive74
1.Требуется открыть приложение в папке `Travel` 
2. Строка подключения в проекте `TravelDatabase` в классе `DataContext`
3. После изменения строки подключения требуется прописать в консоле команду `Update-Database -Project TravelDatabase`


Работа с API

1. Требуется открыть проект в папке `WpfTravelApp`
2. Запустить проект в папке `WebAPI`
2. В классе `MainWindow.xaml.cs` есть `static string local = "https://localhost:44317/";` вместо существуещего local требуется прописать результат полученный в строке подключения запущенного проекта WebAPI
4. Запустить приложения WPF


# ClientBank
Client bank desktop application

Для работы с приложением ClientBank требуется выполнить следующее!

1. Требуется создать базу данных `Microsoft SQL Server`
2. Открыть решении ClientBank и перейти в проект с названием StoreDatabase 
3. В проекте находится файл `.sql` с названием `SQLCreateDatandTables.sql`, 
   файл содержит в себе информацию о таблицах и данных к ним которые требуется 
   создать в созданой базе  `Microsoft SQL Server`.
4. Так же в проекте находится `class` `ConnectionClient.cs`, в нем нужно отредактировать 
    строку подключения соответствующе созданной вами базы данных
    ```C#
    private static SqlConnectionStringBuilder sqlCon = new SqlConnectionStringBuilder()
        {
            DataSource = @"(LocalDB)\MSSQLLocalDB",  //Ваши данные
            InitialCatalog = "ClientDatabase",     //Ваши данные
            IntegratedSecurity = true,
            Pooling = false
        };
    ```
5. Запускайте приложение!


# Hotels
Главная страница сайта Гостиничного комплекса - `HTML, CSS`

На страничке используются спрайты `svg`, запускать лучше на локальном сервере

<img src="https://github.com/nikolaynn1984/Portfolio/blob/main/hotels.png" max-width="500" alt="Скрин странички отели">


# Ewklid
Главная страница сайта Евклид - `HTML, CSS`

На страничке используются спрайты `svg`, запускать лучше на локальном сервере

`Разрешение - 1920`

<img src="https://github.com/nikolaynn1984/Portfolio/blob/main/Web-Desktop/HTML,CSS,JS/ewklid/screen/max.png" max-width="500" alt="Скрин Евклид">

`Разрешение - 768`

<img src="https://github.com/nikolaynn1984/Portfolio/blob/main/Web-Desktop/HTML,CSS,JS/ewklid/screen/768.png" max-width="350" alt="Скрин Евклид">

`Разрешение - 320`

<img src="https://github.com/nikolaynn1984/Portfolio/blob/main/Web-Desktop/HTML,CSS,JS/ewklid/screen/320.png" max-width="250" alt="Скрин Евклид">

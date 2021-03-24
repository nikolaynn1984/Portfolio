

# Хранилище
Приложение MVC

Для работы с приложением требуется выполнить следующее!

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

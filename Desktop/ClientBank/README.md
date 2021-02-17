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

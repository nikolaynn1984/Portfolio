
CREATE TABLE [dbo].[Application](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [StatusId] [int] NULL,
    [Name] [nvarchar](150) NULL,
    [Email] [nvarchar](150) NULL,
    [Date] [datetime2](7) NULL,
    [OrderNumber] [int] NULL,
    [Agree] [bit] NULL,
    [Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[ApplicationStatus](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](150) NULL,
    [Description] [nvarchar](255) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Blog](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](255) NULL,
    [Description] [nvarchar](max) NULL,
    [CreadDate] [datetime2](7) NULL,
    [ImageId] [int] NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Images](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](150) NULL,
    [Location] [nvarchar](50) NULL,
    [Extension] [nvarchar](15) NULL,
    [Size] [bigint] NULL,
    [Description] [nvarchar](255) NULL,
    [HashCode] [nvarchar](150) NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Menu](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](50) NULL,
    [Contoller] [nvarchar](50) NULL,
    [Method] [nvarchar](50) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[MessagesBot](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UserMessageId] [int] NULL,
    [FromIdUser] [bigint] NULL,
    [SendType] [nvarchar](50) NOT NULL,
    [CreateDate] [datetime2](7) NULL,
    [MessgeType] [nvarchar](50) NULL,
    [MessageText] [nvarchar](max) NULL,
    [ImageId] [int] NULL,
    [MessageCaption] [nvarchar](max) NULL,
 CONSTRAINT [PK_MessagesBot] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[MessageUser](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UserName] [nvarchar](150) NULL,
    [UserId] [bigint] NULL,
    [AddDate] [datetime2](7) NULL,
 CONSTRAINT [PK_MessageUser] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PageView](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [HeaderName] [nvarchar](250) NULL,
    [HeaderButtton] [nvarchar](50) NULL,
    [ApplicationHead] [nvarchar](150) NULL,
    [Coordinates] [nvarchar](50) NULL,
    [LabelColor] [nvarchar](50) NULL,
    [ContactInfo] [nvarchar](max) NULL,
    [HeaderImageId] [int] NULL,
    [TelegramToken] [nvarchar](250) NULL,
 CONSTRAINT [PK_PageView] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Portfolio](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](255) NULL,
    [Description] [nvarchar](max) NULL,
    [ImageId] [int] NULL,
 CONSTRAINT [PK_Portfolio] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[RolesLanding](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_RolesLanding] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Services](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](255) NULL,
    [Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Social](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](255) NULL,
    [Link] [nvarchar](max) NULL,
    [ImageId] [int] NULL,
 CONSTRAINT [PK_Social] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[UserLanding](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UserName] [nvarchar](150) NULL,
    [Name] [nvarchar](150) NULL,
    [Surname] [nvarchar](150) NULL,
    [Email] [nvarchar](150) NULL,
    [Password] [nvarchar](150) NULL,
    [RegisterDate] [datetime2](7) NULL,
    [ImageId] [int] NULL,
    [RoleId] [int] NULL,
 CONSTRAINT [PK_UserLanding] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




INSERT INTO [dbo].[Images]  ([Name]  ,[Location] ,[Extension]  ,[Size] ,[Description] ,[HashCode])
VALUES  (N'DOC19920211039.jpg' ,N'Header' ,N'.jpg' ,150982, N'Банер ' ,N'F8-B2-00-5B-C1-5A-E8-3E-67-8C-69-5D-2C-C8-AB-85-9D-9E-26-E2')


INSERT INTO [dbo].[PageView]  ([HeaderName]  ,[HeaderButtton] ,[ApplicationHead] ,[Coordinates] ,[LabelColor] ,[ContactInfo] ,[HeaderImageId]  ,[TelegramToken])
VALUES  (N'Консалтинг без регистрации и СМС'  ,N'Оставить заявку' , N'Оставить заявку или задать вопрос',N'55.61940967516268, 37.65422381714141' , N'156894', N'', 1, N'')


INSERT INTO [dbo].[ApplicationStatus]  ([Id], [Name] ,[Description])
     VALUES (1, N'Получена' , N'начальный статус. Гость заполнил форму и отправил данные, они поступили в систему, но ещё не были обработаны.')
     VALUES (2, N'В работе', N'Администратор связался с гостем для уточнения деталей.')
     VALUES (3, N'Выполнена', N'Услуга оказана.')
     VALUES (4, N'Отклонена', N'Заявка не подходит или сделана ботом.')
     VALUES (5, N'Отменена', N'Насчёт заявки передумали или она потеряла актуальность.')



INSERT INTO [dbo].[RolesLanding] ([Name])
     VALUES ('Admin')
     VALUES ('Guest')


INSERT INTO [dbo].[UserLanding]([UserName] ,[Name] ,[Surname] ,[Email] ,[Password] ,[RegisterDate] ,[ImageId] ,[RoleId])
     VALUES ('admin@landing.ru' ,'admin' ,'admin' ,'admin@landing.ru' ,'Passwd123','2021-09-18 00:00:00.0000000' ,0  , 1)
     VALUES ('guest@landing.ru' ,'guest' ,'guest' ,'guest@landing.ru' ,'Passwd123','2021-09-18 00:00:00.0000000' ,0  , 2)



using Landing.Library.Interfaces;
using Landing.Library.Model;
using Landing.Library.Logging;
using Landing.Server.Data;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Landing.Library.Interfaces.Server;

namespace Landing.Server.Options
{
    public class MessagesBotService : IHostedService, IBotMessage
    {

        private readonly IMessagesBot data;
        private readonly IImagesFile imgData;
        private string token = string.Empty; //Токен Бота;
        private static TelegramBotClient bot;
        private readonly PageData page;
        private long FromUser = 0;

        public MessagesBotService(IMessagesBot data, IImagesFile imgData)
        {
            this.data = data;
            this.imgData = imgData;
            this.page = new PageData();
            PageData.TelegramTokenChanged += Page_TelegramTokenChanged;
        }

        /// <summary>
        /// Слушаем изменение токека телеграмм бота
        /// </summary>
        /// <param name="token">Токен</param>
        private void Page_TelegramTokenChanged(string token)
        {
            if(this.token != token)
            {
                ConnectionBot(token);
            }
        }

        /// <summary>
        /// Обработка событий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BotHandler(object sender, MessageEventArgs e)
        {
            switch (e.Message.Type)
            {
                case MessageType.Text: await AcceptMessage(e); break;
                case MessageType.Photo: await AcceptMessagePhoto(e); break;
                default: break;
            }
        }
        /// <summary>
        /// Получаем сообщение с изображением
        /// </summary>
        /// <param name="message">Сообщение</param>
        private async Task AcceptMessagePhoto(MessageEventArgs message)
        {
            MessageUser user = new MessageUser();
            var image = await ImageDownload(message);
            user.UserId = message.Message.Chat.Id;
            user.UserName = message.Message.Chat.FirstName;
            user = await data.AddUser(user);
            if (user != null)
            {
                if(image.Id > 0)
                {
                    MessagesBot item = new MessagesBot
                    {
                        MessageText = "",
                        FromIdUser = 0,
                        CreateDate = DateTime.Now,
                        MessgeType = MessageType.Photo.ToString(),
                        SendType = "From",
                        UserMessageId = user.Id,
                        ImageId = image.Id,
                        MessageCaption = message.Message.Caption != null ? message.Message.Caption : ""
                    };

                    await data.AddMessage(item);
                }
               
            }
        }
        /// <summary>
        /// Текстовое сообщение Комманд Бота
        /// </summary>
        /// <returns>Возвращает сообщение</returns>
        private string CommandMessage()
        {
            return "Ниже описаны команды " +
                   "\n /Привет" +
                   "\n /Как дела? " +
                   "\n /Фото " +
                   "\n /Музыка" +
                   "\n /info"; ;
        }
        /// <summary>
        /// Принимаем сообщение от пользователя
        /// </summary>
        /// <param name="message">Сообщение</param>
        public async Task AcceptMessage(MessageEventArgs message)
        {
            MessageUser user = new MessageUser();
            user.UserId = message.Message.Chat.Id;
            user.UserName = message.Message.Chat.FirstName;
            user = await data.AddUser(user);
            if (user != null)
            {
                MessagesBot item = new MessagesBot
                {
                    MessageText = message.Message.Text,
                    FromIdUser = 0,
                    CreateDate = DateTime.Now,
                    MessgeType = MessageType.Text.ToString(),
                    UserMessageId = user.Id,
                    SendType = "From",
                    ImageId = 0,
                    MessageCaption = ""
                };

                await data.AddMessage(item);
            }
        }
        /// <summary>
        /// Отправляем сообщение пользователю
        /// </summary>
        /// <param name="messages">Сообщение</param>
        /// <returns></returns>
        public async Task<MessagesBot> Send(MessagesBot messages)
        {
            try
            {
                MessageUser user = await data.GetUsersById(messages.UserMessageId);
                messages.FromIdUser = FromUser;
                switch (messages.MessgeType)
                {
                    case "Text": await SendText(user.UserId, messages); break;
                    case "Photo": await SendImage(user.UserId, messages); break;
                    default: break;
                }
            }catch(Exception ex)
            {
                Log.Error("Ошибка отправки сообщения", "Send", ex);
                return null;
            }

            return messages;
        }
        /// <summary>
        /// Отправляем текстовое сообщение
        /// </summary>
        /// <param name="userid">Идентификатор пользователя</param>
        /// <param name="messages">Сообщение</param>
        private async Task SendText(long userid, MessagesBot messages)
        {
            messages.FromIdUser = FromUser;
            await bot.SendTextMessageAsync(userid, messages.MessageText);
            await data.AddMessage(messages);
        }
        /// <summary>
        /// Отправляем текстовое сообщение
        /// </summary>
        /// <param name="userid">Идентификатор пользователя</param>
        /// <param name="message">Сообщение</param>
        private async Task SendText(long userid, string message)
        {
            await bot.SendTextMessageAsync(userid, message);
            var user = await data.GetUsersByUserId(userid);
            await data.AddMessage(new MessagesBot {
                Id = 0,
                MessageText = message,
                MessageCaption = "",
                UserMessageId = user.Id,
                FromIdUser = FromUser,
                CreateDate = DateTime.Now,
                SendType = "To",
                MessgeType = "Text",
                ImageId = 0
            });
        }
        /// <summary>
        /// Отправляем сообщение с изображением
        /// </summary>
        /// <param name="userid">Идентификатор пользователя</param>
        /// <param name="messages">Объект MessagesBot с данными </param>
        private async Task SendImage(long userid, MessagesBot messages)
        {
            Images images = await imgData.GetItemById(messages.ImageId);
            if(images != null)
            {
                string filenames = Path.Combine("Files", images.Location, images.Name);
                using (var stream = File.OpenRead(filenames))
                {
                    await bot.SendPhotoAsync(userid, new InputOnlineFile(stream, stream.Name), caption: messages.MessageCaption);
                }
                await data.AddMessage(messages);
            }
        }
        /// <summary>
        /// Обрабатываем принятое изображение с сообщением
        /// </summary>
        /// <param name="args">Событие</param>
        /// <returns>Модель Images с присвоенныеми данными</returns>
        private async Task<Images> ImageDownload(MessageEventArgs args)
        {
            string fileId = args.Message.Photo[args.Message.Photo.Length - 1].FileId;
            try
            {
                if (args.Message.Photo[0].FileSize > 20971520)
                {
                    await SendText(args.Message.Chat.Id, "Размер файла не должен превышать 20 Мб");
                }
                else
                {
                    string caption = args.Message.Caption != null ? args.Message.Caption : "";
                    string location = "Messages";
                    var file = await bot.GetFileAsync(fileId);
                    string extension = Path.GetExtension(file.FilePath);
                    string name = FileHandler.FileNameGenerator(location, extension);
                    string path = Path.Combine("Files", location, name);
                    string folder = Path.Combine("Files", location);

                    if (!File.Exists(path)) Directory.CreateDirectory(folder);

                    using (var saveImageStream = new FileStream(path, FileMode.Create))
                    {
                        await bot.DownloadFileAsync(file.FilePath, saveImageStream);
                    }



                    return await imgData.Add(location, name, caption, extension);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Загрузка изображения TelegramBot", "ImageDownload", ex);
            }
            return null;
        }
        /// <summary>
        /// Запускаем телеграмм бот
        /// </summary>
        /// <param name="token">Токен</param>
        private void ConnectionBot(string token)
        {
            this.token = token;
            try
            {
                if (bot != null)
                {
                    bot.StopReceiving();
                    data.BotConnection(false, "");
                }
                bot = new TelegramBotClient(token);
                bot.OnMessage += BotHandler;
                bot.StartReceiving();
                var botResult = bot.GetMeAsync(); 

                if(botResult.Result != null)
                {
                    FromUser = botResult.Result.Id;
                    data.BotConnection(true, botResult.Result.FirstName);
                }
                
            }catch(Exception ex)
            {
                Log.Error($"Ошибка запуска телеграмм бота", "ConnectionBot", ex);
                data.BotConnection(false, "");
            }
           
        }
        /// <summary>
        /// Запускаем телеграм бот
        /// </summary>
        private async Task RunTelegram()
        {
            PageView token = await page.Get();
            if (!string.IsNullOrEmpty(token.TelegramToken))
            {
                ConnectionBot(token.TelegramToken);
            }
        }

        public  Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(RunTelegram);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if(bot != null) bot.StopReceiving();
            return Task.CompletedTask;
        }

        
    }
}

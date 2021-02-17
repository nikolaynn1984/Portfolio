using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;
using MessagesData;
using System.IO;
using System.Diagnostics;
using MessagesData.Model;
using System.Collections.ObjectModel;

namespace TelegramClient
{
    public class Client
    {

        internal string token;
        private TelegramBotClient bot;
        private Repository repository;
        private Random random;
        public ObservableCollection<MessagesLog> GetMessages { get; set; }

        public Client(string token)
        {
            this.token = token;
            this.bot = new TelegramBotClient(token);
            this.repository = new Repository();
            this.random = new Random();
            this.GetMessages = repository.GetMessages();
            bot.OnMessage += BotHandler;
            bot.StartReceiving();
        }

        

        /// <summary>
        /// Обработка сообщений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotHandler(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null) { TextHandler(e, e.Message.Text); SaveMessage(e); }
            else FileTypes(e);

            Debug.WriteLine(e.Message.Text);
        }

       

        /// <summary>
        /// Обработчик текстовых сообщений
        /// </summary>
        /// <param name="args"></param>
        /// <param name="messageText"></param>
        public void TextHandler(MessageEventArgs args, string messageText)
        {
            string message = messageText.ToLowerInvariant();
            bool result = true;
            message.ToLower();
            switch (message)
            {
                case "привет":
                    messageText = $"{args.Message.Chat.FirstName}, добро пожаловать в чат \n" + CommandMessage();
                    break;
                case "как дела?":
                    messageText = HowSMessage();
                    break;
                case "как дела":
                    messageText = HowSMessage();
                    break;
                case "фото":
                    SendImage(args); result = false;
                    break;
                case "музыка":
                    SendAudio(args); result = false;
                    break;
                case "хорошо":
                    messageText = "Это хорошо";
                    break;
                case "отлично":
                    messageText = "Я рад";
                    break;
                case "прекрасно":
                    messageText = "Ооууу ";
                    break;
                case "великолепно":
                    messageText = "это прекрасно, я рад ";
                    break;
                default:
                    messageText = CommandMessage();
                    break;
            }
            if (result)
            {
                SendMessage(messageText, args.Message.Chat.Id, args.Message.Chat.FirstName);
            }

        }
        /// <summary>
        /// Текстовое сообщение Комманд Бота
        /// </summary>
        /// <returns>Возвращает сообщение</returns>
        private string CommandMessage()
        {
            return "Ниже описаны команды " +
                   "\n @Привет" +
                   "\n @Как дела? " +
                   "\n @Фото " +
                   "\n @Музыка";
        }
        /// <summary>
        /// ОБработчик команды - Как дела?
        /// </summary>
        /// <returns>Возвращает случайный вариант ответа</returns>
        private string HowSMessage()
        {
            string[] how = { "Отлично", "Хорошо, спасибо что спросил", "Прекрасно", "Хорошо", "Так себе", "Слыхал что в мире творится!?", "Великолепно" };
            string[] you = { "Как сам(а)?", "У тебя как?", "Твои?" };

            int howIndex = random.Next(how.Length);
            int youIndex = random.Next(you.Length);

            string first = how[howIndex];
            string second = you[youIndex];

            return $"{first}, {second}";
        }
        /// <summary>
        /// Метод отправляет изображение
        /// </summary>
        /// <param name="args"></param>
        private async void SendImage(MessageEventArgs args)
        {
            string filenames = "TelegramImage/imge.jpg";
            string cap = "Крутой автомобиль";
            using (var stream = File.OpenRead(filenames))
            {
                await bot.SendPhotoAsync(args.Message.Chat.Id, new InputOnlineFile(stream, stream.Name), caption: cap);
            }
            string type = "Photo";
            SendMessage(args, cap, type, filenames);
        }
        /// <summary>
        /// Метод отправки аудио записи
        /// </summary>
        /// <param name="args"></param>
        private async void SendAudio(MessageEventArgs args)
        {
            string filenames = "TelegramAudio/bon-jovi_runaway.mp3";
            string cap = "Слушать громко";
            using (var stream = File.OpenRead(filenames))
            {
                await bot.SendAudioAsync(args.Message.Chat.Id, new InputOnlineFile(stream, stream.Name), caption: cap);
            }
            string type = "Audio";
            SendMessage(args, cap, type, filenames);
        }

        /// <summary>
        /// Метод обработки типа данных
        /// </summary>
        /// <param name="args"></param>
        public string FileTypes(MessageEventArgs args)
        {
            string filename = String.Empty;
            DateTime date = DateTime.Now;
            string dateString = date.ToString("dd.MM.yyyy_HH.mm.ss");
            string time = DateTime.Now.ToString();
            String.Format("{0:d dd ddd dddd}", time);
            switch (args.Message.Type)
            {
                case Telegram.Bot.Types.Enums.MessageType.Audio:
                    filename = $"TelegramAudio/MUS{dateString}.mp3";
                    AudioHandler(args, filename);
                    break;
                case Telegram.Bot.Types.Enums.MessageType.Document:
                    filename = $"TelegramDocument/{args.Message.Document.FileName}";
                    DocumentHandler(args, filename);
                    break;
                case Telegram.Bot.Types.Enums.MessageType.Photo:
                    filename = $"TelegramImage/IMAGE{dateString}.jpg";
                    ImageHandler(args, filename);
                    break;
                case Telegram.Bot.Types.Enums.MessageType.Video:
                    filename = $"TelegramVideo/video{dateString}.mpeg";
                    break;
            }
            return filename;
        }

        /// <summary>
        /// Метод обработки входящих документов
        /// </summary>
        /// <param name="args"></param>
        private void DocumentHandler(MessageEventArgs args, string name)
        {
            if (args.Message.Document.FileSize > 20971520)
            {
                SendMessage(args, "Размер файла не должен превышать 20 Мб", "");
            }
            else
            {
                DocumentDownLoad(args, name);

            }
        }
        /// <summary>
        /// Метод обработки входящих изображений
        /// </summary>
        /// <param name="args"></param>
        private void ImageHandler(MessageEventArgs args, string name)
        {
            if (args.Message.Photo[0].FileSize > 20971520)
            {
                SendMessage(args, "Размер файла не должен превышать 20 Мб", "");
            }
            else
            {
                ImageDownLoad(args, name);
            }
        }


        /// <summary>
        /// Метод обработки Айдио файла
        /// </summary>
        /// <param name="args"></param>
        private void AudioHandler(MessageEventArgs args, string name)
        {
            if (args.Message.Audio.FileSize > 20971520)
            {
                SendMessage(args, "Размер файла не должен превышать 20 Мб", "");
            }
            else
            {
                DocumentDownLoad(args, name);
            }
        }
        /// <summary>
        /// Скачивает документа
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="path"></param>
        private async void DocumentDownLoad(MessageEventArgs args, string path)
        {
            var file = await bot.GetFileAsync(args.Message.Audio.FileId);
            using (var fileStream = File.OpenWrite(path))
            {
                await bot.DownloadFileAsync(file.FilePath, fileStream);
            }
            SendMessage(args, "Спасибо, принято", path);
        }

        /// <summary>
        /// Загружает отправленое изображение в папку
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="path"></param>
        private async void ImageDownLoad(MessageEventArgs args, string path)
        {
            string fileId = args.Message.Photo[args.Message.Photo.Length - 1].FileId;
            try
            {
                var file = await bot.GetFileAsync(fileId);

                using (var saveImageStream = new FileStream(path, FileMode.Create))
                {
                    await bot.DownloadFileAsync(file.FilePath, saveImageStream);
                   // await bot.GetInfoAndDownloadFileAsync();
                }
                
                SendMessage(args, "Спасибо, принято", path);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Ошибка загрузки: " + ex.Message);
            }
        }
       

        /// <summary>
        /// Метод отправки сообщения клиенту и добавления в коллекцию
        /// </summary>
        /// <param name="args"></param>
        /// <param name="message"></param>
        public async void SendMessage(MessageEventArgs args, string message, string filename)
        {
            try
            {
                await bot.SendTextMessageAsync(args.Message.Chat.Id, message);
                MessagesLog messages = new MessagesLog()
                {
                    UserId = args.Message.Chat.Id,
                    Name = args.Message.Chat.FirstName,
                    MessageType = args.Message.Type.ToString(),
                    MessageText = message,
                    MessageCaption = args.Message.Caption,
                    FileName = filename,
                    FromId = 0
                };
                messages.Create(messages);

            }
            catch
            {
                Debug.WriteLine("Не удалось подключиться к серверу");

            }
        }
        /// <summary>
        /// Метод отправки сообщения клиенту и добавления в коллекцию
        /// </summary>
        /// <param name="args"></param>
        /// <param name="message"></param>
        public async void SendMessage(MessageEventArgs args, string message)
        {
            try
            {
                await bot.SendTextMessageAsync(args.Message.Chat.Id, message);
                MessagesLog messages = new MessagesLog()
                {
                    UserId = args.Message.Chat.Id,
                    Name = args.Message.Chat.FirstName,
                    MessageType = args.Message.Type.ToString(),
                    MessageText = message,
                    MessageCaption = args.Message.Caption,
                    FileName = "",
                    FromId = 0
                };
                messages.Create(messages);

            }
            catch
            {
                Debug.WriteLine("Не удалось подключиться к серверу");
            }

        }
        /// <summary>
        /// Сохранение сообщения от клиента
        /// </summary>
        /// <param name="args"></param>
        private void SaveMessage(MessageEventArgs args)
        {
            try
            {
                MessagesLog messages = new MessagesLog()
                {
                    UserId = args.Message.Chat.Id,
                    Name = args.Message.Chat.FirstName,
                    MessageType = args.Message.Type.ToString(),
                    MessageText = args.Message.Text,
                    MessageCaption = args.Message.Caption,
                    FileName = "",
                    FromId = 0
                };
                

                messages.Create(messages);

            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Метод отправки сообщения клиенту и добавления в коллекцию
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Id"></param>
        /// <param name="nick"></param>
        public async void SendMessage(string Text, long UserId, string nick)
        {
            try
            {
                await bot.SendTextMessageAsync(UserId, Text);
                MessagesLog messages = new MessagesLog()
                {
                    UserId = UserId,
                    Name = nick,
                    MessageType = "Text",
                    MessageText = Text,
                    MessageCaption = "",
                    FileName = "",
                    FromId = 487665335
                };
                messages.Create(messages);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }

        }
        /// <summary>
        /// Метод добавления сообщения в коллекцию
        /// </summary>
        /// <param name="args"></param>
        /// <param name="caption"></param>
        /// <param name="type"></param>
        /// <param name="filename"></param>
        public void SendMessage(MessageEventArgs args, string caption, string type, string filename)
        {
            try
            {
                MessagesLog messages = new MessagesLog()
                {
                    UserId = args.Message.Chat.Id,
                    Name = args.Message.Chat.FirstName,
                    MessageType = type,
                    MessageText = args.Message.Text,
                    MessageCaption = caption,
                    FileName = filename,
                    FromId = args.Message.From.Id
                };
                messages.Create(messages);
            }
            catch
            {
                Debug.WriteLine("Не удалось подключиться к серверу");
            }

        }

    }
}

using MessagesData.Model;
using System.Windows;
using System.Windows.Controls;

namespace MessageBotTelegram.UI
{
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate messageClien { get; set; }
        public DataTemplate messageAdmin { get; set; }
        public DataTemplate imageAdmin { get; set; }
        public DataTemplate imageClient { get; set; }
        public DataTemplate audioAdmin { get; set; }
        public DataTemplate audioClient { get; set; }
        public DataTemplate videoAdmin { get; set; }
        public DataTemplate videoClient { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            MessagesLog mes = (MessagesLog)item;

            if (mes.FromId != 0)
            {
                if (mes.MessageType == "Photo")
                {
                    return imageAdmin;
                }
                else if (mes.MessageType == "Audio")
                {
                    return audioAdmin;
                }
                else if(mes.MessageType == "Video")
                {
                    return videoAdmin;
                }
                else
                {
                    return messageAdmin;
                }

            }
            else
            {
                if (mes.MessageType == "Photo")
                {
                    return imageClient;
                }
                else if (mes.MessageType == "Audio")
                {
                    return audioClient;
                }
                else if (mes.MessageType == "Video")
                {
                    return videoClient;
                }
                else
                {
                    return messageClien;
                }
            }

        }
    }
}

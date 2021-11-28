using Landing.Model.Data;
using System.Windows;
using System.Windows.Controls;

namespace Landing.Win.UI
{
    /// <summary>
    /// Обработка представления в телеграмм боте
    /// </summary>
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate messageClien { get; set; }
        public DataTemplate messageAdmin { get; set; }
        public DataTemplate imageAdmin { get; set; }
        public DataTemplate imageClient { get; set; }
        /// <summary>
        /// Возвращаяем информацию о отображаемом Template в данном поле
        /// </summary>
        /// <param name="item">Информация</param>
        /// <param name="container">Контейнер</param>
        /// <returns>Имя Template</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            MessagesBot mes = (MessagesBot)item;
            DataTemplate template = new DataTemplate();

            if(mes.SendType == "To")
            {
                switch (mes.MessgeType)
                {
                    case "Photo": template = imageAdmin; break;
                    case "Text": template = messageAdmin; break;
                }
            }
            else
            {
                switch (mes.MessgeType)
                {
                    case "Photo": template =  imageClient; break;
                    case "Text": template = messageClien; break;
                }
            }
            return template;
        }
    }
}

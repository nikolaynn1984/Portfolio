using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Landing.Model.Data;

namespace Landing.Win.UI
{
    /// <summary>
    /// Конвертер иконки
    /// </summary>
    public class IconPathConverter : IValueConverter
    {
        private string imageDirectory = Directory.GetCurrentDirectory();
        public string ImageDirectory
        {
            get { return imageDirectory; }
            set { imageDirectory = value; }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return new Uri("pack://application:,,,/Images/ic_add_black.png");
            }
            var images = (Images)value;
           
            string imagePath = Path.Combine(ImageDirectory,"Files", images.Location, images.Name);
            bool result = File.Exists(imagePath);

            if (result)
            {
                try
                {
                    return new BitmapImage(new Uri(imagePath));
                }catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                    Repository.Failt.ErrorsHandler.AddMessage(e.Message);
                    return imagePath;
                }
                
            }
            else
            {
                return imagePath;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

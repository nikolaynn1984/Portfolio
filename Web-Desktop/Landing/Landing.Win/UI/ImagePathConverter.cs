using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Landing.Library.Model;

namespace Landing.Win.UI
{
    /// <summary>
    /// Конвертер изображения для представления
    /// </summary>
    public class ImagePathConverter : IValueConverter
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
                return null;
            }
            var images = (Images)value;

            if(string.IsNullOrEmpty(images.Location) && string.IsNullOrEmpty(images.Name))
            {
                return null;
            }
           
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

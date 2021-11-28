using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Landing.Win.Helper
{
    public class ViewModelHelper : INotifyPropertyChanged, IDataErrorInfo
    {
        protected string path;

        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; OnPropertyChanged(nameof(this.Name)); }
        }
        private string description;
        public string Description
        {
            get { return this.description; }
            set { this.description = value; OnPropertyChanged(nameof(this.Description)); }
        }

        private string headtext;
        public string HeadText
        {
            get { return this.headtext; }
            set { this.headtext = value; OnPropertyChanged(nameof(this.HeadText)); }
        }

        private Visibility listvisible;
        public Visibility ListVisible
        {
            get { return this.listvisible; }
            set { this.listvisible = value; OnPropertyChanged(nameof(this.ListVisible)); }
        }

        private Visibility editvisible;
        public Visibility EditVisivle
        {
            get { return this.editvisible; }
            set { this.editvisible = value; OnPropertyChanged(nameof(this.EditVisivle)); }
        }

        private bool savebuttonenabled;
        public bool SaveButtonEnabled
        {
            get { return this.savebuttonenabled; }
            set { this.savebuttonenabled = value; OnPropertyChanged(nameof(this.SaveButtonEnabled)); }
        }

        private BitmapImage image;
        public BitmapImage Image
        {
            get { return this.image; }
            set { this.image = value; OnPropertyChanged(nameof(this.Image)); }
        }

        public string Error { get { return null; } }
        /// <summary>
        /// Проверка валидации данных
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Name": if (string.IsNullOrEmpty(Name)) { error = $"Поле не должно быть пустым"; } break;
                    case "Description": if (string.IsNullOrEmpty(Description)) { error = $"Поле не должно быть пустым"; } break;
                    default:
                        break;
                }
                if (string.IsNullOrEmpty(error)) { SaveButtonEnabled = true; }
                else { SaveButtonEnabled = false; }

                return error;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

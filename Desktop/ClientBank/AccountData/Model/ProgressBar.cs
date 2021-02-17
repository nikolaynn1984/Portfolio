using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccountData.Model
{
    public class ProgressBar : INotifyPropertyChanged
    {
        public static event Action<ProgressBar> ChangedValue;

        public ProgressBar(){}
        public ProgressBar(int MinValue, int MaxValue, int CurrentValue, string ProgressName, bool ButtonState)
        {
            this.MinValue = MinValue;
            this.MaxValue = MaxValue;
            this.CurrentValue = CurrentValue;
            this.ProgressName = ProgressName;
            this.ButtonState = ButtonState;
        }

        public static void GetValue(string name, int max, int coun)
        {
            ProgressBar progress = new ProgressBar();

            progress.MinValue = 0;
            progress.MaxValue = max;
            if (max != coun)
            {
                progress.CurrentValue = coun;
                progress.ProgressName = name;
                progress.ButtonState = false;
            }
            else 
            {
                progress.CurrentValue = 0;
                progress.ProgressName = "";
                progress.ButtonState = true;
            }

            

            ChangedValue?.Invoke(progress);
        }

        public int MinValue
        {
            get { return this.minvalue; }
            set { this.minvalue = value; OnPropertyChanged(nameof(this.MinValue)); }
        }

        public int MaxValue
        {
            get { return this.maxvalue; }
            set { this.maxvalue = value; OnPropertyChanged(nameof(this.MaxValue)); }
        }

        public int CurrentValue
        {
            get { return this.currentvalue; }
            set { this.currentvalue = value; OnPropertyChanged(nameof(this.CurrentValue)); }
        }

        public string ProgressName
        {
            get { return this.progressname; }
            set { this.progressname = value; OnPropertyChanged(nameof(this.ProgressName)); }
        }

        public bool ButtonState
        {
            get { return this.buttonstate; }
            set { this.buttonstate = value; OnPropertyChanged(nameof(this.ButtonState)); }
        }

        private int minvalue;

        private int maxvalue;

        private int currentvalue;

        private string progressname;

        private bool buttonstate;


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prog = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prog));
        }
    }
}

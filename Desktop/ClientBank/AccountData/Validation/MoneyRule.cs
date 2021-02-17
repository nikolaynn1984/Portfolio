using System;
using System.Globalization;
using System.Windows.Controls;

namespace AccountData.Validation
{
    public class MoneyRule : ValidationRule
    {


        private decimal min = 0;
        private decimal max = Decimal.MaxValue;

        public decimal Min
        {
            get { return this.min; }
            set { this.min = value; }
        }
        public decimal Max
        {
            get { return this.max; }
            set { this.max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal price = 0;
            try
            {
                if (((string)value).Length > 0)
                    price = Decimal.Parse((string)value, NumberStyles.Any, cultureInfo);
            }
            catch (FormatException e)
            {
                return new ValidationResult(false, $"{e.Message}");
            }
            catch
            {
                return new ValidationResult(false, "Недопустимые символы.");
            }
            if ((price < Min) || (price > Max))
            {
                return new ValidationResult(false, $"Значение не должно быть меньше {Min} и больше {Max}");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}

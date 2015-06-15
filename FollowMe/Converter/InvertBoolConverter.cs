using System;
using System.Globalization;
using System.Windows.Data;

namespace FollowMe.Converter
{
    [ValueConversion(typeof(bool), typeof(bool))]   
    
    public class InvertBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var boolValue = Boolean.Parse(value.ToString());
            return !boolValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var boolValue = Boolean.Parse(value.ToString());
            return !boolValue;
        }
    }
}

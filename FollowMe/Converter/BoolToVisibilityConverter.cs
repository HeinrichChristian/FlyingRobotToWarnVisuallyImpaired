using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FollowMe.Converter
{
    [ValueConversion(typeof(bool), typeof(Visibility))]   
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && parameter.ToString() == "VisibleOnFalse")
            {
                value = !(value as bool?);
            }

            if (!(value is bool))
                return Visibility.Collapsed;
        
            bool objValue = (bool)value;
            if (objValue)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

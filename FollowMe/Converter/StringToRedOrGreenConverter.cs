using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace FollowMe.Converter
{
   [ValueConversion(typeof(string), typeof(Brushes))]   
  
    public class StringToRedOrGreenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null ||  string.IsNullOrEmpty(value.ToString()))
            {
                return Brushes.Red;
            }
            
            return Brushes.LawnGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

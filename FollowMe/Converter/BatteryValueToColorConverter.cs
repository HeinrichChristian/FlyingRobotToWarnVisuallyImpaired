using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FollowMe.Converter
{
    [ValueConversion(typeof(int), typeof(Brushes))]   
    public class BatteryValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int batteryValue = Int32.Parse(value.ToString());
            if (batteryValue < 17)
            {
                return Brushes.Red;
            }
            else if(batteryValue < 40)
            {
                return Brushes.Orange;
            }
            return Brushes.LawnGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

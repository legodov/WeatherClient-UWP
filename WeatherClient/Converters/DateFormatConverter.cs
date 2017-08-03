using System;
using Windows.UI.Xaml.Data;

namespace WeatherClient.Converters
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            DateTime dt = DateTime.Parse(value.ToString());
            return dt.ToString("dd/MM/yyyy");
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            return DateTime.Parse(value.ToString());
        }
    }
}

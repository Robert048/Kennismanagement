using System;
using Windows.UI.Xaml.Data;

namespace WorQit.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                DateTime dt = DateTime.Parse(value.ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
                parameter = parameter.ToString().Substring(1, parameter.ToString().IndexOf("}") - 1);
                string returnString = dt.ToString(parameter.ToString());
                return returnString;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

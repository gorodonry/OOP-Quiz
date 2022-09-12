using System;
using System.Globalization;
using System.Windows.Data;

namespace OOPQuiz.Core.Converters
{
    /// <summary>
    /// Returns the inverse value of a given boolean.
    /// </summary>
    public class BooleanNegator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value;
        }
    }
}

using System.Windows.Data;
using System;
using System.Globalization;
using OOPQuiz.Core.Models;

namespace OOPQuiz.Core.Converters
{
    /// <summary>
    /// Converts a given <see cref="TimeSpan"/> to a string.
    /// </summary>
    /// <remarks>
    /// Conversion back is not supported.
    /// </remarks>
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Methods.ConvertTimeSpanToString((TimeSpan)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

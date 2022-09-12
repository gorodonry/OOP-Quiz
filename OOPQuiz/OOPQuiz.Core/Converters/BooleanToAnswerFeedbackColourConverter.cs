using System;
using System.Globalization;
using System.Windows.Data;

namespace OOPQuiz.Core.Converters
{
    /// <summary>
    /// Converts a boolean to a colour indicating whether or not the user got the answer correct.
    /// </summary>
    /// <remarks>
    /// Conversion back is not supported.
    /// </remarks>
    public class BooleanToAnswerFeedbackColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool?)value is null)
            {
                return "#909090";
            }
            else if ((bool)value)
            {
                return "#228B22";
            }
            else
            {
                return "#FF0000";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

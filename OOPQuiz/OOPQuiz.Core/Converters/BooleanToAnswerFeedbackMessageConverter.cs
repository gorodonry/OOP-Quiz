using System;
using System.Globalization;
using System.Windows.Data;

namespace OOPQuiz.Core.Converters
{
    /// <summary>
    /// Converts a boolean to a message indicating whether or not the user got the answer correct.
    /// </summary>
    /// <remarks>
    /// Conversion back is not supported.
    /// </remarks>
    public class BooleanToAnswerFeedbackMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool?)value is null)
            {
                return string.Empty;
            }
            else if ((bool)value)
            {
                return "Correct";
            }
            else
            {
                return "Incorrect";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

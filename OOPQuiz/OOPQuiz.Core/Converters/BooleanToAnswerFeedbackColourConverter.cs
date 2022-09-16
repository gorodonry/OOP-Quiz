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
            switch ((bool?)value)
            {
                case null:
                    // If the user has not yet answered, return grey.
                    return "#909090";

                case true:
                    // If the question has been correctly answered, return green.
                    return "#228B22";

                case false:
                    // If the question has been incorrectly answered, return red.
                    return "#FF0000";

                default:
                    throw new ArgumentException("Value entered was not a boolean");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

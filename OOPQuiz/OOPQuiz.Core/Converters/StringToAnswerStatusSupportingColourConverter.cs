using System.Globalization;
using System;
using System.Windows.Data;

namespace OOPQuiz.Core.Converters
{
    /// <summary>
    /// Returns a supporting colour indicating the answer status of the question.
    /// </summary>
    /// <remarks>
    /// Conversion back is not supported. Should be used in conjunction with <see cref="StringToAnswerStatusColourConverter"/>.
    /// </remarks>
    public class StringToAnswerStatusSupportingColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "Selected":
                    // If the question has been selected, return grey.
                    return "#909090";

                case "True":
                    // If the question has been correctly answered, return dark green.
                    return "#186218";

                case "False":
                    // If the question has been incorrectly answered, return dark red.
                    return "#B30000";

                default:
                    // If none of the above apply, return white.
                    return "#FFFFFF";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

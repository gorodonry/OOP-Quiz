using System;
using System.Globalization;
using System.Windows.Data;

namespace OOPQuiz.Core.Converters
{
    /// <summary>
    /// Returns a colour indicating the answer status of the question.
    /// </summary>
    /// <remarks>
    /// Conversion back is not supported.
    /// </remarks>
    public class StringToAnswerStatusColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "Selected":
                    // If the question has been selected, return grey.
                    return "#909090";

                case "True":
                    // If the question has been correctly answered, return green.
                    if (parameter is null)
                        return "#00FF00";

                    if (parameter.ToString() == "DarkerColour")
                        return "#228B22";

                    return "#00FF00";

                case "False":
                    // If the question has been incorrectly answered, return red.
                    return "#FF0000";

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

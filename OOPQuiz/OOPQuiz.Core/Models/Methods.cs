using System;

namespace OOPQuiz.Core.Models
{
    /// <summary>
    /// Project unique methods.
    /// </summary>
    /// <remarks>
    /// Accessible to the entire project.
    /// </remarks>
    public static class Methods
    {
        /// <summary>
        /// Makes the first character of a string upper case.
        /// </summary>
        /// <param name="str">The string to be capitalised.</param>
        /// <returns>
        /// The string with the first character converted to upper case.
        /// </returns>
        /// <remarks>
        /// String should be trimmed before parsed into this method.
        /// </remarks>
        public static string Capitalise(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else if (str.Length == 1)
            {
                return str.ToUpper();
            }

            return char.ToUpper(str[0]).ToString() + string.Join("", new ArraySegment<char>(str.ToCharArray(), 1, str.Length - 1));
        }
    }
}

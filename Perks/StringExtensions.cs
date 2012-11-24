using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Perks
{
    /// <summary>
    /// Contains common extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns string only if it has value. Can be used with ?? operator. 
        /// </summary>
        /// <param name="source">Source string. </param>
        /// <returns>Returns string if it's not empty or null.</returns>
        public static string IfNotNullOrEmpty(this string source)
        {
            return !string.IsNullOrEmpty(source) ? source : null;
        }

        /// <summary>
        /// Returns result of expression with string if it's not null or empty. Can be used with ?? operator.
        /// </summary>
        /// <typeparam name="TOut">The type of the result.</typeparam>
        /// <param name="source">Source string.</param>
        /// <param name="result">The expression to get the result.</param>
        /// <returns>Result of expression if string is not null or empty; otherwise null.</returns>
        public static TOut IfNotNullOrEmpty<TOut>(this string source, Func<string, TOut> result)
        {
            return !string.IsNullOrEmpty(source) ? result(source) : default(TOut);
        }

        /// <summary>
        /// Determines whether specified string contains another string.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="toCheck">The string to check for containing.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <returns>
        /// <c>true</c> if specified string contains another string; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string source, string toCheck, StringComparison comparisonType)
        {
            Ensure.ArgumentNotNull(source, "source");

            return source.IndexOf(toCheck, comparisonType) >= 0;
        }

        /// <summary>
        /// Returns equivalent of the specified string with each word starting from a character in the upper case.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>Equivalent of the specified string with each word starting from a character in the upper case.</returns>
        public static string ToTitleCase(this string source)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(source);
        }

        /// <summary>
        /// Converts specified string to the camel cased string.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>Camel cased string.</returns>
        public static string ToCamelCase(this string source)
        {
            Ensure.ArgumentNotNull(source, "source");

            if (source == string.Empty || !char.IsUpper(source.First()))
            {
                return source;
            }

            var camelCased = char.ToLower(source.First()).ToString();

            if (source.Length > 1)
            {
                camelCased += source.Substring(1);
            }

            return camelCased;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to <see cref="SecureString"/>.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns><see cref="SecureString"/> representing the specified string.</returns>
        public static SecureString ToSecureString(this string source)
        {
            Ensure.ArgumentNotNull(source, "source");

            var result = new SecureString();

            foreach (var ch in source)
            {
                result.AppendChar(ch);
            }

            return result;
        }

        /// <summary>
        /// Converts specified <see cref="SecureString"/> object to not secure <see cref="string"/>.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns><see cref="string"/> taken from the specified <see cref="SecureString"/>.</returns>
        public static string ToInsecureString(this SecureString source)
        {
            var bstr = Marshal.SecureStringToBSTR(source);

            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }
    }
}

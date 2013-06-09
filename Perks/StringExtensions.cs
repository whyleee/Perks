using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace Perks
{
    /// <summary>
    /// Contains common extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether the specified string is null or empty.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified string is null or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// Determines whether the specified string is not null or empty.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified string is not null or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNotNullOrEmpty(this string source)
        {
            return !string.IsNullOrEmpty(source);
        }

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
        /// Cuts the string to the specified maximum count of characters, not splitting the words.
        /// </summary>
        /// <param name="text">The text to cut.</param>
        /// <param name="maxChars">The maximum characters for the result text.</param>
        /// <param name="with">The string to insert to the place of cut.</param>
        /// <returns>Cut string to the specified maximum count of characters.</returns>
        public static string CutTo(this string text, int maxChars, string with = "")
        {
            if (text.IsNullOrEmpty())
            {
                return text;
            }

            var cut = new string(text.Take(maxChars).ToArray());

            if (cut.Length < text.Length)
            {
                if (text[maxChars] != ' ')
                {
                    var lastSpace = cut.LastIndexOf(' ');

                    if (lastSpace != -1)
                    {
                        cut = cut.Substring(0, lastSpace);
                    }
                }

                cut += with;
            }

            return cut;
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

        /// <summary>
        /// Converts a camel-cased or pascal-cased string to ordinary readable text.
        /// </summary>
        /// <param name="text">The text to convert.</param>
        /// <returns>Readable string converted from camel-cased or pascal-cased string.</returns>
        public static string ToFriendlyString(this string text)
        {
            return Regex.Replace(text, @"([a-z](?=[A-Z]|\d)|[A-Z](?=[A-Z][a-z]|\d)|\d(?=[A-Z][a-z]))", "$1 ");
        }

        /// <summary>
        /// Converts a string to a URL-encoded string.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>An encoded string.</returns>
        public static string UrlEncode(this string value)
        {
            return WebUtility.UrlEncode(value);
        }

        /// <summary>
        /// Converts a string that has been URL-encoded for HTTP transmission into a decoded string.
        /// </summary>
        /// <param name="value">The string to decode.</param>
        /// <returns>A decoded string.</returns>
        public static string UrlDecode(this string value)
        {
            return WebUtility.UrlDecode(value);
        }

        /// <summary>
        /// Converts a string to an HTML-encoded string.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns>An encoded string.</returns>
        public static string HtmlEncode(this string value)
        {
            return WebUtility.HtmlEncode(value);
        }

        /// <summary>
        /// Converts a string that has been HTML-encoded for HTTP transmission into a decoded string.
        /// </summary>
        /// <param name="value">The string to decode.</param>
        /// <returns>A decoded string.</returns>
        public static string HtmlDecode(this string value)
        {
            return WebUtility.HtmlDecode(value);
        }
    }
}

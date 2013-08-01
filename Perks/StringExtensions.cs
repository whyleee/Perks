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
#if NET40
            return WebUtility_UrlEncode(value);
#else
            return WebUtility.UrlEncode(value);
#endif
        }

        /// <summary>
        /// Converts a string that has been URL-encoded for HTTP transmission into a decoded string.
        /// </summary>
        /// <param name="value">The string to decode.</param>
        /// <returns>A decoded string.</returns>
        public static string UrlDecode(this string value)
        {
#if NET40
            return WebUtility_UrlDecode(value);
#else
            return WebUtility.UrlDecode(value);
#endif
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

#if NET40
        private static string WebUtility_UrlEncode(string value)
        {
            if (value == null)
            {
                return null;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return Encoding.UTF8.GetString(WebUtility_UrlEncode(bytes, 0, bytes.Length, false));
        }

        private static byte[] WebUtility_UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
        {
            byte[] array = WebUtility_UrlEncode(bytes, offset, count);
            if (!alwaysCreateNewReturnValue || array == null || array != bytes)
            {
                return array;
            }
            return (byte[])array.Clone();
        }

        private static byte[] WebUtility_UrlEncode(byte[] bytes, int offset, int count)
        {
            if (!WebUtility_ValidateUrlEncodingParameters(bytes, offset, count))
            {
                return null;
            }
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < count; i++)
            {
                char c = (char)bytes[offset + i];
                if (c == ' ')
                {
                    num++;
                }
                else
                {
                    if (!WebUtility_IsUrlSafeChar(c))
                    {
                        num2++;
                    }
                }
            }
            if (num == 0 && num2 == 0)
            {
                return bytes;
            }
            byte[] array = new byte[count + num2 * 2];
            int num3 = 0;
            for (int j = 0; j < count; j++)
            {
                byte b = bytes[offset + j];
                char c2 = (char)b;
                if (WebUtility_IsUrlSafeChar(c2))
                {
                    array[num3++] = b;
                }
                else
                {
                    if (c2 == ' ')
                    {
                        array[num3++] = 43;
                    }
                    else
                    {
                        array[num3++] = 37;
                        array[num3++] = (byte)WebUtility_IntToHex(b >> 4 & 15);
                        array[num3++] = (byte)WebUtility_IntToHex((int)(b & 15));
                    }
                }
            }
            return array;
        }

        private static string WebUtility_UrlDecode(string encodedValue)
        {
            if (encodedValue == null)
            {
                return null;
            }
            return WebUtility_UrlDecodeInternal(encodedValue, Encoding.UTF8);
        }

        private static string WebUtility_UrlDecodeInternal(string value, Encoding encoding)
        {
            if (value == null)
            {
                return null;
            }
            int length = value.Length;
            WebUtility_UrlDecoder urlDecoder = new WebUtility_UrlDecoder(length, encoding);
            int i = 0;
            while (i < length)
            {
                char c = value[i];
                if (c == '+')
                {
                    c = ' ';
                    goto IL_77;
                }
                if (c != '%' || i >= length - 2)
                {
                    goto IL_77;
                }
                int num = WebUtility_HexToInt(value[i + 1]);
                int num2 = WebUtility_HexToInt(value[i + 2]);
                if (num < 0 || num2 < 0)
                {
                    goto IL_77;
                }
                byte b = (byte)(num << 4 | num2);
                i += 2;
                urlDecoder.AddByte(b);
            IL_91:
                i++;
                continue;
            IL_77:
                if ((c & 'ﾀ') == '\0')
                {
                    urlDecoder.AddByte((byte)c);
                    goto IL_91;
                }
                urlDecoder.AddChar(c);
                goto IL_91;
            }
            return urlDecoder.GetString();
        }

        private static bool WebUtility_ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
        {
            if (bytes == null && count == 0)
            {
                return false;
            }
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (offset < 0 || offset > bytes.Length)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (count < 0 || offset + count > bytes.Length)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            return true;
        }

        private static bool WebUtility_IsUrlSafeChar(char ch)
        {
            if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
            {
                return true;
            }
            if (ch != '!')
            {
                switch (ch)
                {
                    case '(':
                    case ')':
                    case '*':
                    case '-':
                    case '.':
                        return true;
                    case '+':
                    case ',':
                        break;
                    default:
                        if (ch == '_')
                        {
                            return true;
                        }
                        break;
                }
                return false;
            }
            return true;
        }

        private static char WebUtility_IntToHex(int n)
        {
            if (n <= 9)
            {
                return (char)(n + 48);
            }
            return (char)(n - 10 + 65);
        }

        private static int WebUtility_HexToInt(char h)
        {
            if (h >= '0' && h <= '9')
            {
                return (int)(h - '0');
            }
            if (h >= 'a' && h <= 'f')
            {
                return (int)(h - 'a' + '\n');
            }
            if (h < 'A' || h > 'F')
            {
                return -1;
            }
            return (int)(h - 'A' + '\n');
        }

        private class WebUtility_UrlDecoder
        {
            private int _bufferSize;
            private int _numChars;
            private char[] _charBuffer;
            private int _numBytes;
            private byte[] _byteBuffer;
            private Encoding _encoding;
            private void FlushBytes()
            {
                if (this._numBytes > 0)
                {
                    this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
                    this._numBytes = 0;
                }
            }
            internal WebUtility_UrlDecoder(int bufferSize, Encoding encoding)
            {
                this._bufferSize = bufferSize;
                this._encoding = encoding;
                this._charBuffer = new char[bufferSize];
            }
            internal void AddChar(char ch)
            {
                if (this._numBytes > 0)
                {
                    this.FlushBytes();
                }
                this._charBuffer[this._numChars++] = ch;
            }
            internal void AddByte(byte b)
            {
                if (this._byteBuffer == null)
                {
                    this._byteBuffer = new byte[this._bufferSize];
                }
                this._byteBuffer[this._numBytes++] = b;
            }
            internal string GetString()
            {
                if (this._numBytes > 0)
                {
                    this.FlushBytes();
                }
                if (this._numChars > 0)
                {
                    return new string(this._charBuffer, 0, this._numChars);
                }
                return string.Empty;
            }
        }
#endif
    }
}

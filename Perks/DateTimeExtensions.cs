using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perks
{
    /// <summary>
    /// Contains extension methods for <see cref="DateTime" />.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts the <see cref="DateTime" /> to the Unix time stamp (count of seconds since January 1, 1970).
        /// </summary>
        /// <param name="time">The <see cref="DateTime" /> to convert.</param>
        /// <returns>Unix time stamp (count of seconds since January 1, 1970).</returns>
        public static long ToUnixTime(this DateTime time)
        {
            return (long) (time - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        /// <summary>
        /// Converts the <see cref="DateTime" /> to the precise Unix time stamp (count of milliseconds since January 1, 1970).
        /// </summary>
        /// <param name="time">The <see cref="DateTime" /> to convert.</param>
        /// <returns>Precise Unix time stamp (count of milliseconds since January 1, 1970).</returns>
        public static long ToUnixMilliTime(this DateTime time)
        {
            return (long) (time - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        /// <summary>
        /// Converts the <see cref="long" /> value with Unix time stamp (count of seconds since January 1, 1970) to <see cref="DateTime" />
        /// </summary>
        /// <param name="timestamp">The <see cref="long" /> with Unix time stamp.</param>
        /// <returns><see cref="DateTime" /> representing the specified Unix time stamp.</returns>
        public static DateTime FromUnixTime(this long timestamp)
        {
            return new DateTime(1970, 1, 1).AddSeconds(timestamp);
        }

        /// <summary>
        /// Converts the <see cref="long" /> value with precise Unix time stamp (count of milliseconds since January 1, 1970) to <see cref="DateTime" />
        /// </summary>
        /// <param name="timestamp">The <see cref="long" /> with precise Unix time stamp.</param>
        /// <returns><see cref="DateTime" /> representing the specified precise Unix time stamp.</returns>
        public static DateTime FromUnixMilliTime(this long timestamp)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(timestamp);
        }
    }
}

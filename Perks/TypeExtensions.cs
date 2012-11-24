using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perks
{
    /// <summary>
    /// Extension methods for <see cref="Type"/> objects.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether type is simple (primitive, predefined or date/time).
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the type is simple; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSimpleType(this Type type)
        {
            return type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type == typeof(decimal) || type == typeof(Guid) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan);
        }
    }
}

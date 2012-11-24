using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perks
{
    /// <summary>
    /// Contains common extension methods for all objects.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Performs specified action on the object if it's not null.
        /// </summary>
        /// <typeparam name="T">The type of the source object.</typeparam>
        /// <param name="source">Source object.</param>
        /// <param name="action">The action to perform.</param>
        public static void IfNotNull<T>(this T source, Action<T> action) where T : class
        {
            if (source != null) action(source);
        }

        /// <summary>
        /// Returns object only if it has value. Can be used with ?? operator.
        /// </summary>
        /// <typeparam name="TIn">The type of the source object.</typeparam>
        /// <typeparam name="TOut">The type of the result.</typeparam>
        /// <param name="source">Source object.</param>
        /// <param name="result">The expression to get the result.</param>
        /// <returns>Returns object if it's not empty or null.</returns>
        public static TOut IfNotNull<TIn, TOut>(this TIn source, Func<TIn, TOut> result) where TIn : class
        {
            return source != null ? result(source) : default(TOut);
        }
    }
}

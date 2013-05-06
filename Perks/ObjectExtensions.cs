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

        /// <summary>
        /// Returns object only if assertion is passed. Usefull with ?? operator.
        /// </summary>
        /// <typeparam name="T">The type of the source object.</typeparam>
        /// <param name="source">The source object.</param>
        /// <param name="assert">The assertion for the object.</param>
        /// <returns>Returns object if assertion is passed or default value otherwise.</returns>
        /// <example>
        /// Here is an example of usage:
        /// <code>
        /// var image = topImage.If(x => x.Src.IsNotNullOrEmpty()) ?? fallbackImage;
        /// </code>
        /// </example>
        public static T If<T>(this T source, Func<T, bool> assert) where T : class
        {
            return source != null && assert(source) ? source : default(T);
        }

        /// <summary>
        /// Returns the object if it is of the specified type or <c>null</c> otherwise.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="source">The source object.</param>
        /// <returns>The object itself if it's of the specified type of <c>null</c> otherwise.</returns>
        public static T If<T>(this object source) where T : class
        {
            return source is T ? (T)source : default(T);
        }

        /// <summary>
        /// Returns the value from the specified expression if source object is of the specified type.
        /// </summary>
        /// <typeparam name="TIn">The type of the object.</typeparam>
        /// <param name="source">The source object.</param>
        /// <param name="getter">The expression to get the value from the object.</param>
        /// <returns>
        /// The value from the specified expression if the source object is of the
        /// specified type or <c>null</c> otherwise.
        /// </returns>
        public static object If<TIn>(this object source, Func<TIn, object> getter)
        {
            return source is TIn ? getter((TIn)source) : null;
        }
    }
}

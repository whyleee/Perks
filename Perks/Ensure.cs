using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perks
{
    /// <summary>
    /// Contains assertions for method arguments.
    /// </summary>
    public class Ensure
    {
        /// <summary>
        /// Ensures method argument is not null.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="name">Argument name.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if method argument is null.</exception>
        public static void ArgumentNotNull(object argument, string name)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Ensures string method argument is not null or empty.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="name">Argument name.</param>
        /// <exception cref="System.ArgumentException">Thrown if string argument empty.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown if string argument is null.</exception>
        public static void ArgumentNotNullOrEmpty(string argument, string name)
        {
            Ensure.ArgumentNotNull(argument, name);

            if (argument.Length == 0)
            {
                throw new ArgumentException("Can't be empty", name);
            }
        }

        /// <summary>
        /// Ensures method argument is correct using specified assertion.
        /// </summary>
        /// <param name="assert">Assertion lambda.</param>
        /// <param name="name">Argument name.</param>
        /// <param name="error">The error in case if assertion fail.</param>
        /// <exception cref="System.ArgumentException">Thrown if assertion is failed for provided argument.</exception>
        public static void Argument(Func<bool> assert, string name, string error)
        {
            if (!assert())
            {
                throw new ArgumentException(error, name);
            }
        }
    }
}

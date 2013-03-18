using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Perks
{
    /// <summary>
    /// Contains extension methods for easier use of .NET reflection.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the field value from the object by name.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="source">The source object.</param>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the object field.</returns>
        /// <exception cref="System.ArgumentException">Thrown if field is not found by name.</exception>
        /// <remarks>
        /// This method will search any field matching the name, static or instance, public or private,
        /// declared in the object type or it's ancestors.
        /// </remarks>
        public static T Field<T>(this object source, string name)
        {
            FieldInfo field = null;
            var type = source.GetType();

            while (field == null && type != null)
            {
                field = type.GetField(name,
                    BindingFlags.Instance |
                    BindingFlags.Static |
                    BindingFlags.Public |
                    BindingFlags.NonPublic);

                type = type.BaseType;
            }

            if (field == null)
            {
                throw new ArgumentException(string.Format("Type '{0}' doesn't have '{1}' field", source.GetType(), name), "name");
            }

            var value = field.GetValue(source);

            if (value != null && !(value is T))
            {
                throw new InvalidCastException(string.Format(
                    "Field value of type '{0}' can't be casted to requested '{1}' type",
                    value.GetType().FullName, typeof(T).FullName));
            }

            return (T) value;
        }
    }
}

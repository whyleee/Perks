using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Perks
{
    /// <summary>
    /// Extension methods for <see cref="Type"/> objects.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether a type is assignable from the specified base type.
        /// </summary>
        /// <typeparam name="T">The base type to check.</typeparam>
        /// <param name="type">A type.</param>
        /// <returns><c>true</c> if type is assignable from the specified base type; otherwise, <c>false</c>.</returns>
        public static bool Is<T>(this Type type)
        {
            return type.Is(typeof(T));
        }

        /// <summary>
        /// Determines whether a type is assignable from the specified base type.
        /// </summary>
        /// <param name="type">A type.</param>
        /// <param name="baseType">The base type to check.</param>
        /// <returns><c>true</c> if type is assignable from the specified base type; otherwise, <c>false</c>.</returns>
        public static bool Is(this Type type, Type baseType)
        {
            if (baseType.IsGenericTypeDefinition)
            {
                if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseType))
                {
                    return true;
                }

                if (type.IsGenericType && type.GetGenericTypeDefinition() == baseType)
                {
                    return true;
                }

                return type.BaseType != null && Is(type.BaseType, baseType);
            }

            return baseType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Determines whether a type is not assignable from the specified base type.
        /// </summary>
        /// <typeparam name="T">The base type to check.</typeparam>
        /// <param name="type">A type.</param>
        /// <returns><c>true</c> if type is not assignable from the specified base type; otherwise, <c>false</c>.</returns>
        public static bool IsNot<T>(this Type type)
        {
            return !type.Is<T>();
        }

        /// <summary>
        /// Determines whether a type is not assignable from the specified base type.
        /// </summary>
        /// <param name="type">A type.</param>
        /// <param name="baseType">The base type to check.</param>
        /// <returns><c>true</c> if type is not assignable from the specified base type; otherwise, <c>false</c>.</returns>
        public static bool IsNot(this Type type, Type baseType)
        {
            return !type.Is(baseType);
        }

        /// <summary>
        /// Determines whether a type is simple (primitive, predefined or date/time).
        /// </summary>
        /// <param name="type">A type.</param>
        /// <returns><c>true</c> if the type is simple; otherwise, <c>false</c>.</returns>
        public static bool IsSimpleType(this Type type)
        {
            return type.IsPrimitive ||
                type == typeof(string) ||
                type == typeof(DateTime) ||
                type == typeof(decimal) ||
                type == typeof(Guid) ||
                type == typeof(DateTimeOffset) ||
                type == typeof(TimeSpan);
        }

        /// <summary>
        /// Determines whether a type is a value type and allows nulls.
        /// </summary>
        /// <param name="type">A type.</param>
        /// <returns><c>true</c> if the type is value type which allows nulls; otherwise, <c>false</c>.</returns>
        public static bool IsNullableValueType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Gets the type of collection items.
        /// </summary>
        /// <param name="type">The type of collection.</param>
        /// <returns>The type of collection items or <c>null</c> for not enumerable types.</returns>
        public static Type GetCollectionItemType(this Type type)
        {
            if (type.IsNot<IEnumerable>())
            {
                return null;
            }

            return type.GetElementType() ?? type.GetGenericArguments().FirstOrDefault();
        }

        public static bool Has<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.Get<TAttribute>() != null;
        }

        public static bool Has<TAttribute>(this MemberInfo member) where TAttribute : Attribute
        {
            return member.Get<TAttribute>() != null;
        }

        public static bool Has<TAttribute>(this MemberInfo member, Func<TAttribute, bool> with) where TAttribute : Attribute
        {
            var attr = member.Get<TAttribute>();

            return attr != null && with(attr);
        }

        public static TAttribute Get<TAttribute>(this Type type) where TAttribute : Attribute
        {
#if NET40
            return type.CustomAttributeExtensions_GetCustomAttribute<TAttribute>();
#else
            return type.GetCustomAttribute<TAttribute>();
#endif
        }

        public static TAttribute Get<TAttribute>(this MemberInfo member) where TAttribute : Attribute
        {
#if NET40
            return member.CustomAttributeExtensions_GetCustomAttribute<TAttribute>();
#else
            return member.GetCustomAttribute<TAttribute>();
#endif
        }

#if NET40
        private static T CustomAttributeExtensions_GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
        {
            return (T)((object)element.CustomAttributeExtensions_GetCustomAttribute(typeof(T)));
        }

        public static Attribute CustomAttributeExtensions_GetCustomAttribute(this MemberInfo element, Type attributeType)
        {
            return Attribute.GetCustomAttribute(element, attributeType);
        }
#endif
    }
}

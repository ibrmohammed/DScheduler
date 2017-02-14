using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DScheduler.Framework
{
    public static class TypeExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="firstItem"></param>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Enlist<T>(this T firstItem, params T[] list)
        {
            var result = new List<T> { firstItem };
            result.AddRange(list);
            return result;
        }

        /// <summary>
        /// Checks whether a class implements another type or interface.
        /// </summary>
        /// <param name="type">The type to be checked</param>
        /// <typeparam name="T">The interface</typeparam>
        /// <returns>true if the type implements the interface</returns>
        public static bool Implements<T>(this Type type)
        {
            return typeof(T).IsAssignableFrom(type);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum Parse<TEnum>(this string value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value.Trim());
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum Parse<TEnum>(this int value)
        {
            dynamic result = value;
            return (TEnum)result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T SetFlag<T>(this Enum value, T flags)
        {
            if (!value.GetType().IsEquivalentTo(typeof(T)))
            {
                throw new ArgumentException("Enum value and flags types don't match.");
            }

            // yes this is ugly, but unfortunately we need to use an intermediate boxing cast
            return (T)Enum.ToObject(typeof(T), Convert.ToUInt64(value) | Convert.ToUInt64(flags));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="flags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T SetFlag<T>(this T value, T flags)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type not supported!");

            // yes this is ugly, but unfortunately we need to use an intermediate boxing cast
            return (T)Enum.ToObject(typeof(T), Convert.ToUInt64(value) | Convert.ToUInt64(flags));
        }
    }
}

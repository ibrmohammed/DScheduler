using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///Adds one or more items to a collection
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        public static void Add<T>(this ICollection<T> collection, params T[] items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
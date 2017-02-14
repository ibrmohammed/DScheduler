using System.Linq;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool InList<T>(this T item, params T[] items)
        {
            return items.ToList().Contains(item);
        }
    }
}
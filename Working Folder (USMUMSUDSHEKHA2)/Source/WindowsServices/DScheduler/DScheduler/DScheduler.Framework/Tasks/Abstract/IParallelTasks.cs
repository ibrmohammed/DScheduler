using System.Collections.Concurrent;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParallel<T>
    {
        /// <summary>
        ///
        /// </summary>
        ConcurrentBag<T> Items
        {
            get;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        void Add(T[] items);
    }
}
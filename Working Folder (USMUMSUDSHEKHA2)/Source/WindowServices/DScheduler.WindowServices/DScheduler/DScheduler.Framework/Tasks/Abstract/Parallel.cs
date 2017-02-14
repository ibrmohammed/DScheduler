using System.Collections.Concurrent;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Parallel<T> : IParallel<T>
    {
        private readonly ConcurrentBag<T> _items;

        /// <summary>
        ///
        /// </summary>
        public Parallel()
        {
            _items = new ConcurrentBag<T>();
        }

        /// <summary>
        ///
        /// </summary>
        public ConcurrentBag<T> Items
        {
            get
            {
                return _items;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            _items.Add(item);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        public void Add(params T[] items)
        {
            foreach (var item in items)
            {
                _items.Add(item);
            }
        }
    }
}
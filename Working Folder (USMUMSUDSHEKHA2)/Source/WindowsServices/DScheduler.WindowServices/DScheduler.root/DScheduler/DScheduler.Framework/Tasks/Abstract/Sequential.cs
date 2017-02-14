using System.Collections.Generic;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Sequential<T> : ISequential<T>
    {
        private readonly Queue<T> _items;

        /// <summary>
        ///
        /// </summary>
        public Sequential()
        {
            _items = new Queue<T>();
        }

        /// <summary>
        ///
        /// </summary>
        public Queue<T> Items
        {
            get
            {
                return _items;
            }
        }
    }
}
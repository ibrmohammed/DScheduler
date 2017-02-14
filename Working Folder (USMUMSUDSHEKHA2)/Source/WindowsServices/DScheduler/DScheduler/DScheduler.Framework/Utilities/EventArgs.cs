using System;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventArgs<T> : EventArgs
    {
        private readonly T _item;

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        public EventArgs(T item)
        {
            _item = item;
        }

        /// <summary>
        ///
        /// </summary>
        public T Item
        {
            get { return _item; }
        }
    }
}
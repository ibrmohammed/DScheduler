using System;
using System.Threading;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class RandomNumberTask : TaskOut<int>
    {
        private readonly int _from;
        private readonly int _to;

        private Random _random;

        /// <summary>
        ///
        /// </summary>
        public RandomNumberTask()
            : this(1, 100)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public RandomNumberTask(int from, int to)
        {
            _from = @from;
            _to = to;
        }

        /// <summary>
        ///Provides a method for any clean up activity needed for the task
        /// </summary>
        protected override void CleanUp()
        {
            Console.WriteLine("Cleaning {0} up Random Number Task...",
                              Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override int Function()
        {
            var result = _random.Next(_from, _to);
            Console.WriteLine("Random Number Generated: {0}", result);
            return result;
        }

        /// <summary>
        ///Provides a method for any initialization needed for the task
        /// </summary>
        protected override void Initialize()
        {
            Console.WriteLine("Initializing Random Number Task...");

            _random = RandomProvider.GetThreadRandom();
        }
    }
}
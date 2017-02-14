using System;
using System.Threading;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public static class RandomProvider
    {
        private static readonly ThreadLocal<Random> RandomWrapper = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

        private static int _seed = Environment.TickCount;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static Random GetThreadRandom()
        {
            return RandomWrapper.Value;
        }
    }
}
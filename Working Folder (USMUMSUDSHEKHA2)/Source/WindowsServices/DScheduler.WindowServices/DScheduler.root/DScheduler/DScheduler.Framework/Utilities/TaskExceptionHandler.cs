using System;
using System.Linq;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    public static class TaskExceptionHandler
    {
        /// <summary>
        ///
        /// </summary>
        /// <exception>
        ///     <cref>MyCustomException</cref>
        /// </exception>
        public static void TryRun(this ITask batchTask, params ExceptionHandler[] handlers)
        {
            var task = Task.Factory.StartNew(batchTask.Run);

            try
            {
                task.Wait();
            }
            catch (AggregateException ae)
            {
                // Assume we know what's going on with this particular exception.
                // Rethrow anything else. AggregateException.Handle provides
                // another way to express this. See later example.
                foreach (var e in ae.InnerExceptions)
                {
                    var handler = handlers.FirstOrDefault(h => e.GetType().IsAssignableFrom(h.ExceptionType));

                    if (handler != null)
                    {
                        handler.Action(e);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}

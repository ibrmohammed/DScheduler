using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EnumerateTask<T> : TaskIn<IEnumerable<T>>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        protected override void Execute(IEnumerable<T> argument)
        {
            Parallel.ForEach(argument, ProcessItem);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        protected abstract void ProcessItem(T item);
    }

    ///   <summary>
    ///
    ///   </summary>
    /// <typeparam name="TArgument"></typeparam>
    ///  <typeparam name="TResult"></typeparam>
    public abstract class EnumerateTaskOut<TArgument, TResult> : TaskInOut<IEnumerable<TArgument>, IEnumerable<TResult>>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="arguments"></param>
        protected override IEnumerable<TResult> Execute(IEnumerable<TArgument> arguments)
        {
            return arguments.Select(ProcessItem);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        protected abstract TResult ProcessItem(TArgument item);
    }
}
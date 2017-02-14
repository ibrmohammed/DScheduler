
namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class TaskWithResult<T> : TaskDecoration
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        public TaskWithResult(ITask task)
            : base(task)
        {
        }

        /// <summary>
        ///
        /// </summary>
        public T Result { get; protected set; }
    }
}
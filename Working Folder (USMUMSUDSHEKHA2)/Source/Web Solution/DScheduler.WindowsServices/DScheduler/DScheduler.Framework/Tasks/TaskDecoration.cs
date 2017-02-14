
namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class TaskDecoration : ITask
    {
        protected readonly ITask Task;

        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        protected TaskDecoration(ITask task)
        {
            Task = task;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        public virtual void Run()
        {
            Task.Run();
        }
    }
}
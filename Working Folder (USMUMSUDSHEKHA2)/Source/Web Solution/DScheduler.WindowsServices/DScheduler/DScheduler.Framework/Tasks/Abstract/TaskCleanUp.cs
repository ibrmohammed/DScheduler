namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class TaskCleanUp : TaskDecorator<ITask>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        protected TaskCleanUp(ITask task)
            : base(task)
        {
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        public override void Run()
        {
            Task.Run();
            CleanUp();
        }

        /// <summary>
        ///
        /// </summary>
        protected abstract void CleanUp();
    }
}
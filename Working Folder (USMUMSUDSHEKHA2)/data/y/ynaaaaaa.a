namespace DScheduler.Framework
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public abstract class TaskDecorator<TTask> : ITask where TTask : ITask
    {
        /// <summary>
        ///
        /// </summary>
        protected readonly TTask Task;

        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        protected TaskDecorator(TTask task)
        {
            Task = task;
        }

        #region ITaskError Members

        /// <summary>
        /// Occurs when [task error].
        /// </summary>
        public virtual event EventHandler TaskException;

        #endregion

        /// <summary>
        /// Executes the task
        /// </summary>
        public virtual void Run()
        {
            Task.Run();
        }

        /// <summary>
        /// Called when [task error].
        /// </summary>
        protected virtual void OnTaskException()
        {
            EventHandler handler = this.TaskException;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
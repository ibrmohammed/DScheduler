namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class Initializer<TTask> : TaskDecorator<TTask> where TTask : ITask
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        protected Initializer(TTask task)
            : base(task)
        {
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        public override void Run()
        {
            Initialize(Task);
            base.Run();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        protected abstract void Initialize(TTask task);
    }
}
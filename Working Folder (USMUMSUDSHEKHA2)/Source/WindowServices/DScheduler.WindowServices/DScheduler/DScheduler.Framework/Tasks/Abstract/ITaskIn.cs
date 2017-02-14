namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    public interface ITaskIn<TArgument> : ITask
    {
        /// <summary>
        /// The argument to be passed to the task
        /// </summary>
        TArgument Argument
        {
            get;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        void Run(TArgument argument);
    }
}
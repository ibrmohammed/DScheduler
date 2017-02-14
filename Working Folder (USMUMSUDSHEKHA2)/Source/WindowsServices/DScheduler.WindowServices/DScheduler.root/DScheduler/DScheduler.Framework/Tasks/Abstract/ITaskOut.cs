namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface ITaskOut<TResult> : ITask
    {
        /// <summary>
        ///The result for this task
        /// </summary>
        TResult Result
        {
            get;
        }

        /// <param name="nextTask"></param>
        TTask Chain<TTask>(TTask nextTask) where TTask : ITaskIn<TResult>;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        new TResult Run();
    }
}
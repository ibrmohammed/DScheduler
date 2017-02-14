namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TArgument"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface ITaskInOut<TArgument, TResult> : ITaskIn<TArgument>, ITaskOut<TResult>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        new TResult Run(TArgument argument);
    }
}
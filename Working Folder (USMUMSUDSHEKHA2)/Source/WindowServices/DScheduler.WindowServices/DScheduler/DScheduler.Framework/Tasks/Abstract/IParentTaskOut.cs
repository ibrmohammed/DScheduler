namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IParentTaskOut<T, TResult> : IParentTask<T>, ITaskOut<TResult>
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IParentTaskOut<T1, T2, TResult> : IParentTask<T1, T2>, ITaskOut<TResult>
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IParentTaskOut<T1, T2, T3, TResult> : IParentTask<T1, T2, T3>, ITaskOut<TResult>
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IParentTaskOut<T1, T2, T3, T4, TResult> : IParentTask<T1, T2, T3, T4>, ITaskOut<TResult>
    {
    }
}
namespace DScheduler.Framework
{
    /// <summary>
    ///An interface that specifies what type of Child task a parent task can have
    /// </summary>
    /// <typeparam name="T">The child task result type</typeparam>
    public interface IParentTask<T> : ITask
    {
        /// <summary>
        ///
        /// </summary>
        ITaskOut<T> Child
        {
            get;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public interface IParentTask<T1, T2> : ITask
    {
        /// <summary>
        ///
        /// </summary>
        ITaskOut<T1> Child1
        {
            get;
        }

        /// <summary>
        ///
        /// </summary>
        ITaskOut<T2> Child2
        {
            get;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public interface IParentTask<T1, T2, T3> : IParentTask<T1, T2>
    {
        /// <summary>
        ///
        /// </summary>
        ITaskOut<T3> Child3
        {
            get;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    public interface IParentTask<T1, T2, T3, T4> : IParentTask<T1, T2, T3>
    {
        /// <summary>
        ///
        /// </summary>
        ITaskOut<T4> Child4
        {
            get;
        }
    }
}
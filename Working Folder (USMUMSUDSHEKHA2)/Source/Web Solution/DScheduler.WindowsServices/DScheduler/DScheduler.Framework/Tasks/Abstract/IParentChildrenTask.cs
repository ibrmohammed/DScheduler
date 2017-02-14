namespace DScheduler.Framework
{
    /// <summary>
    ///An interface that specifies what type of Children tasks a parent task can have
    /// </summary>
    /// <typeparam name="T">The children task type</typeparam>
    public interface IParentChildrenTask<T> : ITask
    {
        /// <summary>
        ///
        /// </summary>
        ITaskOut<T>[] Children
        {
            get;
        }
    }
}
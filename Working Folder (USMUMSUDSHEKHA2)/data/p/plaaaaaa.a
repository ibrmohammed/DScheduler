namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TChildTask"></typeparam>
    public abstract class EnumerateWithChildTask<TEntity, TChildTask> : EnumerateTask<TEntity> where TChildTask : ITaskIn<TEntity>, new()
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        protected override void ProcessItem(TEntity item)
        {
            new TChildTask().Run(item);
        }
    }
}
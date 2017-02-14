namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class ResourceManager : IResourceManager
    {
        /// <summary>
        ///
        /// </summary>
        public abstract void Commit();

        /// <summary>
        ///
        /// </summary>
        public virtual void InDoubt()
        {
            //
        }

        /// <summary>
        ///
        /// </summary>
        public abstract Vote Prepare();

        /// <summary>
        ///
        /// </summary>
        public abstract void Rollback();
    }
}
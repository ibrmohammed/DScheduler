namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public interface IResourceManager
    {
        /// <summary>
        ///
        /// </summary>
        void Commit();

        /// <summary>
        ///
        /// </summary>
        void InDoubt();

        /// <summary>
        ///
        /// </summary>
        Vote Prepare();

        /// <summary>
        ///
        /// </summary>
        void Rollback();
    }
}
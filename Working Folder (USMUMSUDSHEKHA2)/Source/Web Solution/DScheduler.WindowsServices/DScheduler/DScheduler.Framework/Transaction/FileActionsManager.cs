using System;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class FileActionsManager : ResourceManager
    {
        private readonly Action<FileActionsManager> _action;

        ///  <summary>
        ///
        ///  </summary>
        /// <param name="action"></param>
        public FileActionsManager(Action<FileActionsManager> action)
        {
            _action = action;
        }

        /// <summary>
        ///
        /// </summary>
        public override void Commit()
        {
        }

        /// <summary>
        ///
        /// </summary>
        public void Copy()
        {
        }

        /// <summary>
        ///
        /// </summary>
        public void Delete()
        {
        }

        /// <summary>
        ///
        /// </summary>
        public void Move()
        {
        }

        /// <summary>
        ///
        /// </summary>
        public override Vote Prepare()
        {
            Vote result;

            try
            {
                _action(this);
                result = Vote.Commit;
            }
            catch (Exception)
            {
                result = Vote.Rollback;
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        public override void Rollback()
        {
        }
    }
}
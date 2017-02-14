using System;
using System.IO;


namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class FileCopyTask : ParentTask<string, string, bool>
    {
        /// <summary>
        ///
        /// </summary>
        public FileCopyTask(string fileKey, string destinationKey, bool overwrite = true)
            : this(new ReadConfigurationKey(fileKey), new ReadConfigurationKey(destinationKey), new InputTask<bool>(overwrite))
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="getFileName"></param>
        /// <param name="getDestination"></param>
        /// <param name="getOverwrite"></param>
        protected FileCopyTask(ITaskOut<string> getFileName, ITaskOut<string> getDestination, ITaskOut<bool> getOverwrite)
            : base(getFileName, getDestination, getOverwrite)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override Action<string, string, bool> Action()
        {
            return File.Copy;
        }
    }
}
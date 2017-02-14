
namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class CopyFile : WriteFile
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="fromFile"></param>
        /// <param name="toFile"></param>
        public CopyFile(string fromFile, string toFile)
            : this(new InputTask<string>(fromFile), new ReadFile(toFile))
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileNameTask"></param>
        /// <param name="contentTask"></param>
        public CopyFile(ITaskOut<string> fileNameTask, ITaskOut<byte[]> contentTask)
            : base(fileNameTask, contentTask)
        {
        }
    }
}
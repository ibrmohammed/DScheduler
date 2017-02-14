using System;
using System.IO;


namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class WriteFile : ParentTask<string, byte[]>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="fileNameTask"></param>
        /// <param name="contentTask"></param>
        public WriteFile(ITaskOut<string> fileNameTask, ITaskOut<byte[]> contentTask)
            : base(fileNameTask, contentTask)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override Action<string, byte[]> Action()
        {
            return (f, c) =>
                 {
                     Console.WriteLine("Writing file {0}", f);
                     File.WriteAllBytes(f, c);
                 };
        }

        /// <summary>
        ///Provides a method for any clean up activity needed for the task
        /// </summary>
        protected override void CleanUp()
        {
            Console.WriteLine("Finished writing file...");
        }

        /// <summary>
        ///Provides a method for any initialization needed for the task
        /// </summary>
        protected override void Initialize()
        {
            Console.WriteLine("Ready to write file...");
        }
    }
}
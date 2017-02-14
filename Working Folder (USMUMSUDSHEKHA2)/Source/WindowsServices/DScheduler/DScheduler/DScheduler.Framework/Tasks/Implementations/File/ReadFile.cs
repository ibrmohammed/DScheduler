using System;
using System.IO;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class ReadFile : TaskInOut<string, byte[]>
    {
        private readonly string _fileName;

        /// <summary>
        ///
        /// </summary>
        /// <param name="fileName"></param>
        public ReadFile(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// The argument to be passed to the task
        /// </summary>
        public override string Argument
        {
            get
            {
                return _fileName;
            }
        }

        /// <summary>
        ///Provides a method for any clean up activity needed for the task
        /// </summary>
        protected override void CleanUp()
        {
            Console.WriteLine("Finished reading file {0}...", _fileName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        protected override byte[] Execute(string argument)
        {
            Console.WriteLine("Reading file {0}...", _fileName);
            return File.ReadAllBytes(_fileName);
        }

        /// <summary>
        ///Provides a method for any initialization needed for the task
        /// </summary>
        protected override void Initialize()
        {
            Console.WriteLine("Ready to read file {0}...", _fileName);
        }
    }
}
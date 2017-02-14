using System;
using System.IO;


namespace DScheduler.Framework
{
    /// <summary>
    ///Helps copy files from one location to another
    /// </summary>
    public class FilesCopyTask : BatchTask
    {
        private readonly string[] _destinations;
        private readonly string[] _fileNames;
        private readonly bool _overwrite;

        ///  <summary>
        ///Copies a set of files from target to a destination specified for each file
        ///  </summary>
        ///  <param name="fileNames">A array of file names</param>
        ///  <param name="destinations"></param>
        /// <param name="overwrite"></param>
        public FilesCopyTask(string[] fileNames, string[] destinations, bool overwrite = true)
        {
            if (fileNames.Length != destinations.Length)
                throw new ApplicationException("Destination count must match Source count");

            _fileNames = fileNames;
            _destinations = destinations;
            _overwrite = overwrite;
        }

        /// <summary>
        ///Copies a set of files to a destination folder
        /// </summary>
        /// <param name="fileNames">An array of files</param>
        /// <param name="destination">The name of the target folder</param>
        /// <param name="overwrite"></param>
        public FilesCopyTask(string[] fileNames, string destination, bool overwrite = true)
        {
            _fileNames = fileNames;

            _destinations = new string[fileNames.Length];

            for (var i = 0; i < _fileNames.Length; i++)
            {
                var fileName = Path.GetFileName(_fileNames[i]);
                if (fileName != null)
                    _destinations[i] = Path.Combine(destination, fileName);
                else
                    throw new ApplicationException("File name cannot be NULL.");
            }

            _overwrite = overwrite;
        }

        /// <summary>
        ///The core of the task
        /// </summary>
        protected override void Execute()
        {
            for (var i = 0; i < _fileNames.Length; i++)
            {
                var fileName = _fileNames[i];
                var destination = _destinations[i];
                File.Copy(fileName, destination, _overwrite);
            }
        }
    }
}
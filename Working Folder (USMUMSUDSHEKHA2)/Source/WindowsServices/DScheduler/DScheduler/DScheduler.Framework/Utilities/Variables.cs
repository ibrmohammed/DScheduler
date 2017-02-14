using System;

namespace DScheduler.Framework
{
    /// <summary>
    /// Variables
    /// </summary>
    public class Variables
    {
        /// <summary>
        /// Gets the badlib.
        /// </summary>
        /// <value>The badlib.</value>
        public static string BadLibrary
        {
            get { return Get("badlib"); }
        }

        /// <summary>
        /// Gets the batchlib.
        /// </summary>
        /// <value>The batchlib.</value>
        public static string BatchLibrary
        {
            get { return Get("batchlib"); }
        }

        /// <summary>
        /// Gets the batchpath.
        /// </summary>
        /// <value>The batchpath.</value>
        public static string BatchPath
        {
            get { return Get("batchpath"); }
        }

        /// <summary>
        /// Gets the binlib.
        /// </summary>
        /// <value>The binlib.</value>
        public static string BinLibrary
        {
            get { return Get("binlib"); }
        }

        /// <summary>
        /// Gets the configlib.
        /// </summary>
        /// <value>The configlib.</value>
        public static string ConfigLibrary
        {
            get { return Get("configlib"); }
        }

        /// <summary>
        /// Gets the consolelog.
        /// </summary>
        /// <value>The consolelog.</value>
        public static string ConsoleLog
        {
            get { return Get("console.log"); }
        }

        /// <summary>
        /// Gets the ctllib.
        /// </summary>
        /// <value>The ctllib.</value>
        public static string Ctllibrary
        {
            get { return Get("ctllib"); }
        }

        /// <summary>
        /// Gets the dataarchive.
        /// </summary>
        /// <value>The dataarchive.</value>
        public static string DataArchive
        {
            get { return Get("dataarchive"); }
        }

        /// <summary>
        /// Gets the datadrive.
        /// </summary>
        /// <value>The datadrive.</value>
        public static string DataDrive
        {
            get { return Get("datadrive"); }
        }

        /// <summary>
        /// Gets the datalib.
        /// </summary>
        /// <value>The datalib.</value>
        public static string DataLibrary
        {
            get { return Get("datalib"); }
        }

        /// <summary>
        /// Gets the discard.
        /// </summary>
        /// <value>The discard.</value>
        public static string Discard
        {
            get { return Get("discard"); }
        }

        /// <summary>
        /// Gets the joblib.
        /// </summary>
        /// <value>The joblib.</value>
        public static string JobLibrary
        {
            get { return Get("joblib"); }
        }

        /// <summary>
        /// Gets the jobname.
        /// </summary>
        /// <value>The jobname.</value>
        public static string JobName
        {
            get { return Get("jobname"); }
        }

        /// <summary>
        /// Gets the logarchive.
        /// </summary>
        /// <value>The logarchive.</value>
        public static string LogArchive
        {
            get { return Get("logarchive"); }
        }

        /// <summary>
        /// Gets the loglib.
        /// </summary>
        /// <value>The loglib.</value>
        public static string LogLibrary
        {
            get { return Get("loglib"); }
        }

        /// <summary>
        /// Gets the sqllib.
        /// </summary>
        /// <value>The sqllib.</value>
        public static string SqlLibrary
        {
            get { return Get("sqllib"); }
        }

        /// <summary>
        /// Gets the steplib.
        /// </summary>
        /// <value>The steplib.</value>
        public static string StepLibrary
        {
            get { return Get("steplib"); }
        }

        /// <summary>
        /// Gets the templib.
        /// </summary>
        /// <value>The templib.</value>
        public static string TempLibrary
        {
            get { return Get("templib"); }
        }

        /// <summary>
        /// Gets the vsnet.
        /// </summary>
        /// <value>The vsnet.</value>
        public static string VsNet
        {
            get { return Get("vs.net"); }
        }

        private static string Get(string variableName)
        {
            try
            {
                return Environment.GetEnvironmentVariable(variableName);
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}
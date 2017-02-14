using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    /// <summary>
    /// Contains parameters necessary to run the Job.
    /// </summary>
    public static class JobParameters
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [restart previous single instance run].
        /// </summary>
        /// <value>
        /// <c>true</c> if [restart previous single instance run]; otherwise, <c>false</c>.
        /// </value>
        public static bool ContinuePreviousInstanceRun { get; set; }

        #endregion

        #region Internal Properties

        /// <summary>
        /// The default chunk size for a chunked job.
        /// </summary>
        internal static int DefaultChunkSize { get; set; }

        #endregion
    }
}

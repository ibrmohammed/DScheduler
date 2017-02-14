using System.Collections.Generic;

namespace DScheduler.Framework
{
    /// <summary>
    /// Class to log batch information.
    /// </summary>
    public class BatchError
    {
        /// <summary>
        /// Gets or sets the identifier information.
        /// </summary>
        /// <value>
        /// The identifier information.
        /// </value>
        public KeyValuePair<string, string> IdentifierInformation { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string ErrorType { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string ErrorSummary { get; set; }

        /// <summary>
        /// Gets or sets the detail.
        /// </summary>
        /// <value>
        /// The detail.
        /// </value>
        public string ErrorDetail { get; set; }

        /// <summary>
        /// Gets or sets the job chunk identifier.
        /// </summary>
        /// <value>
        /// The job chunk identifier.
        /// </value>
        public int JobChunkId { get; set; }
    }
}
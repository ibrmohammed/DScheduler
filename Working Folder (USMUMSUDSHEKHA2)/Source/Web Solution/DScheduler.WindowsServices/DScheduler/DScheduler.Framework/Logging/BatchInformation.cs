using System.Collections.Generic;

namespace DScheduler.Framework
{
    /// <summary>
    /// Class to log information for a batch.
    /// </summary>
    public class BatchInformation
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
        public string InformationType { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string InformationSummary { get; set; }

        /// <summary>
        /// Gets or sets the detail.
        /// </summary>
        /// <value>
        /// The detail.
        /// </value>
        public string InformationDetail { get; set; }

        /// <summary>
        /// Gets or sets the job chunk identifier.
        /// </summary>
        /// <value>
        /// The job chunk identifier.
        /// </value>
        public int JobChunkId { get; set; }
    }
}
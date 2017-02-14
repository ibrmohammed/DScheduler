using System;
using System.Collections.Generic;

namespace DScheduler.Framework
{
    /// <summary>
    /// Class to log batch exceptions.
    /// </summary>
    public class BatchException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchException"/> class.
        /// </summary>
        public BatchException()
        {
            IdentifierInformation = new KeyValuePair<string, string>();
        }

        /// <summary>
        /// Gets or sets the exceptions.
        /// </summary>
        /// <value>
        /// The exceptions.
        /// </value>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the identifier information.
        /// </summary>
        /// <value>
        /// The identifier information.
        /// </value>
        public KeyValuePair<string, string> IdentifierInformation { get; set; }

        /// <summary>
        /// Gets or sets the job chunk identifier.
        /// </summary>
        /// <value>
        /// The job chunk identifier.
        /// </value>
        public int JobChunkId { get; set; }
    }
}
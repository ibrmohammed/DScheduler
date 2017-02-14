namespace DScheduler.Framework
{
    /// <summary>
    /// Holds information about a single chunk.
    /// </summary>
    public class ChunkInformation
    {
        /// <summary>
        /// Gets or sets the job segment identifier.
        /// </summary>
        /// <value>
        /// The job segment identifier.
        /// </value>
        public int JobChunkId { get; internal set; }

        /// <summary>
        /// Gets or sets the start position.
        /// </summary>
        /// <value>
        /// The start position.
        /// </value>
        public long ChunkStartPosition { get; set; }

        /// <summary>
        /// Gets or sets the end position.
        /// </summary>
        /// <value>
        /// The end position.
        /// </value>
        public long ChunkEndPosition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is processed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is processed; otherwise, <c>false</c>.
        /// </value>
        public string ProcessingStatus { get; internal set; }

        /// <summary>
        /// Gets or sets the chunk statistics.
        /// </summary>
        /// <value>
        /// The chunk statistics.
        /// </value>
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// Gets or sets the name of the source.
        /// </summary>
        /// <value>
        /// The name of the source.
        /// </value>
        public string SourceName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to [run single instanced].
        /// In case this value is set to true for one of the values, the whole batch will only run on the first instance.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [run single instanced]; otherwise, <c>false</c>.
        /// </value>
        public bool IsSingleInstanced { get; set; }
    }
}
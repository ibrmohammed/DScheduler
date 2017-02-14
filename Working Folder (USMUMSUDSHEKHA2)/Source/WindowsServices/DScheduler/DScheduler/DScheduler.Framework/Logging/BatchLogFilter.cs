using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace DScheduler.Framework
{
    /// <summary>
    /// 
    /// </summary>
    internal class BatchLogFilter
    {
        /// <summary>
        /// Filters the specified log.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        internal bool Filter(TraceEventType eventType)
        {
            var filter = ConfigurationManager.AppSettings[BatchConstants.KeyBatchLogFilter] ?? string.Empty;
            bool result;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var severity = (TraceEventType)Enum.Parse(typeof(TraceEventType), filter);
                result = Convert.ToInt32(severity) >= Convert.ToInt32(eventType);
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}

using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;

namespace DScheduler.Framework
{
    /// <summary>
    /// ConsoleLogger
    /// </summary>
    public class ConsoleLogger
    {
        /// <summary>
        /// Writes the log entry.
        /// </summary>
        /// <param name="returnMessage">The return message.</param>
        public static void WriteLogEntry(string returnMessage)
        {
            var entry = new LogEntry();
            entry.Categories.Add(Variables.JobName ?? "Test" + ".Console");
            entry.TimeStamp = DateTime.Now;
            entry.Message = returnMessage;
            entry.ProcessName = Variables.JobName ?? "Test";
            Logger.Write(entry);
        }
    }
}
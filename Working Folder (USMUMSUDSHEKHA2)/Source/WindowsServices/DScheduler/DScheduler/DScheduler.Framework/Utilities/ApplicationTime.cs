using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    public static class ApplicationTime
    {
        private static TimeSpan timeDifference = TimeSpan.Zero;

        /// <summary>
        /// Sets the time based on the timespan.
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        /// <param name="time">
        /// It is of type DateTime which is used to calculate the time difference.
        /// </param>
        public static void SetTime(DateTime time)
        {
            SetTime(time - DateTime.Now);
        }

        /// <summary>
        /// Sets the time based on the timespan.
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        /// <param name="time">
        /// It is of type TimeSpan which is used to calculate the time difference.
        /// </param>
        public static void SetTime(TimeSpan time)
        {
            timeDifference = time;
        }

        /// <summary>
        /// Sets the time based on days passed
        /// </summary>
        /// <param name="days"></param>
        public static void SetTime(int days)
        {
            SetTime(new TimeSpan(days, 0, 0, 0));
        }

        /// <summary>
        /// Gets the current time based on the time span.
        /// </summary>
        /// <returns>
        /// DateTime.
        /// </returns>
        public static DateTime GetCurrentTime()
        {
            return DateTime.Now + timeDifference;
        }
    }
}

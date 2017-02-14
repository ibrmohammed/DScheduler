using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class StatusArgs : EventArgs
    {
        private readonly IStatusInfo _status;

        /// <summary>
        ///
        /// </summary>
        /// <param name="status"></param>
        public StatusArgs(IStatusInfo status)
        {
            _status = status;
        }

        /// <summary>
        ///
        /// </summary>
        public IStatusInfo Status
        {
            get
            {
                return _status;
            }
        }
    }
}
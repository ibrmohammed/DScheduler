using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DScheduler.Framework
{
    /// <summary>
    /// Implement this interface to have the ability to send emails on error
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends out an error email based on configuration in a backing store.
        /// </summary>
        void SendErrorEmail();
    }
}

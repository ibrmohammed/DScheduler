
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class InterfaceExceptionArgs : EventArgs
    {
        private readonly IDSchedulerException _khbeInterfaceException;

        /// <summary>
        ///
        /// </summary>
        /// <param name="khbeInterfaceException"></param>
        public InterfaceExceptionArgs(IDSchedulerException khbeInterfaceException)
        {
            _khbeInterfaceException = khbeInterfaceException;
        }

        /// <summary>
        ///
        /// </summary>
        public IDSchedulerException KhbeInterfaceException
        {
            get
            {
                return _khbeInterfaceException;
            }
        }
    }
}
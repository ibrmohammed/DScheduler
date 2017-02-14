using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    public delegate void LogException<T>(T exception) where T : IDSchedulerException;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="statusInfo"></param>
    public delegate void LogStatus<T>(T statusInfo) where T : IStatusInfo;
}
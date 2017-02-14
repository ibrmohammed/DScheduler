using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DScheduler.Common
{
    public class Logger
    {
        #region Logging
        public static void WriteLog(string message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(@"C:\Dev1\MSCoPSchedularService.log", true);
                sw.WriteLine(DateTime.Now.ToString() + " : " + message);
                sw.Flush();
                sw.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void WriteLog(string message, string jobName)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(@"C:\Temp\MSCoPSchedularService_" + jobName + ".log", true);
                sw.WriteLine(DateTime.Now.ToString() + " : " + message);
                sw.Flush();
                sw.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

using System;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DScheduler.BusinessServices;
using System.Configuration;
using DScheduler.Common;

namespace DScheduler.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]             
            //{
            //    new DSchedulerService()             
            //};
            //ServiceBase.Run(ServicesToRun);

            ProcessServiceInformation psi = new ProcessServiceInformation();
            var conn = System.Configuration.ConfigurationManager.ConnectionStrings[Constants.DBConnectionString].ConnectionString;
            string serviceInterval = ConfigurationManager.AppSettings["SchedulerInterval"];
            int numberOfThreads = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfThreads"].ToString());
            int schedulerInterval = Convert.ToInt32(serviceInterval);
            psi.FetchJobRecords(conn, DateTime.Now.TimeOfDay, schedulerInterval, numberOfThreads);
        }

    }
}


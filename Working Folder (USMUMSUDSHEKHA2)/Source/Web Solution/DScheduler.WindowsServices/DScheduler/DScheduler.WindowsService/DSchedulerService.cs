using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DScheduler.BusinessServices;
using DScheduler.Common;
namespace DScheduler.WindowsService
{
    partial class DSchedulerService : ServiceBase
    {
        private System.Timers.Timer timer = null;
        private int schedulerInterval;
        private int numberOfThreads;
       // private System.Diagnostics.EventLog eventLogger;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        public DSchedulerService()
        {
            InitializeComponent();
           // eventLogger = new System.Diagnostics.EventLog();

            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
           // eventLogger.Source = "MySource";
           // eventLogger.Log = "MyNewLog";
            string serviceInterval = ConfigurationManager.AppSettings["SchedulerInterval"];
            schedulerInterval = Convert.ToInt32(serviceInterval);
            numberOfThreads = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfThreads"].ToString());
        }

        protected override void OnStart(string[] args)
        {
           // eventLogger.WriteEntry("In OnStart");

            Logger.WriteLog("Service Execution Started");
            // TODO: Add code here to start your service.
            timer = new System.Timers.Timer();
            this.timer.Interval = schedulerInterval;
            this.timer.Elapsed += timer_Elapsed;
            this.timer.Start();
        }

        protected override void OnStop()
        {
           // eventLogger.WriteEntry("In OnStop");
            Logger.WriteLog("Service Execution Stopped");
            this.timer.Enabled = false;
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        protected override void OnContinue()
        {
           // eventLogger.WriteEntry("In OnContinue.");
        }  


        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Logger.WriteLog("Timer event handler,ProcessServiceInformation called" + DateTime.Now);
            ProcessServiceInformation psi = new ProcessServiceInformation();
            var conn = ConfigurationManager.ConnectionStrings[Constants.DBConnectionString].ConnectionString;
            psi.FetchJobRecords(conn, DateTime.Now.TimeOfDay, schedulerInterval,numberOfThreads);
        }

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public long dwServiceType;
            public ServiceState dwCurrentState;
            public long dwControlsAccepted;
            public long dwWin32ExitCode;
            public long dwServiceSpecificExitCode;
            public long dwCheckPoint;
            public long dwWaitHint;
        };
    }
}

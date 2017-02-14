using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Messaging;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DScheduler.BusinessServices;
using DScheduler.Common;

namespace ExecuteJobRulesUsingWS
{
    public partial class DailyExecutionService : ServiceBase
    {
        private System.Timers.Timer timer = null;
        private int dailySchedulerInterval;
        public int numberOfThreads { get; set; }

        public DailyExecutionService()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            string serviceInterval = ConfigurationManager.AppSettings["SchedulerInterval"];
            dailySchedulerInterval = Convert.ToInt32(serviceInterval) * 60 * 100 * 60; //converting number of hours into seconds
            numberOfThreads = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfThreads"].ToString());
        }

        protected override void OnStart(string[] args)
        {
            // eventLogger.WriteEntry("In OnStart");

            Logger.WriteLog("Service Execution Started");
            // TODO: Add code here to start your service.
            timer = new System.Timers.Timer();
            this.timer.Interval = dailySchedulerInterval;
            this.timer.Elapsed += timer_Elapsed;
            this.timer.Start();
        }

        protected override void OnStop()
        {
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Logger.WriteLog("Timer event handler,ProcessPeriodicServiceInformation called" + DateTime.Now);
            ProcessPeriodicServiceInformation psi = new ProcessPeriodicServiceInformation();
            var conn = System.Configuration.ConfigurationManager.ConnectionStrings[Constants.DBConnectionString].ConnectionString;
            psi.ExecutePeriodicJobs(conn, DateTime.Now, dailySchedulerInterval, numberOfThreads);
        }
    }
}

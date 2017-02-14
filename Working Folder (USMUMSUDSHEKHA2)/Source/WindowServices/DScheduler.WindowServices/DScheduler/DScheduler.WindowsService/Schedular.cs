using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using tmt = System.Timers;
using DScheduler.BusinessServices;
using DScheduler.Common;
using System.Configuration;

namespace DScheduler.WindowsService
{
    
//C:\Windows\Microsoft.NET\Framework\v4.0.30319>installutil /u "C:\Users\prpandey\Documents\Visual Studio 2013\Projects\MSCoPSchedularService\MSCoPSchedularService\bin\Debug\MSCoPSchedularService.exe"
//Microsoft (R) .NET Framework Installation utility Version 4.0.30319.33440
//Copyright (C) Microsoft Corporation.  All rights reserved.

    public partial class Schedular : ServiceBase
    {
        private tmt.Timer timer = null;
        //private bool finishedFlag = false;
        public Schedular()
        {
            InitializeComponent();
        }

        public void Start()
        {
            OnStart(new string[0]);
        }

        protected override void OnStart(string[] args)
        {
            timer = new tmt.Timer();
            this.timer.Interval = schedulerInterval;
            this.timer.Elapsed += timer_Elapsed;
            this.timer.Enabled = true;
        }

        private void timer_Elapsed(object sender, tmt.ElapsedEventArgs e)
        {
            ProcessServiceInformation psi = new ProcessServiceInformation();
            var conn = ConfigurationManager.ConnectionStrings[Constants.DBConnectionString].ConnectionString;
            psi.FetchJobRecords(conn,DateTime.Now.TimeOfDay,schedulerInterval,numberOfThreads);
        }
        
       // private void timer_tick(object sender, ElapsedEventArgs e)
       // {
            
            //SQLiteConnection sqlite_conn;
            //SQLiteCommand sqlite_cmd;
            //SQLiteCommand sqlite_cmdUpdateStarted;
            //SQLiteCommand sqlite_cmdUpdateCompleted;
            //SQLiteDataReader sqlite_datareader;
            //DataTable dataLoader = new DataTable();
            //try
            //{
            //    sqlite_conn = new SQLiteConnection("Data Source=C:\\Users\\prpandey\\Documents\\Visual Studio 2013\\Projects\\ManageJobs\\ManageJobs\\bin\\Debug\\MSCoPSchedularDB.db;Version=3;New=True;Compress=True;");
            //    sqlite_conn.Open();
            //    sqlite_cmd = sqlite_conn.CreateCommand();
            //    sqlite_cmdUpdateStarted = sqlite_conn.CreateCommand();
            //    sqlite_cmdUpdateCompleted = sqlite_conn.CreateCommand();
            //    string dtCurrentDateTime = System.DateTime.Now.ToString();

            //    string strSQL = "Select JOBID,JOBPATH,JOBEXENAME,JOBSCHEDULEDSTARTDATE from Jobdescription where JobStatus = 'Pending' AND ParentJobID = 0 AND JOBSCHEDULEDSTARTDATE <= '" + dtCurrentDateTime + "'";
            //    strSQL = strSQL + " UNION Select JDS.JOBID,JDS.JOBPATH,JDS.JOBEXENAME,JDS.JOBSCHEDULEDSTARTDATE from Jobdescription PJDS, Jobdescription JDS where PJDS.JOBID = JDS.PARENTJOBID ";
            //    strSQL = strSQL + " AND PJDS.JobStatus = 'Completed' AND JDS.JobStatus = 'Pending' AND JDS.ParentJobID > 0 AND JDS.JOBSCHEDULEDSTARTDATE <= '" + dtCurrentDateTime + "'";
                
            //    sqlite_cmd.CommandText = strSQL;
                
            //    sqlite_datareader = sqlite_cmd.ExecuteReader();
            //    if (sqlite_datareader.HasRows)
            //    {
            //        dataLoader.Load(sqlite_datareader);
            //        Parallel.ForEach(dataLoader.AsEnumerable(), dRow =>
            //        {
            //            sqlite_cmdUpdateStarted.CommandText = "UPDATE JOBDESCRIPTION SET JOBSTATUS = 'Started',JOBSTARTDATE = '" + System.DateTime.Now.ToString() + "' WHERE JOBID = '" + Convert.ToInt16(dRow[0]) + "'";
            //            sqlite_cmdUpdateStarted.ExecuteNonQuery();
            //            ProcessJobs(dRow[1].ToString(), dRow[2].ToString());                       
            //            sqlite_cmdUpdateCompleted.CommandText = "UPDATE JOBDESCRIPTION SET JOBSTATUS = 'Completed',JOBENDDATE = '" + System.DateTime.Now.ToString() + "' WHERE JOBID = '" + Convert.ToInt16(dRow[0]) + "'";
            //            sqlite_cmdUpdateCompleted.ExecuteNonQuery();
            //        });
            //    }
            //    sqlite_datareader.Close();
            //    sqlite_conn.Close();
            //}
            //catch (Exception Ex)
            //{
            //    WriteLog(Ex.Message);
            //}
    //    }

        protected override void OnStop()
        {
            this.timer.Enabled = false;
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
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
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new DailyExecutionService() 
            //};
            //ServiceBase.Run(ServicesToRun);

            //var encoding = Encoding.UTF8;
            //try
            //{
            //    MessageQueue myQueue;
            //    Message myMessage = new System.Messaging.Message();

            //    if (MessageQueue.Exists(@".\Private$\MyQueue"))
            //    {
            //        myQueue = new MessageQueue(@".\Private$\MyQueue");
            //    }
            //    else
            //    {
            //        myQueue = MessageQueue.Create(@".\Private$\MyQueue");
            //    }
            ProcessPeriodicServiceInformation psi = new ProcessPeriodicServiceInformation();
            var conn = System.Configuration.ConfigurationManager.ConnectionStrings[Constants.DBConnectionString].ConnectionString;
            string serviceInterval = ConfigurationManager.AppSettings["SchedulerInterval"];
            int numberOfThreads = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfThreads"].ToString());
            int schedulerInterval = Convert.ToInt32(serviceInterval);
            psi.ExecutePeriodicJobs(conn, DateTime.Now, schedulerInterval, numberOfThreads);

            //    myMessage.BodyStream = new MemoryStream(encoding.GetBytes(json));
            //    myQueue.Formatter = new System.Messaging.ActiveXMessageFormatter();
            //    myQueue.DefaultPropertiesToSend.Priority = System.Messaging.MessagePriority.High;
            //    myQueue.Send(myMessage);

                
            //    //myQueue = new MessageQueue(@".\Private$\MyQueue");
            //    //Message myMessge2 = myQueue.Receive();
            //    //var jsonResult = dbc.Read(myMessge2);
            //}
            //catch (Exception ex)
            //{

            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DScheduler.Common;

namespace DScheduler.BusinessServices
{
    public class ProcessPeriodicServiceInformation
    {
        protected DScheduleEntities dbConnection;
        public void ExecutePeriodicJobs(string conn, DateTime processDate, int schedulerInterval, int numberOfThreads)
        {
            this.dbConnection = new DScheduleEntities(conn);
            try
            {
                if (dbConnection != null)
                {
                    #region ScheduledRecords Processing Starts
                    var fetchProviderDetails = new FetchPeriodicUprocsForExecution(processDate, numberOfThreads, conn);
                    fetchProviderDetails.Execute();
                    #endregion
                }
                Logger.WriteLog("Scheduled Records Processing Finished" + DateTime.Now);

            }

            catch (Exception ex)
            {
                Logger.WriteLog("ProcessePeriodicServiceInformation :  " + ex.Message.ToString() + DateTime.Now);
                Logger.WriteLog("ProcessePeriodicServiceInformation :" + ex.InnerException.ToString() + DateTime.Now);
            }
        }
    }
}

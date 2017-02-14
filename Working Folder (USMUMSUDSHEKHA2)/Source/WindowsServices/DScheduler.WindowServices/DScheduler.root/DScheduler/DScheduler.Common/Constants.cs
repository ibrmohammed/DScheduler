using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DScheduler.Common
{
    public class Constants
    {
        public const string DBConnectionString = "DBConnectionString";
        public const string DefaultConnectionString = "DefaultConnectionString";
        public static string UprocID = "UprocID";
        public static string UProcIDs = "UProcID";

        public static string SessionID = "SessionID";

        public static string DependentID = "DependentID";

        public static string SequenceNumber = "SequenceNumber";
        public static string ProcessDate = "ProcessDate";
        public static string IsActive = "IsActive";
        public static string JobStartTime = "JobStartTime";
        public static string UpdatedBy = "UpdatedBy";
        public static string SpInsertDataIntoJobMonitor = "[DScheduler.uspInsertDataIntoJobMonitor]";
        public static string SpValidateUProcBeforeExecution = "[DScheduler.uspValidateUProcBeforeExecution]";
        public static string SpFetchRecurrentSessionsDetails = "[DScheduler.uspFetchRecurrentSessionsDetails]";
        public static string UpdatedDateTime = "UpdatedDateTime";
        public static object AdminUser = "Admin";
        public static string UProcStatus = "UProcStatus";
        public static string CreatedBy = "CreatedBy";
        public static string StartedDateTime = "StartedDateTime";
        public static string EnvironmentID = "EnvironmentID";
        public static string NodeID = "NodeID";
        public static object DefaultNodeID = "1";
        public static object DefaultEnvironmentID = "2";
        public static string CreatedDateTime = "CreatedDateTime";
        public static string CompletedStatus = "Completed";
        public static string StartedStatus = "Started";
        public static string PendingStatus = "Pending";
        public static string JobActionReRunStatus = "Re-Run";


        public static string UpdatedByDefaultUserName = "Admin";
    }
}

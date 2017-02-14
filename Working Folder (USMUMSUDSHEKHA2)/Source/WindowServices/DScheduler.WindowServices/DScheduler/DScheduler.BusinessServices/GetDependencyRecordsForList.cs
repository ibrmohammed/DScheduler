using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DScheduler.Framework;
using DScheduler.Framework.Helpers;

namespace DScheduler.BusinessServices
{
    public class GetSessionRecordsForList : TaskOut<DataTable>
    {
        public IEnumerable<ScheduledSessionDetails> scheduledRecordsList;
        #region Batch Framework overrides

        /// <summary>
        /// Override batch framework task cleanup method
        /// </summary>
        protected override void CleanUp()
        {
            base.CleanUp();
        }

        /// <summary>
        /// Override batch framework task initialize method
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        #endregion Batch Framework overrides


        protected override DataTable Function()
        {
            DataTable scheduledRecordsDT = new DataTable(typeof(ScheduledSessionDetails).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(ScheduledSessionDetails).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                scheduledRecordsDT.Columns.Add(prop.Name);
            }
            foreach (ScheduledSessionDetails item in scheduledRecordsList)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                scheduledRecordsDT.Rows.Add(values);
            }

            return scheduledRecordsDT;

            //var helper = new SqlClientHelper();
            //var scheduledRecordsDT = helper.GetDataTableByProcedure(Constants.SpValidateScheduledJobs, Constants.DefaultDbConn, true,
            //    new KeyValuePair<string, object>(Constants.ParamCorrespondencePriority, Priority)
            //    , new KeyValuePair<string, object>(Constants.ParamBatchStartdate, BatchStartDate)
            //    , new KeyValuePair<string, object>(Constants.RequestTriggerStatusCode, RequestTriggerStatus));
            //return scheduledRecordsDT;
        }
    }
}


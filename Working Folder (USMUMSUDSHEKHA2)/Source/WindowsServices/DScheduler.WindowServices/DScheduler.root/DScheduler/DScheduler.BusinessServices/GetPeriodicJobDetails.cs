using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DScheduler.Framework;
using DScheduler.Common;
using DScheduler.Framework.Helpers;

namespace DScheduler.BusinessServices
{
    class GetPeriodicJobDetails: TaskOut<DataTable>
    {
        public DateTime ProcessDate;
        public string ConnectionString;
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
            var helper = new SqlClientHelper();
            var scheduledRecordsDT = helper.GetDataTableByProcedure(Constants.SpFetchRecurrentSessionsDetails, Constants.DefaultConnectionString, true,
                new KeyValuePair<string, object>(Constants.ProcessDate, ProcessDate));
            return scheduledRecordsDT;
        }
    
    }
}

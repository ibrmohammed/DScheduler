
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


using DScheduler.Framework.Helpers;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class BulkInsertTableTask : BatchTask
    {
        private readonly string _connectionName;
        private readonly IDatabaseHelper _databaseHelper;
        private readonly DataTable _dataTable;
        private readonly string _tableName;

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="tableName"></param>
        /// <param name="databaseHelper"></param>
        /// <param name="connectionName"></param>
        protected BulkInsertTableTask(DataTable dataTable, string tableName, IDatabaseHelper databaseHelper, string connectionName = "default")
        {
            _tableName = tableName;
            _connectionName = connectionName;
            _databaseHelper = databaseHelper;
            _dataTable = dataTable;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="tableName"></param>
        /// <param name="connectionName"></param>
        protected BulkInsertTableTask(DataTable dataTable, string tableName, string connectionName = "default")
            : this(dataTable, tableName, new SqlClientHelper(), connectionName)
        {
        }

        /// <summary>
        ///
        /// </summary>
        protected override void Execute()
        {
            _databaseHelper.BulkInsert(_dataTable, _tableName, _connectionName);
        }
    }
}
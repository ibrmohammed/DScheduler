
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
    /// <typeparam name="T"></typeparam>
    public class BulkInsertEntitiesTask<T> : BatchTask
    {
        private readonly string _connectionName;
        private readonly IDatabaseHelper _databaseHelper;
        private readonly IEnumerable<T> _entities;
        private readonly Action<T, DataRow> _fillRow;
        private readonly string _tableName;

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="tableName"></param>
        /// <param name="databaseHelper"></param>
        /// <param name="fillRow"></param>
        /// <param name="connectionName"></param>
        protected BulkInsertEntitiesTask(IEnumerable<T> entities, string tableName, IDatabaseHelper databaseHelper, Action<T, DataRow> fillRow, string connectionName = "default")
        {
            _fillRow = fillRow;
            _entities = entities;
            _tableName = tableName;
            _connectionName = connectionName;
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="tableName"></param>
        /// <param name="fillRow"></param>
        /// <param name="connectionName"></param>
        protected BulkInsertEntitiesTask(IEnumerable<T> entities, string tableName, Action<T, DataRow> fillRow, string connectionName = "default")
            : this(entities, tableName, new SqlClientHelper(), fillRow, connectionName)
        {
        }

        /// <summary>
        ///
        /// </summary>
        protected override void Execute()
        {
            _databaseHelper.BulkInsert(_entities, _tableName, _fillRow, _connectionName);
        }
    }
}
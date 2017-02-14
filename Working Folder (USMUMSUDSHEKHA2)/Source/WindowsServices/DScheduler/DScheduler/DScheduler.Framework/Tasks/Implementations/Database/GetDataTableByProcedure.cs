
using System;
using System.Collections.Generic;
using System.Data;
using DScheduler.Framework.Helpers;



namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class GetDataTableByProcedure : GetDataTable
    {
        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        public GetDataTableByProcedure(string procedureName)
            : this(procedureName, new SqlClientHelper())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="databaseHelper"></param>
        public GetDataTableByProcedure(string procedureName, IDatabaseHelper databaseHelper)
            : base(procedureName, (helper, s) => helper.GetDataTableByProcedure(s), databaseHelper)
        {
        }

        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="procedureName"></param>
        /// <param name="parameters"></param>
        public GetDataTableByProcedure(string procedureName, params KeyValuePair<string, object>[] parameters)
            : this(procedureName, new SqlClientHelper(), parameters)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="parameters"></param>
        public GetDataTableByProcedure(string procedureName, IDatabaseHelper databaseHelper, params KeyValuePair<string, object>[] parameters)
            : base(procedureName, (helper, s) => helper.GetDataTableByProcedure(s, parameters: parameters), databaseHelper)
        {
        }

        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="procedureName"></param>
        /// <param name="connectionName"></param>
        public GetDataTableByProcedure(string procedureName, string connectionName)
            : this(procedureName, new SqlClientHelper(), connectionName)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="connectionName"></param>
        public GetDataTableByProcedure(string procedureName, IDatabaseHelper databaseHelper, string connectionName)
            : base(procedureName, (helper, s) => helper.GetDataTableByProcedure(s, connectionName), databaseHelper)
        {
        }

        ///    <summary>
        ///
        ///    </summary>
        ///    <param name="procedureName"></param>
        /// <param name="connectionName"></param>
        /// <param name="parameters"></param>
        public GetDataTableByProcedure(string procedureName, string connectionName, params KeyValuePair<string, object>[] parameters)
            : this(procedureName, new SqlClientHelper(), connectionName, parameters)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="connectionName"></param>
        /// <param name="parameters"></param>
        public GetDataTableByProcedure(string procedureName, IDatabaseHelper databaseHelper, string connectionName, params KeyValuePair<string, object>[] parameters)
            : base(procedureName, (helper, s) => helper.GetDataTableByProcedure(s, connectionName, parameters: parameters), databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        public GetDataTableByProcedure(string text, Func<IDatabaseHelper, string, DataTable> getResult, IDatabaseHelper databaseHelper)
            : base(text, getResult, databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        public GetDataTableByProcedure(string text, Func<IDatabaseHelper, string, DataTable> getResult)
            : base(text, getResult)
        {
        }
    }
}
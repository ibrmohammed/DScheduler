
using System;
using System.Collections.Generic;
using DScheduler.Framework.Helpers;



namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class GetDataSetByProcedure : GetDataSet
    {
        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        public GetDataSetByProcedure(string procedureName)
            : this(procedureName, new SqlClientHelper())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="databaseHelper"></param>
        public GetDataSetByProcedure(string procedureName, IDatabaseHelper databaseHelper)
            : base(procedureName, (helper, s) => helper.GetDataSetByProcedure(s), databaseHelper)
        {
        }

        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="procedureName"></param>
        /// <param name="parameters"></param>
        public GetDataSetByProcedure(string procedureName, params KeyValuePair<string, object>[] parameters)
            : this(procedureName, new SqlClientHelper(), parameters)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="parameters"></param>
        public GetDataSetByProcedure(string procedureName, IDatabaseHelper databaseHelper, params KeyValuePair<string, object>[] parameters)
            : base(procedureName, (helper, s) => helper.GetDataSetByProcedure(s, parameters: parameters), databaseHelper)
        {
        }

        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="procedureName"></param>
        /// <param name="connectionName"></param>
        public GetDataSetByProcedure(string procedureName, string connectionName)
            : this(procedureName, new SqlClientHelper(), connectionName)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="connectionName"></param>
        public GetDataSetByProcedure(string procedureName, IDatabaseHelper databaseHelper, string connectionName)
            : base(procedureName, (helper, s) => helper.GetDataSetByProcedure(s, connectionName), databaseHelper)
        {
        }

        ///    <summary>
        ///
        ///    </summary>
        ///    <param name="procedureName"></param>
        /// <param name="connectionName"></param>
        /// <param name="parameters"></param>
        public GetDataSetByProcedure(string procedureName, string connectionName, params KeyValuePair<string, object>[] parameters)
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
        public GetDataSetByProcedure(string procedureName, IDatabaseHelper databaseHelper, string connectionName, params KeyValuePair<string, object>[] parameters)
            : base(procedureName, (helper, s) => helper.GetDataSetByProcedure(s, connectionName, parameters: parameters), databaseHelper)
        {
        }
    }
}
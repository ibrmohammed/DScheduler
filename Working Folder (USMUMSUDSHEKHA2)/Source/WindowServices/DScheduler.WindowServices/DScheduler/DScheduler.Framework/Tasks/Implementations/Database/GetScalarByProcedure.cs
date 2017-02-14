
using System.Collections.Generic;
using DScheduler.Framework.Helpers;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class GetScalarByProcedure : GetScalar
    {
        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        public GetScalarByProcedure(string procedureName)
            : this(procedureName, new SqlClientHelper())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="databaseHelper"></param>
        public GetScalarByProcedure(string procedureName, IDatabaseHelper databaseHelper)
            : base(procedureName, (helper, s) => helper.GetScalarByProcedure(s), databaseHelper)
        {
        }

        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="procedureName"></param>
        /// <param name="parameters"></param>
        public GetScalarByProcedure(string procedureName, params KeyValuePair<string, object>[] parameters)
            : this(procedureName, new SqlClientHelper(), parameters)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="parameters"></param>
        public GetScalarByProcedure(string procedureName, IDatabaseHelper databaseHelper, params KeyValuePair<string, object>[] parameters)
            : base(procedureName, (helper, s) => helper.GetScalarByProcedure(s, parameters: parameters), databaseHelper)
        {
        }

        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="procedureName"></param>
        /// <param name="connectionName"></param>
        public GetScalarByProcedure(string procedureName, string connectionName)
            : this(procedureName, new SqlClientHelper(), connectionName)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="connectionName"></param>
        public GetScalarByProcedure(string procedureName, IDatabaseHelper databaseHelper, string connectionName)
            : base(procedureName, (helper, s) => helper.GetScalarByProcedure(s, connectionName), databaseHelper)
        {
        }

        ///    <summary>
        ///
        ///    </summary>
        ///    <param name="procedureName"></param>
        /// <param name="connectionName"></param>
        /// <param name="parameters"></param>
        public GetScalarByProcedure(string procedureName, string connectionName, params KeyValuePair<string, object>[] parameters)
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
        public GetScalarByProcedure(string procedureName, IDatabaseHelper databaseHelper, string connectionName, params KeyValuePair<string, object>[] parameters)
            : base(procedureName, (helper, s) => helper.GetScalarByProcedure(s, connectionName, parameters: parameters), databaseHelper)
        {
        }
    }
}
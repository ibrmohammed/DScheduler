
using System.Collections.Generic;
using DScheduler.Framework.Helpers;



namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class GetScalarBySql : GetScalar
    {
        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="sql"></param>
        public GetScalarBySql(string sql)
            : this(sql, new SqlClientHelper())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="databaseHelper"></param>
        public GetScalarBySql(string sql, IDatabaseHelper databaseHelper)
            : base(sql, (helper, s) => helper.GetScalarBySql(s), databaseHelper)
        {
        }

        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="sql"></param>
        /// <param name="connectionName"></param>
        public GetScalarBySql(string sql, string connectionName)
            : this(sql, new SqlClientHelper(), connectionName)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="sql"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="connectionName"></param>
        public GetScalarBySql(string sql, IDatabaseHelper databaseHelper, string connectionName)
            : base(sql, (helper, s) => helper.GetScalarBySql(s, connectionName), databaseHelper)
        {
        }
    }
}
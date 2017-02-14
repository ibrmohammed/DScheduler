
using System;
using System.Data;
using DScheduler.Framework.Helpers;



namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class GetDataSetBySql : GetDataSet
    {
        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="sql"></param>
        public GetDataSetBySql(string sql)
            : this(sql, new SqlClientHelper())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="databaseHelper"></param>
        public GetDataSetBySql(string sql, IDatabaseHelper databaseHelper)
            : base(sql, (helper, s) => helper.GetDataSetBySql(s), databaseHelper)
        {
        }
    }
}
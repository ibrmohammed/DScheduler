
using System;
using System.Data;
using DScheduler.Framework.Helpers;



namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class GetDataTableBySql : GetDataTable
    {
        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="sql"></param>
        public GetDataTableBySql(string sql)
            : this(sql, new SqlClientHelper())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="databaseHelper"></param>
        public GetDataTableBySql(string sql, IDatabaseHelper databaseHelper)
            : base(sql, (helper, s) => helper.GetDataTableBySql(s), databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        public GetDataTableBySql(string text, Func<IDatabaseHelper, string, DataTable> getResult, IDatabaseHelper databaseHelper)
            : base(text, getResult, databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        public GetDataTableBySql(string text, Func<IDatabaseHelper, string, DataTable> getResult)
            : base(text, getResult)
        {
        }
    }
}
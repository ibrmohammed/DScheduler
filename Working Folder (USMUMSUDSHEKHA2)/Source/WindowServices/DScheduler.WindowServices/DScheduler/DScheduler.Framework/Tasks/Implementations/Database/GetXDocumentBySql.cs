
using System;
using System.Xml.Linq;
using DScheduler.Framework.Helpers;



namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class GetXDocumentBySql : GetXDocument
    {
        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="sql"></param>
        public GetXDocumentBySql(string sql)
            : this(sql, new SqlClientHelper())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="databaseHelper"></param>
        public GetXDocumentBySql(string sql, IDatabaseHelper databaseHelper)
            : base(sql, (helper, s) => helper.GetXDocumentBySql(s), databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        public GetXDocumentBySql(string text, Func<IDatabaseHelper, string, XDocument> getResult, IDatabaseHelper databaseHelper)
            : base(text, getResult, databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        public GetXDocumentBySql(string text, Func<IDatabaseHelper, string, XDocument> getResult)
            : base(text, getResult)
        {
        }
    }
}
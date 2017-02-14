
using System;
using System.Xml;
using DScheduler.Framework.Helpers;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class GetXmlDocumentBySql : GetXmlDocument
    {
        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="sql"></param>
        public GetXmlDocumentBySql(string sql)
            : this(sql, new SqlClientHelper())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="databaseHelper"></param>
        public GetXmlDocumentBySql(string sql, IDatabaseHelper databaseHelper)
            : base(sql, (helper, s) => helper.GetXmlDocumentBySql(s), databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        public GetXmlDocumentBySql(string text, Func<IDatabaseHelper, string, XmlDocument> getResult, IDatabaseHelper databaseHelper)
            : base(text, getResult, databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        public GetXmlDocumentBySql(string text, Func<IDatabaseHelper, string, XmlDocument> getResult)
            : base(text, getResult)
        {
        }
    }
}
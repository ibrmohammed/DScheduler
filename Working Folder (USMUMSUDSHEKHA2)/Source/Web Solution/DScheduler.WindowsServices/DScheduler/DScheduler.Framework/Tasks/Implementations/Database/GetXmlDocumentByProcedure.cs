
using System;
using System.Collections.Generic;
using System.Xml;
using DScheduler.Framework.Helpers;



namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class GetXmlDocumentByProcedure : GetXmlDocument
    {
        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        public GetXmlDocumentByProcedure(string procedureName)
            : this(procedureName, new SqlClientHelper())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="databaseHelper"></param>
        public GetXmlDocumentByProcedure(string procedureName, IDatabaseHelper databaseHelper)
            : base(procedureName, (helper, s) => helper.GetXmlDocumentByProcedure(s), databaseHelper)
        {
        }

        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="procedureName"></param>
        /// <param name="parameters"></param>
        public GetXmlDocumentByProcedure(string procedureName, params KeyValuePair<string, object>[] parameters)
            : this(procedureName, new SqlClientHelper(), parameters)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="parameters"></param>
        public GetXmlDocumentByProcedure(string procedureName, IDatabaseHelper databaseHelper, params KeyValuePair<string, object>[] parameters)
            : base(procedureName, (helper, s) => helper.GetXmlDocumentByProcedure(s, parameters: parameters), databaseHelper)
        {
        }

        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="procedureName"></param>
        /// <param name="connectionName"></param>
        public GetXmlDocumentByProcedure(string procedureName, string connectionName)
            : this(procedureName, new SqlClientHelper(), connectionName)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedureName"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="connectionName"></param>
        public GetXmlDocumentByProcedure(string procedureName, IDatabaseHelper databaseHelper, string connectionName)
            : base(procedureName, (helper, s) => helper.GetXmlDocumentByProcedure(s, connectionName), databaseHelper)
        {
        }

        ///    <summary>
        ///
        ///    </summary>
        ///    <param name="procedureName"></param>
        /// <param name="connectionName"></param>
        /// <param name="parameters"></param>
        public GetXmlDocumentByProcedure(string procedureName, string connectionName, params KeyValuePair<string, object>[] parameters)
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
        public GetXmlDocumentByProcedure(string procedureName, IDatabaseHelper databaseHelper, string connectionName, params KeyValuePair<string, object>[] parameters)
            : base(procedureName, (helper, s) => helper.GetXmlDocumentByProcedure(s, connectionName, parameters: parameters), databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        public GetXmlDocumentByProcedure(string text, Func<IDatabaseHelper, string, XmlDocument> getResult, IDatabaseHelper databaseHelper)
            : base(text, getResult, databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        public GetXmlDocumentByProcedure(string text, Func<IDatabaseHelper, string, XmlDocument> getResult)
            : base(text, getResult)
        {
        }
    }
}
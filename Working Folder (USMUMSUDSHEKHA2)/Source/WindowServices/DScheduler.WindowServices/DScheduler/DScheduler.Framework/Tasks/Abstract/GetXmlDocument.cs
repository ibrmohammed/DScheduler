
using System;
using System.Xml;
using DScheduler.Framework.Helpers;


namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class GetXmlDocument : GetResult<XmlDocument>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        public GetXmlDocument(string text, Func<IDatabaseHelper, string, XmlDocument> getResult, IDatabaseHelper databaseHelper)
            : base(text, getResult, databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        public GetXmlDocument(string text, Func<IDatabaseHelper, string, XmlDocument> getResult)
            : base(text, getResult)
        {
        }
    }
}
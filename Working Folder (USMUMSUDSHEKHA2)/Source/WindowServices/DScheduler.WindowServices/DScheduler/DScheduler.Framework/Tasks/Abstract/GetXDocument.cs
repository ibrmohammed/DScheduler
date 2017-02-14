
using System;
using System.Xml.Linq;
using DScheduler.Framework.Helpers;


namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class GetXDocument : GetResult<XDocument>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        public GetXDocument(string text, Func<IDatabaseHelper, string, XDocument> getResult, IDatabaseHelper databaseHelper)
            : base(text, getResult, databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        public GetXDocument(string text, Func<IDatabaseHelper, string, XDocument> getResult)
            : base(text, getResult)
        {
        }
    }
}
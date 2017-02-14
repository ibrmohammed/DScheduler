
using System;
using System.Data;
using System.Data.Common;
using DScheduler.Framework.Helpers;


namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class GetDataReader : GetResult<DbDataReader>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        public GetDataReader(string text, Func<IDatabaseHelper, string, DbDataReader> getResult, IDatabaseHelper databaseHelper)
            : base(text, getResult, databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        public GetDataReader(string text, Func<IDatabaseHelper, string, DbDataReader> getResult)
            : base(text, getResult)
        {
        }
    }
}
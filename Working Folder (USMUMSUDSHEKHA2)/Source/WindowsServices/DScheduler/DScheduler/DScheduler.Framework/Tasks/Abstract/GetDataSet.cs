
using System;
using System.Data;
using DScheduler.Framework.Helpers;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class GetDataSet : GetResult<DataSet>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        public GetDataSet(string text, Func<IDatabaseHelper, string, DataSet> getResult, IDatabaseHelper databaseHelper)
            : base(text, getResult, databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        public GetDataSet(string text, Func<IDatabaseHelper, string, DataSet> getResult)
            : base(text, getResult)
        {
        }
    }
}
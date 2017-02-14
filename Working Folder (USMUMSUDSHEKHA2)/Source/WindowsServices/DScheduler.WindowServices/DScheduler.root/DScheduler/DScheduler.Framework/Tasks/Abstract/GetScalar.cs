
using System;
using System.Data;
using DScheduler.Framework.Helpers;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class GetScalar : GetResult<object>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        public GetScalar(string text, Func<IDatabaseHelper, string, object> getResult, IDatabaseHelper databaseHelper)
            : base(text, getResult, databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        public GetScalar(string text, Func<IDatabaseHelper, string, object> getResult)
            : base(text, getResult)
        {
        }
    }
}
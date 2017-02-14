
using System;

using DScheduler.Framework.Helpers;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class GetResult<TResult> : TaskInOut<string, TResult>
    {
        private readonly IDatabaseHelper _databaseHelper;
        private readonly Func<IDatabaseHelper, string, TResult> _getResult;
        private readonly string _text;

        ///   <summary>
        ///
        ///   </summary>
        ///  <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        protected GetResult(string text, Func<IDatabaseHelper, string, TResult> getResult, IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
            _text = text;
            _getResult = getResult;
        }

        ///  <summary>
        /// The constructor takes an SQL statement in the parameter
        ///  </summary>
        ///  <param name="text">SQL statement</param>
        /// <param name="getResult"></param>
        protected GetResult(string text, Func<IDatabaseHelper, string, TResult> getResult)
            : this(text, getResult, new SqlClientHelper())
        {
        }

        /// <summary>
        /// The argument to be passed to the task
        /// </summary>
        public override string Argument
        {
            get
            {
                return _text;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        protected override TResult Execute(string argument)
        {
            var result = _getResult(_databaseHelper, argument);
            return result;
        }
    }
}
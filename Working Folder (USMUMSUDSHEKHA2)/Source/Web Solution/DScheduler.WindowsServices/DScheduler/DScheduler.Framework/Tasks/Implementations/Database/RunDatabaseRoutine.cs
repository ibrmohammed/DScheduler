
using System;

using DScheduler.Framework.Helpers;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class RunDatabaseRoutine : TaskOut<int>
    {
        private readonly IDatabaseHelper _databaseHelper;
        private readonly string _procedure;
        private readonly Func<IDatabaseHelper, string, int> _runProcedure;

        /// <summary>
        ///
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="runProcedure"></param>
        protected RunDatabaseRoutine(string procedure, Func<IDatabaseHelper, string, int> runProcedure)
            : this(procedure, runProcedure, new SqlClientHelper())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="runProcedure"></param>
        /// <param name="databaseHelper"></param>
        protected RunDatabaseRoutine(string procedure, Func<IDatabaseHelper, string, int> runProcedure, IDatabaseHelper databaseHelper)
        {
            _procedure = procedure;
            _runProcedure = runProcedure;
            _databaseHelper = databaseHelper;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override int Function()
        {
            return _runProcedure(_databaseHelper, _procedure);
        }
    }
}
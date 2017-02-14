
using System.Collections.Generic;
using DScheduler.Framework.Helpers;


namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class RunProcedureTask : RunDatabaseRoutine
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="procedure"></param>
        protected RunProcedureTask(string procedure)
            : this(procedure, new SqlClientHelper())
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedure"></param>
        /// <param name="parameters"></param>
        protected RunProcedureTask(string procedure, params KeyValuePair<string, object>[] parameters)
            : this(procedure, new SqlClientHelper(), parameters)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedure"></param>
        ///  <param name="databaseHelper"></param>
        protected RunProcedureTask(string procedure, IDatabaseHelper databaseHelper)
            : base(procedure, (helper, s) => helper.RunProcedure(s), databaseHelper)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedure"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="connectionName"></param>
        protected RunProcedureTask(string procedure, IDatabaseHelper databaseHelper, string connectionName)
            : base(procedure, (helper, s) => helper.RunProcedure(s, connectionName), databaseHelper)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedure"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="parameters"></param>
        protected RunProcedureTask(string procedure, IDatabaseHelper databaseHelper, params KeyValuePair<string, object>[] parameters)
            : base(procedure, (helper, s) => helper.RunProcedure(s, parameters: parameters), databaseHelper)
        {
        }

        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="procedure"></param>
        ///   <param name="databaseHelper"></param>
        /// <param name="connectionName"></param>
        /// <param name="parameters"></param>
        protected RunProcedureTask(string procedure, IDatabaseHelper databaseHelper, string connectionName, params KeyValuePair<string, object>[] parameters)
            : base(procedure, (helper, s) => helper.RunProcedure(s, connectionName, parameters: parameters), databaseHelper)
        {
        }
    }
}
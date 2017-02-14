


using DScheduler.Framework.Helpers;
namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class RunSqlTask : RunDatabaseRoutine
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        protected RunSqlTask(string sql)
            : this(sql, new SqlClientHelper())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="databaseHelper"></param>
        protected RunSqlTask(string procedure, IDatabaseHelper databaseHelper)
            : base(procedure, (helper, s) => helper.RunSql(s), databaseHelper)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="sql"></param>
        /// <param name="connectionName"></param>
        protected RunSqlTask(string sql, string connectionName)
            : this(sql, new SqlClientHelper(), connectionName)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="procedure"></param>
        ///  <param name="databaseHelper"></param>
        /// <param name="connectionName"></param>
        protected RunSqlTask(string procedure, IDatabaseHelper databaseHelper, string connectionName)
            : base(procedure, (helper, s) => helper.RunSql(s, connectionName), databaseHelper)
        {
        }
    }
}
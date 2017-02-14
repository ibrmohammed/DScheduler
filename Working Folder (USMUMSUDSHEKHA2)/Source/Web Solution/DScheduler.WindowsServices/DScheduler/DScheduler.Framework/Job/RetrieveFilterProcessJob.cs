
namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class RetrieveFilterProcessJob<TEntity, TFilterTask, TEnumerator> : Job
        where TEntity : new()
        where TFilterTask : FilterDataTable<TEntity>, new()
        where TEnumerator : EnumerateTask<TEntity>, new()
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="procedureName"></param>
        protected RetrieveFilterProcessJob(string name, string procedureName)
            : base()
        {
            ProcedureName = procedureName;
        }

        /// <summary>
        ///
        /// </summary>
        protected string ProcedureName { get; set; }

        /// <summary>
        ///The root task for this job
        /// </summary>
        protected override ITask Task
        {
            get
            {
                var result = new GetDataTableByProcedure(ProcedureName);
                result.Chain<TFilterTask>().Chain<TEnumerator>();
                return result;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public abstract class RetrieveFilterProcessJob<TEntity, TConverter, TFilterTask, TEnumerator, TProcess> : Job
        where TEntity : new()
        where TConverter : DataRowConverter<TEntity>, new()
        where TFilterTask : FilterDataTable<TEntity, TConverter>, new()
        where TProcess : ITaskIn<TEntity>, new()
        where TEnumerator : EnumerateWithChildTask<TEntity, TProcess>, new()
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="procedureName"></param>
        protected RetrieveFilterProcessJob(string name, string procedureName)
            : base()
        {
            ProcedureName = procedureName;
        }

        /// <summary>
        ///
        /// </summary>
        protected string ProcedureName { get; set; }

        /// <summary>
        ///The root task for this job
        /// </summary>
        protected override ITask Task
        {
            get
            {
                var result = new GetDataTableByProcedure(ProcedureName);
                result.Chain<TFilterTask>().Chain<TEnumerator>();
                return result;
            }
        }
    }
}
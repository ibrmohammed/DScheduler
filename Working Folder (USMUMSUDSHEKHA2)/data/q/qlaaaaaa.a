using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TConverter"></typeparam>
    public abstract class FilterDataTable<TEntity, TConverter> : FilterDataTable<TEntity>
        where TConverter : DataRowConverter<TEntity>, new()
        where TEntity : new()
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        protected override TEntity ConvertToEntity(DataRow dataRow)
        {
            return new TConverter().GetEntity(dataRow);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public abstract class FilterDataTable : FilterDataTable<DataRow>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        protected override DataRow ConvertToEntity(DataRow dataRow)
        {
            return dataRow;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FilterDataTable<T> : TaskInOut<DataTable, IEnumerable<T>>, IDisposable
    {
        private readonly ConcurrentBag<ConditionInfo<T>> _conditions = new ConcurrentBag<ConditionInfo<T>>();
        private readonly ITaskIn<ConditionInfo<T>> _failTask;
        private readonly ITaskIn<ConditionInfo<T>> _passTask;
        private DataTable _argument;

        /// <summary>
        ///
        /// </summary>
        protected FilterDataTable()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="passTask"></param>
        /// <param name="failTask"></param>
        protected FilterDataTable(ITaskIn<ConditionInfo<T>> passTask, ITaskIn<ConditionInfo<T>> failTask)
        {
            _passTask = passTask;
            _failTask = failTask;
        }

        /// <summary>
        ///
        /// </summary>
        ~FilterDataTable()
        {
            Dispose(false);
        }

        /// <summary>
        ///
        /// </summary>
        public event EventHandler<ConditionEventArgs<T>> ConditionFailed;

        /// <summary>
        ///
        /// </summary>
        public event EventHandler<ConditionEventArgs<T>> ConditionPassed;

        /// <summary>
        ///
        /// </summary>
        public override DataTable Argument
        {
            get
            {
                return _argument;
            }
        }

        /// <summary>
        ///
        /// </summary>
        protected IEnumerable<ConditionInfo<T>> Conditions
        {
            get
            {
                return _conditions;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name">The name of the condition</param>
        /// <param name="condition">The condition itself</param>
        /// <returns>The created ConditionInfo object</returns>
        protected ConditionInfo<T> AddCondition(string name, Predicate<T> condition)
        {
            return AddCondition(name, condition, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name">The name of the condition</param>
        /// <param name="condition">The condition itself</param>
        /// <param name="reverse">whether to check for a reverse condition</param>
        /// <returns>The created ConditionInfo object</returns>
        protected ConditionInfo<T> AddCondition(string name, Predicate<T> condition, bool reverse)
        {
            return AddCondition(name, condition, reverse, string.Format("Invalid {0}", name), string.Format("Valid {0}", name));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name">The name of the condition</param>
        /// <param name="condition">The condition itself</param>
        /// <param name="failureMessage">The message to display/store when the condition is met</param>
        /// <param name="reverse">whether to check for a reverse condition</param>
        /// <returns>The created ConditionInfo object</returns>
        protected ConditionInfo<T> AddCondition(string name, Predicate<T> condition, bool reverse, string failureMessage)
        {
            return AddCondition(name, condition, reverse, failureMessage, string.Format("Valid {0}", name));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name">The name of the condition</param>
        /// <param name="condition">The condition itself</param>
        /// <param name="successMessage">The message to display/store when the condition is met</param>
        /// <param name="failureMessage">The message to display/store when the condition is not met</param>
        /// <param name="reverse">whether to check for a reverse condition</param>
        /// <returns>The created ConditionInfo object</returns>
        protected ConditionInfo<T> AddCondition(string name, Predicate<T> condition, bool reverse, string successMessage, string failureMessage)
        {
            var result = new ConditionInfo<T>(name, condition, reverse, successMessage, failureMessage);
            _conditions.Add(result);
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="conditionInfo"></param>
        protected void AddCondition(ConditionInfo<T> conditionInfo)
        {
            _conditions.Add(conditionInfo);
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="entity"></param>
        ///  <param name="result"></param>
        /// <param name="passedConditions"></param>
        /// <param name="failedConditions"></param>
        protected virtual void AfterValidate(T entity, bool result, ReadOnlyCollection<ConditionInfo<T>> passedConditions, ReadOnlyCollection<ConditionInfo<T>> failedConditions)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void BeforeValidate(T entity)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        protected abstract T ConvertToEntity(DataRow dataRow);

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (_argument != null)
                {
                    _argument.Dispose();
                    _argument = null;
                }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        protected override IEnumerable<T> Execute(DataTable argument)
        {
            var result = new List<T>();

            if (argument != null)
            {
                var entities = argument.Rows.Cast<DataRow>().Select<DataRow, T>(ConvertToEntity);
                result.AddRange(entities.Where(IsValid));
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected bool IsValid(T entity)
        {
            var passedConditions = new ConcurrentBag<ConditionInfo<T>>();
            var failedConditions = new ConcurrentBag<ConditionInfo<T>>();

            var result = true;

            BeforeValidate(entity);

            var loopResult = Parallel.ForEach(Conditions, condition =>
            {
                var conditionPassed = condition.Condition(entity) ? !condition.Reverse : condition.Reverse;

                if (conditionPassed)
                {
                    passedConditions.Add(condition);

                    if (_passTask != null)
                        _passTask.Run(condition);

                    if (ConditionPassed != null)
                        ConditionPassed(entity, new ConditionEventArgs<T>(condition));
                }
                else
                {
                    failedConditions.Add(condition);
                    result = false;

                    if (_failTask != null)
                        _failTask.Run(condition);

                    if (ConditionFailed != null)
                        ConditionFailed(entity, new ConditionEventArgs<T>(condition));
                }
            });

            if (loopResult.IsCompleted)
                AfterValidate(entity, result, passedConditions.ToList().AsReadOnly(), failedConditions.ToList().AsReadOnly());

            if (result)
                OnSuccess(entity);
            else
                OnFailure(entity, failedConditions.ToList().AsReadOnly());

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="failedConditions"></param>
        protected virtual void OnFailure(T entity, ReadOnlyCollection<ConditionInfo<T>> failedConditions)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void OnSuccess(T entity)
        {
        }
    }
}
using System;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class TaskOut<TResult> : BatchTask, ITaskOut<TResult>
    {
        private ITaskIn<TResult> _nextTask;

        /// <summary>
        /// Occurs when [task error].
        /// </summary>
        public override event EventHandler TaskException;

        /// <summary>
        /// The result for the task
        /// </summary>
        public TResult Result { get; protected set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nextTask"></param>
        public TTask Chain<TTask>(TTask nextTask) where TTask : ITaskIn<TResult>
        {
            _nextTask = nextTask;

            return nextTask;
        }

        /// <summary>
        ///
        /// </summary>
        public TTask Chain<TTask>() where TTask : ITaskIn<TResult>, new()
        {
            var nextTask = new TTask();

            _nextTask = nextTask;

            return nextTask;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        void ITask.Run()
        {
            Result = ((ITaskOut<TResult>)this).Run();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        TResult ITaskOut<TResult>.Run()
        {
            Initialize();

            Execute();

            var task = Task.Factory.StartNew(() =>
                {
                    if (_nextTask != null)
                    {
                        _nextTask.Run(Result);
                    }
                });

            task.Wait();

            CleanUp();

            return Result;
        }

        /// <summary>
        ///
        /// </summary>
        protected override void Execute()
        {
            try
            {
                Result = Function();
            }
            catch (Exception exception)
            {
                if (this.TaskException == null)
                {
                    throw;
                }

                BatchLogger.LogBatchException(exception);
                this.OnTaskException();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected abstract TResult Function();

        /// <summary>
        /// Called when [task error].
        /// </summary>
        protected override void OnTaskException()
        {
            EventHandler handler = this.TaskException;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
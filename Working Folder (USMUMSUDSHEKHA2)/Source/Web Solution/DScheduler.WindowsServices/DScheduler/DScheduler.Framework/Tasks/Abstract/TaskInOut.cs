using System.Threading.Tasks;

namespace DScheduler.Framework
{
    using System;

    /// <summary>
    /// A task that takes an input of type TArgument
    /// </summary>
    /// <typeparam name="TArgument">The type of the argument.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public abstract class TaskInOut<TArgument, TResult> : BatchTask, ITaskInOut<TArgument, TResult>
    {
        private ITaskIn<TResult> _nextTask;

        /// <summary>
        /// Occurs when [task error].
        /// </summary>
        public override event EventHandler TaskException;

        /// <summary>
        /// The argument to be passed to the task
        /// </summary>
        public abstract TArgument Argument
        {
            get;
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public TResult Result
        {
            get;
            protected set;
        }

        /// <summary>
        /// Chains the specified next task.
        /// </summary>
        /// <typeparam name="TTask">The type of the task.</typeparam>
        /// <param name="nextTask">The next task.</param>
        /// <returns></returns>
        public TTask Chain<TTask>(TTask nextTask) where TTask : ITaskIn<TResult>
        {
            _nextTask = nextTask;

            return nextTask;
        }

        /// <summary>
        /// Chains this instance.
        /// </summary>
        /// <typeparam name="TTask">The type of the task.</typeparam>
        /// <returns></returns>
        public TTask Chain<TTask>() where TTask : ITaskIn<TResult>, new()
        {
            var nextTask = new TTask();

            _nextTask = nextTask;

            return nextTask;
        }

        /// <summary>
        /// Does all three in the sequence Initialize, Execute and Clean Up
        /// </summary>
        void ITask.Run()
        {
            Result = ((ITaskInOut<TArgument, TResult>)this).Run(Argument);
        }

        /// <summary>
        /// Runs the specified argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        void ITaskIn<TArgument>.Run(TArgument argument)
        {
            Result = ((ITaskInOut<TArgument, TResult>)this).Run(argument);
        }

        /// <summary>
        /// Runs the specified argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <returns></returns>
        TResult ITaskInOut<TArgument, TResult>.Run(TArgument argument)
        {
            TResult result = default(TResult);
            try
            {
                Initialize();
                result = Execute(argument);
            }
            catch (Exception exception)
            {
                if (this.TaskException == null)
                {
                    throw;
                }

               // BatchLogger.LogBatchException(exception);
                this.OnTaskException();
            }

            var task = Task.Factory.StartNew(() =>
            {
                if (_nextTask != null)
                {
                    _nextTask.Run(result);
                }
            });
            task.Wait();
            CleanUp();

            return result;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        /// <returns></returns>
        TResult ITaskOut<TResult>.Run()
        {
            Result = ((ITaskInOut<TArgument, TResult>)this).Run(Argument);
            return Result;
        }

        /// <summary>
        /// The core method to execute the task
        /// </summary>
        protected override void Execute()
        {
            try
            {
                Result = Execute(Argument);
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
        /// Executes the specified argument.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <returns></returns>
        protected abstract TResult Execute(TArgument argument);

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
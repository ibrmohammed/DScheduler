using System;
using System.Threading.Tasks;
using DScheduler.Framework;
namespace DScheduler.Framework
{
    /// <summary>
    /// Default Task that executes an Action
    /// </summary>
    public abstract class BatchTask : ITask
    {
        /// <summary>
        /// The batch logger
        /// </summary>
        internal readonly IBatchLogger BatchLogger = new BatchLogger();

        /// <summary>
        /// Occurs when [log interface exception].
        /// </summary>
        public event EventHandler<InterfaceExceptionArgs> LogInterfaceException;

        /// <summary>
        /// Occurs when [log status].
        /// </summary>
        public event EventHandler<StatusArgs> LogStatus;

        #region ITaskError Members

        /// <summary>
        /// Occurs when [task error].
        /// </summary>
        public virtual event EventHandler TaskException;

        #endregion

        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <param name="task">The task.</param>
        public async static void RunTask(ITask task)
        {
            await Task.Factory.StartNew(task.Run);
        }

        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task">The task.</param>
        /// <param name="argument">The argument.</param>
        public async static void RunTask<T>(ITaskIn<T> task, T argument)
        {
            await Task.Factory.StartNew(() => task.Run(argument));
        }

        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="task">The task.</param>
        /// <returns></returns>
        public async static Task<TResult> RunTask<TResult>(ITaskOut<TResult> task)
        {
            return await Task.Factory.StartNew<TResult>(task.Run);
        }

        /// <summary>
        /// Task Name
        /// </summary>
        /// <value>
        /// The name of the task.
        /// </value>
        public virtual string TaskName
        {
            get { return GetType().Name; }
        }

        /// <summary>
        /// Friendly Name
        /// </summary>
        /// <value>
        /// The name of the friendly.
        /// </value>
        public virtual string FriendlyName
        {
            get { return TaskName.AsFriendlyName(); }
        }

        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="task">The task.</param>
        /// <param name="argument">The argument.</param>
        /// <returns></returns>
        public async static Task<TResult> RunTask<TArgument, TResult>(ITaskInOut<TArgument, TResult> task, TArgument argument)
        {
            return await Task.Factory.StartNew(() => task.Run(argument));
        }

        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <param name="action">The action.</param>
        public async static void RunTask(Action action)
        {
            await Task.Factory.StartNew(action);
        }

        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <param name="argument">The argument.</param>
        public async static void RunTask<T>(Action<T> action, T argument)
        {
            await Task.Factory.StartNew(() => action(argument));
        }

        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        public async static Task<TResult> RunTask<TResult>(Func<TResult> function)
        {
            return await Task.Factory.StartNew(function);
        }

        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <typeparam name="TArgument">The type of the argument.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="argument">The argument.</param>
        /// <returns></returns>
        public async static Task<TResult> RunTask<TArgument, TResult>(Func<TArgument, TResult> function, TArgument argument)
        {
            return await Task.Factory.StartNew(() => function(argument));
        }

        /// <summary>
        /// Does all three in the sequence Initialize, Execute and Clean Up
        /// </summary>
        void ITask.Run()
        {
            Initialize();
            Execute();
            CleanUp();
        }

        /// <summary>
        /// Provides a method for any clean up activity needed for the task
        /// </summary>
        protected virtual void CleanUp()
        {
            //Console.WriteLine("Cleaning Up {0}...", this);
        }

        /// <summary>
        /// The core method to execute the task
        /// </summary>
        protected abstract void Execute();

        /// <summary>
        /// Provides a method for any initialization needed for the task
        /// </summary>
        protected virtual void Initialize()
        {
            //Console.WriteLine("Initializing {0}...", this);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual void Log(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Raises the interface exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        protected void RaiseInterfaceException(IDSchedulerException exception)
        {
            if (LogInterfaceException != null)
            {
                LogInterfaceException(this, new InterfaceExceptionArgs(exception));
            }
        }

        /// <summary>
        /// Raises the status.
        /// </summary>
        /// <param name="status">The status.</param>
        protected void RaiseStatus(IStatusInfo status)
        {
            if (LogStatus != null)
            {
                LogStatus(this, new StatusArgs(status));
            }
        }

        /// <summary>
        /// Called when [task error].
        /// </summary>
        protected virtual void OnTaskException()
        {
            EventHandler handler = this.TaskException;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
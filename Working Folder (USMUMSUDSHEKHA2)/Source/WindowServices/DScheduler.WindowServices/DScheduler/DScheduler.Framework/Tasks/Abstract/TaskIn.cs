namespace DScheduler.Framework
{
    using System;

    /// <summary>
    /// A task that takes an input of type TArgument
    /// </summary>
    /// <typeparam name="TArgument">The input type</typeparam>
    public abstract class TaskIn<TArgument> : BatchTask, ITaskIn<TArgument>
    {
        private bool _initialized;

        #region ITaskError Members

        /// <summary>
        /// Occurs when [task error].
        /// </summary>
        public override event EventHandler TaskException;

        #endregion

        /// <summary>
        /// The argument to be passed to the task
        /// </summary>
        public virtual TArgument Argument
        {
            get
            {
                return default(TArgument);
            }
        }

        /// <summary>
        /// The core method for the task
        /// </summary>
        void ITask.Run()
        {
            Initialize();
            _initialized = true;
            ((ITaskIn<TArgument>)this).Run(Argument);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        void ITaskIn<TArgument>.Run(TArgument argument)
        {
            if (!_initialized)
            {
                Initialize();
            }

            try
            {
                Execute(argument);
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

            CleanUp();
        }

        /// <summary>
        ///
        /// </summary>
        protected override void Execute()
        {
            try
            {
                Execute(Argument);
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
        /// <param name="argument"></param>
        protected abstract void Execute(TArgument argument);

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
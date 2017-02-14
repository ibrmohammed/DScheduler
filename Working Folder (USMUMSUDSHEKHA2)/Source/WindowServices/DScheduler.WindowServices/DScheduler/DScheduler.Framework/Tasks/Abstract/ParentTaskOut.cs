using System;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ParentTaskOut<T, TResult> : ParentTask<T>, IParentTaskOut<T, TResult>
    {
        private ITaskIn<TResult> _nextTask;

        /// <summary>
        ///
        /// </summary>
        /// <param name="child"></param>
        protected ParentTaskOut(ITaskOut<T> child)
            : base(child)
        {
        }

        #region ITaskError Members

        /// <summary>
        /// Occurs when [task error].
        /// </summary>
        public override event EventHandler TaskException;

        #endregion

        /// <summary>
        ///
        /// </summary>
        public TResult Result
        {
            get;
            protected set;
        }

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
                if (Child == null)
                    return;

                Task.Factory.StartNew<T>(Child.Run).ContinueWith(t => Result = Function(t.Result));
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
        protected abstract TResult Function(T t);

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

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ParentTaskOut<T1, T2, TResult> : ParentTask<T1, T2>, IParentTaskOut<T1, T2, TResult>
    {
        private ITaskIn<TResult> _nextTask;

        /// <summary>
        ///
        /// </summary>
        /// <param name="child1"></param>
        /// <param name="child2"></param>
        protected ParentTaskOut(ITaskOut<T1> child1, ITaskOut<T2> child2)
            : base(child1, child2)
        {
        }

        #region ITaskError Members

        /// <summary>
        /// Occurs when [task error].
        /// </summary>
        public override event EventHandler TaskException;

        #endregion

        /// <summary>
        ///
        /// </summary>
        public TResult Result
        {
            get;
            private set;
        }

        /// <param name="nextTask"></param>
        public TTask Chain<TTask>(TTask nextTask) where TTask : ITaskIn<TResult>
        {
            _nextTask = nextTask;

            return nextTask;
        }

        TResult ITaskOut<TResult>.Run()
        {
            Initialize();
            Execute();
            CleanUp();
            return Result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override Action<T1, T2> Action()
        {
            return (t1, t2) => Result = Function(t1, t2);
        }

        /// <summary>
        ///
        /// </summary>
        protected override void Execute()
        {
            try
            {
                if (Child1 == null || Child2 == null)
                {
                    return;
                }

                var task1 = Task.Factory.StartNew<T1>(Child1.Run);
                var task2 = Task.Factory.StartNew<T2>(Child2.Run);

                Task.WaitAll(task1, task2);

                Result = Function(task1.Result, task2.Result);
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
        protected abstract TResult Function(T1 t1, T2 t2);

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

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ParentTaskOut<T1, T2, T3, TResult> : ParentTask<T1, T2, T3>, IParentTaskOut<T1, T2, T3, TResult>
    {
        private ITaskIn<TResult> _nextTask;

        /// <summary>
        ///
        /// </summary>
        /// <param name="child1"></param>
        /// <param name="child2"></param>
        /// <param name="child3"></param>
        protected ParentTaskOut(ITaskOut<T1> child1, ITaskOut<T2> child2, ITaskOut<T3> child3)
            : base(child1, child2, child3)
        {
        }

        #region ITaskError Members

        /// <summary>
        /// Occurs when [task error].
        /// </summary>
        public override event EventHandler TaskException;

        #endregion

        /// <summary>
        ///
        /// </summary>
        public TResult Result
        {
            get;
            private set;
        }

        /// <param name="nextTask"></param>
        public TTask Chain<TTask>(TTask nextTask) where TTask : ITaskIn<TResult>
        {
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
        /// <returns></returns>
        protected override Action<T1, T2, T3> Action()
        {
            return (t1, t2, t3) => Result = Function(t1, t2, t3);
        }

        /// <summary>
        ///
        /// </summary>
        protected override void Execute()
        {
            try
            {
                if (Child1 == null || Child2 == null || Child3 == null)
                {
                    return;
                }

                var task1 = Task.Factory.StartNew<T1>(Child1.Run);
                var task2 = Task.Factory.StartNew<T2>(Child2.Run);
                var task3 = Task.Factory.StartNew<T3>(Child3.Run);

                Task.WaitAll(task1, task2, task3);

                Result = Function(task1.Result, task2.Result, task3.Result);
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
        protected abstract TResult Function(T1 t1, T2 t2, T3 t3);

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

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ParentTaskOut<T1, T2, T3, T4, TResult> : ParentTask<T1, T2, T3, T4>, IParentTaskOut<T1, T2, T3, T4, TResult>
    {
        private ITaskIn<TResult> _nextTask;

        /// <summary>
        ///
        /// </summary>
        /// <param name="child1"></param>
        /// <param name="child2"></param>
        /// <param name="child3"></param>
        /// <param name="child4"></param>
        protected ParentTaskOut(ITaskOut<T1> child1, ITaskOut<T2> child2, ITaskOut<T3> child3, ITaskOut<T4> child4)
            : base(child1, child2, child3, child4)
        {
        }

        #region ITaskError Members

        /// <summary>
        /// Occurs when [task error].
        /// </summary>
        public override event EventHandler TaskException;

        #endregion

        /// <summary>
        ///
        /// </summary>
        public TResult Result
        {
            get;
            private set;
        }

        /// <param name="nextTask"></param>
        public TTask Chain<TTask>(TTask nextTask) where TTask : ITaskIn<TResult>
        {
            _nextTask = nextTask;

            return nextTask;
        }

        TResult ITaskOut<TResult>.Run()
        {
            Initialize();
            Execute();
            CleanUp();
            return Result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override Action<T1, T2, T3, T4> Action()
        {
            return (t1, t2, t3, t4) => Result = Function(t1, t2, t3, t4);
        }

        /// <summary>
        ///
        /// </summary>
        protected override void Execute()
        {
            try
            {
                if (Child1 == null || Child2 == null || Child3 == null || Child4 == null)
                {
                    return;
                }

                var task1 = Task.Factory.StartNew<T1>(Child1.Run);
                var task2 = Task.Factory.StartNew<T2>(Child2.Run);
                var task3 = Task.Factory.StartNew<T3>(Child3.Run);
                var task4 = Task.Factory.StartNew<T4>(Child4.Run);

                Task.WaitAll(task1, task2, task3, task4);

                Result = Function(task1.Result, task2.Result, task3.Result, task4.Result);
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
        protected abstract TResult Function(T1 t1, T2 t2, T3 t3, T4 t4);

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
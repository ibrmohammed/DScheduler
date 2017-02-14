using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    /// <summary>
    /// A set of tasks to be executed simultaneously
    /// </summary>
    /// <typeparam name="TInput">The Input type</typeparam>
    /// <typeparam name="TResult">The return type</typeparam>
    public abstract class ParallelTasks<TInput, TResult> : Parallel<ITaskOut<TInput>>, ITaskOut<TResult>
    {
        private ITaskIn<TResult> _nextTask;

        #region ITaskError Members

        /// <summary>
        /// Occurs when [task error].
        /// </summary>
        public event EventHandler TaskException;

        #endregion

        /// <summary>
        /// The result for this task
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

        void ITask.Run()
        {
            Result = ((ITaskOut<TResult>)this).Run();
        }

        TResult ITaskOut<TResult>.Run()
        {
            Execute().ContinueWith(t =>
                {
                    if (_nextTask != null)
                        _nextTask.Run(t.Result);
                });

            return Result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        protected abstract TResult Aggregate(TInput[] results);

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

        private async Task<TResult> Execute()
        {
            var tasks = Items.Select(t => Task<TInput>.Factory.StartNew(t.Run));

            return await Task.WhenAll(tasks).ContinueWith(results => Aggregate(results.Result));
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ParallelTasks<TResult> : ParallelTasks<TResult, TResult>
    {
    }

    /// <summary>
    /// Represents Parallel Tasks
    /// </summary>
    public class ParallelTasks : BatchTask, IParallel<ITask>
    {
        private readonly Parallel<ITask> _items = new Parallel<ITask>();
        private readonly string _name;

        /// <summary>
        ///
        /// </summary>
        public ParallelTasks()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        public ParallelTasks(string name)
            : this(name, new ITask[] { })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParallelTasks"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tasks">The tasks.</param>
        public ParallelTasks(string name, params ITask[] tasks)
        {
            _name = name;
            _items.Add(tasks);
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ConcurrentBag<ITask> Items
        {
            get
            {
                return _items.Items;
            }
        }

        /// <summary>
        /// Name of the Parallel Task set
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(ITask item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Adds the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Add(params ITask[] items)
        {
            _items.Add(items);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// The core method to execute the task
        /// </summary>
        protected override void Execute()
        {
            Parallel.ForEach(_items.Items, t => t.Run());
        }
    }
}
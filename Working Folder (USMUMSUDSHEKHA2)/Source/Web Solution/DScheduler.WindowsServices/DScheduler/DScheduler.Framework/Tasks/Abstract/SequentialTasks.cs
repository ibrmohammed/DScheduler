using System;
using System.Collections.Generic;

namespace DScheduler.Framework
{
    /// <summary>
    ///Represents a sequence of tasks
    /// </summary>
    public class SequentialTasks : BatchTask, ISequential<ITask>, ITask
    {
        private readonly string _name;

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        public SequentialTasks(string name)
            : this(name, new ITask[] { })
        {
            _name = name;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tasks"></param>
        public SequentialTasks(string name, params ITask[] tasks)
        {
            _name = name;
            Items = new Queue<ITask>();
            foreach (var task in tasks)
            {
                Items.Enqueue(task);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public SequentialTasks()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        ///
        /// </summary>
        public Queue<ITask> Items
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        public void Add(ITask task)
        {
            Items.Enqueue(task);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tasks"></param>
        public void Add(params ITask[] tasks)
        {
            foreach (var task in tasks)
            {
                Items.Enqueue(task);
            }
        }

        void ITask.Run()
        {
            Initialize();
            Execute();
            CleanUp();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return _name;
        }

        /// <summary>
        ///The core method to execute the task
        /// </summary>
        protected override void Execute()
        {
            while (Items.Count > 0)
            {
                var currentTask = Items.Dequeue();
                currentTask.Run();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ParentChildrenTaskOut<TInput, TResult> : TaskOut<TResult>, IParentChildrenTaskOut<TInput, TResult>
    {
        private readonly List<ITaskOut<TInput>> _children;

        ///  <summary>
        ///
        ///  </summary>
        protected ParentChildrenTaskOut()
        {
            _children = new List<ITaskOut<TInput>>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="children"></param>
        protected ParentChildrenTaskOut(params ITaskOut<TInput>[] children)
            : this()
        {
            _children.AddRange(children);
        }

        /// <summary>
        ///
        /// </summary>
        public ITaskOut<TInput>[] Children
        {
            get
            {
                return _children == null ? null : _children.ToArray();
            }
        }

        /// <summary>
        ///
        /// </summary>
        protected async override void Execute()
        {
            //int count = Children.Length;

            //var tasks = new Task<TInput>[count];

            var tasks = _children.Select(child => Task.Factory.StartNew<TInput>(child.Run));

            //for (var iTask = 0; iTask < count; iTask++)
            //{
            //    tasks[iTask] = Task.Factory.StartNew<TInput>(_children[iTask].Run);
            //}

            Console.WriteLine("Running all child tasks...");
            var results = await Task.WhenAll(tasks);
            Console.WriteLine("Aggregating results...");
            Result = Function(results);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected abstract TResult Function(IEnumerable<TInput> inputs);
    }
}
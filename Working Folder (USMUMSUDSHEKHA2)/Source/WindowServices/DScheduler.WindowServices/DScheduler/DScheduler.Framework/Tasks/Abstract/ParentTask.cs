using System;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ParentTask<T> : BatchTask, IParentTask<T>
    {
        /// <summary>
        ///
        /// </summary>
        protected ParentTask()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="child"></param>
        protected ParentTask(ITaskOut<T> child)
        {
            Child = child;
        }

        /// <summary>
        ///
        /// </summary>
        public ITaskOut<T> Child
        {
            get;
            protected set;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected abstract Action<T> Action();

        /// <summary>
        ///
        /// </summary>
        protected override void Execute()
        {
            if (Child == null)
                return;

            Task.Factory.StartNew<T>(Child.Run).ContinueWith(t => Action()(t.Result));
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public abstract class ParentTask<T1, T2> : BatchTask, IParentTask<T1, T2>
    {
        /// <summary>
        ///
        /// </summary>
        protected ParentTask()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="child1"></param>
        /// <param name="child2"></param>
        protected ParentTask(ITaskOut<T1> child1, ITaskOut<T2> child2)
        {
            Child1 = child1;
            Child2 = child2;
        }

        /// <summary>
        ///
        /// </summary>
        public ITaskOut<T1> Child1
        {
            get;
            protected set;
        }

        /// <summary>
        ///
        /// </summary>
        public ITaskOut<T2> Child2
        {
            get;
            protected set;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected abstract Action<T1, T2> Action();

        /// <summary>
        ///
        /// </summary>
        protected override void Execute()
        {
            if (Child1 == null || Child2 == null)
                return;

            var task1 = System.Threading.Tasks.Task.Factory.StartNew<T1>(Child1.Run);
            var task2 = System.Threading.Tasks.Task.Factory.StartNew<T2>(Child2.Run);

            System.Threading.Tasks.Task.WaitAll(task1, task2);

            Action()(task1.Result, task2.Result);
        }
    }

    ///  <summary>
    ///
    ///  </summary>
    ///  <typeparam name="T1"></typeparam>
    ///  <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public abstract class ParentTask<T1, T2, T3> : BatchTask, IParentTask<T1, T2, T3>
    {
        /// <summary>
        ///
        /// </summary>
        protected ParentTask()
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="child1"></param>
        ///  <param name="child2"></param>
        /// <param name="child3"></param>
        protected ParentTask(ITaskOut<T1> child1, ITaskOut<T2> child2, ITaskOut<T3> child3)
        {
            Child1 = child1;
            Child2 = child2;
            Child3 = child3;
        }

        /// <summary>
        ///
        /// </summary>
        public ITaskOut<T1> Child1
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        public ITaskOut<T2> Child2
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        public ITaskOut<T3> Child3
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected abstract Action<T1, T2, T3> Action();

        /// <summary>
        ///
        /// </summary>
        protected override void Execute()
        {
            if (Child1 == null || Child2 == null || Child3 == null)
                return;

            var task1 = Task.Factory.StartNew<T1>(Child1.Run);
            var task2 = Task.Factory.StartNew<T2>(Child2.Run);
            var task3 = Task.Factory.StartNew<T3>(Child3.Run);

            Task.WaitAll(task1, task2, task3);

            Action()(task1.Result, task2.Result, task3.Result);
        }
    }

    ///   <summary>
    ///
    ///   </summary>
    ///   <typeparam name="T1"></typeparam>
    ///   <typeparam name="T2"></typeparam>
    ///  <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    public abstract class ParentTask<T1, T2, T3, T4> : BatchTask, IParentTask<T1, T2, T3, T4>
    {
        /// <summary>
        ///
        /// </summary>
        protected ParentTask()
        {
        }

        ///   <summary>
        ///
        ///   </summary>
        ///   <param name="child1"></param>
        ///   <param name="child2"></param>
        ///  <param name="child3"></param>
        /// <param name="child4"></param>
        protected ParentTask(ITaskOut<T1> child1, ITaskOut<T2> child2, ITaskOut<T3> child3, ITaskOut<T4> child4)
        {
            Child1 = child1;
            Child2 = child2;
            Child3 = child3;
            Child4 = child4;
        }

        /// <summary>
        ///
        /// </summary>
        public ITaskOut<T1> Child1
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        public ITaskOut<T2> Child2
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        public ITaskOut<T3> Child3
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        public ITaskOut<T4> Child4
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected abstract Action<T1, T2, T3, T4> Action();

        /// <summary>
        ///
        /// </summary>
        protected override void Execute()
        {
            if (Child1 == null || Child2 == null || Child3 == null || Child4 == null)
                return;

            var task1 = Task.Factory.StartNew<T1>(Child1.Run);
            var task2 = Task.Factory.StartNew<T2>(Child2.Run);
            var task3 = Task.Factory.StartNew<T3>(Child3.Run);
            var task4 = Task.Factory.StartNew<T4>(Child4.Run);

            Task.WaitAll(task1, task2, task3, task4);

            Action()(task1.Result, task2.Result, task3.Result, task4.Result);
        }
    }
}
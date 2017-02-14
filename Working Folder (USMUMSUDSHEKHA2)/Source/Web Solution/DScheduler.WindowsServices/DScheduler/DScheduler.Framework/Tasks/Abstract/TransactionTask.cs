using System.Transactions;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class TransactionTask : TaskDecorator<ITask>
    {
        private readonly TransactionScopeOption _option;

        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        /// <param name="option"></param>
        public TransactionTask(ITask task, TransactionScopeOption option)
            : base(task)
        {
            _option = option;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        public override void Run()
        {
            using (var scope = new TransactionScope(_option))
            {
                Task.Run();

                scope.Complete();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DScheduler.Framework;
using DScheduler.Framework.Helpers;

namespace DScheduler.BusinessServices
{
    class ScheduledJobRuleTasks : TaskInOut<IEnumerable<ScheduledSessionDetails>, IEnumerable<IEnumerable<ScheduledSessionDetails>>>
    {
        private IEnumerable<ScheduledSessionDetails> argument;
        private readonly int numberOfThreads;

        public ScheduledJobRuleTasks(int numberOfThreads)
        {
            this.numberOfThreads = numberOfThreads;
        }

        protected override IEnumerable<IEnumerable<ScheduledSessionDetails>> Execute(IEnumerable<ScheduledSessionDetails> argument)
        {
            this.argument = argument;
            return this.argument.Partition(numberOfThreads);
        }
        public override IEnumerable<ScheduledSessionDetails> Argument
        {
            get { return argument; }
        }
    }
}

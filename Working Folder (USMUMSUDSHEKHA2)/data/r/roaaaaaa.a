using System.Collections.Generic;
using System.Data;


namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class ProcessDataRowTask : TaskIn<DataRow>
    {
        private readonly Dictionary<string, ConditionInfo<DataRow>> _conditions = new Dictionary<string, ConditionInfo<DataRow>>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        protected override void Execute(DataRow argument)
        {
            foreach (var condition in _conditions)
            {
                LogMessage(condition.Value.Condition(argument)
                               ? condition.Value.SuccessMessage
                               : condition.Value.FailureMessage);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        private void LogMessage(string message)
        {
            //Log message to database
        }
    }
}
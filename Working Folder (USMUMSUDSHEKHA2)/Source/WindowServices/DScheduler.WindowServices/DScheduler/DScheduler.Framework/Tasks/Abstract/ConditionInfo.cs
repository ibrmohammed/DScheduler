using System;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class ConditionInfo<T>
    {
        private readonly bool _reverse;

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="name"></param>
        ///  <param name="condition"></param>
        public ConditionInfo(string name, Predicate<T> condition)
            : this(name, condition, false)
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="name"></param>
        ///  <param name="condition"></param>
        /// <param name="reverse"></param>
        public ConditionInfo(string name, Predicate<T> condition, bool reverse)
            : this(name, condition, reverse, string.Format("Invalid {0}", name))
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="name"></param>
        ///  <param name="condition"></param>
        ///  <param name="failureMessage"></param>
        /// <param name="reverse"></param>
        public ConditionInfo(string name, Predicate<T> condition, bool reverse, string failureMessage)
            : this(name, condition, reverse, failureMessage, string.Format("Valid {0}", name))
        {
        }

        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="name"></param>
        ///  <param name="condition"></param>
        ///  <param name="failureMessage"></param>
        ///  <param name="successMessage"></param>
        /// <param name="reverse"></param>
        public ConditionInfo(string name, Predicate<T> condition, bool reverse, string failureMessage, string successMessage)
        {
            _reverse = reverse;
            Name = name;
            Condition = condition;
            FailureMessage = failureMessage;
            SuccessMessage = successMessage;
        }

        /// <summary>
        ///
        /// </summary>
        public Predicate<T> Condition
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        public string FailureMessage
        {
            get;
            private set;
        }

        /// <summary>
        ///
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public bool Reverse
        {
            get
            {
                return _reverse;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string SuccessMessage
        {
            get;
            private set;
        }
    }
}
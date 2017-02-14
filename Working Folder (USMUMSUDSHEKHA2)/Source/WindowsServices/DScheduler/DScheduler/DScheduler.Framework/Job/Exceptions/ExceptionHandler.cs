using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    public class ExceptionHandler<TException> where TException : Exception
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        /// <param name="handler"></param>
        public void Try(Action action, Action<TException> handler)
        {
            try
            {
                action();
            }
            catch (TException e)
            {
                handler(e);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class ExceptionHandler
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="exceptionType"></param>
        /// <param name="action"></param>
        public ExceptionHandler(Type exceptionType, Action<Exception> action)
        {
            ExceptionType = exceptionType;
            Action = action;
        }

        /// <summary>
        ///
        /// </summary>
        public Action<Exception> Action { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public Type ExceptionType { get; private set; }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TException1"></typeparam>
    /// <typeparam name="TException2"></typeparam>
    public class ExceptionHandler<TException1, TException2>
        where TException1 : Exception
        where TException2 : Exception
    {
        ///  <summary>
        ///
        ///  </summary>
        ///  <param name="action"></param>
        /// <param name="handler1"></param>
        /// <param name="handler2"></param>
        public void Try(Action action, Action<TException1> handler1, Action<TException2> handler2)
        {
            try
            {
                action();
            }
            catch (TException1 e1)
            {
                handler1(e1);
            }
            catch (TException2 e2)
            {
                handler2(e2);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TException1"></typeparam>
    /// <typeparam name="TException2"></typeparam>
    /// <typeparam name="TException3"></typeparam>
    public class ExceptionHandler<TException1, TException2, TException3>
        where TException1 : Exception
        where TException2 : Exception
        where TException3 : Exception
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        /// <param name="handler1"></param>
        /// <param name="handler2"></param>
        /// <param name="handler3"></param>
        public void Try(Action action, Action<TException1> handler1, Action<TException2> handler2, Action<TException3> handler3)
        {
            try
            {
                action();
            }
            catch (TException1 e1)
            {
                handler1(e1);
            }
            catch (TException2 e2)
            {
                handler2(e2);
            }
            catch (TException3 e3)
            {
                handler3(e3);
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class ExceptionHandler<TException1, TException2, TException3, TException4>
        where TException1 : Exception
        where TException2 : Exception
        where TException3 : Exception
        where TException4 : Exception
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="action"></param>
        /// <param name="handler1"></param>
        /// <param name="handler2"></param>
        /// <param name="handler3"></param>
        /// <param name="handler4"></param>
        public void Try(Action action, Action<TException1> handler1, Action<TException2> handler2, Action<TException3> handler3, Action<TException4> handler4)
        {
            try
            {
                action();
            }
            catch (TException1 e1)
            {
                handler1(e1);
            }
            catch (TException2 e2)
            {
                handler2(e2);
            }
            catch (TException3 e3)
            {
                handler3(e3);
            }
            catch (TException4 e4)
            {
                handler4(e4);
            }
        }
    }
}

using System;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InputTask<T> : TaskOut<T>
    {
        private readonly T _input;

        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        public InputTask(T input)
        {
            _input = input;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override T Function()
        {
            Console.WriteLine("Input value is: {0}", _input);
            return _input;
        }
    }
}
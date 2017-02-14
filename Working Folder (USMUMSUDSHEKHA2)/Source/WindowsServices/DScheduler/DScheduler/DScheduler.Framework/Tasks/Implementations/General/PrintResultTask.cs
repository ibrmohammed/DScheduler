using System;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class PrintResultTask : TaskIn<int>
    {
        /// <summary>
        ///Provides a method for any clean up activity needed for the task
        /// </summary>
        protected override void CleanUp()
        {
            Console.WriteLine("Cleaning Up Print Result Task...");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        protected override void Execute(int argument)
        {
            Console.WriteLine("The result of the operation is: {0}", argument);
        }

        /// <summary>
        ///Provides a method for any initialization needed for the task
        /// </summary>
        protected override void Initialize()
        {
            Console.WriteLine("Initializing Print Result Task...");
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class PrintResultTask<T> : TaskIn<T>
    {
        /// <summary>
        ///Provides a method for any clean up activity needed for the task
        /// </summary>
        protected override void CleanUp()
        {
            Console.WriteLine("Cleaning Up Print Result Task...");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="argument"></param>
        protected override void Execute(T argument)
        {
            Console.WriteLine("The result of the operation is: {0}", argument);
        }

        /// <summary>
        ///Provides a method for any initialization needed for the task
        /// </summary>
        protected override void Initialize()
        {
            Console.WriteLine("Initializing Print Result Task...");
        }
    }
}
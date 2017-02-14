using System.Configuration;

namespace DScheduler.Framework
{
    /// <summary>
    ///Read a configuration key and return the value
    /// </summary>
    public abstract class ReadConfigurationSection<TSection, TResult> : TaskOut<TResult> where TSection : ConfigurationSection
    {
        private readonly string _key;

        /// <summary>
        ///Create a task that returns the value from a configuration given the key
        /// </summary>
        /// <param name="key">The key to look for</param>
        protected ReadConfigurationSection(string key)
        {
            _key = key;
        }

        /// <summary>
        ///The core action of the task
        /// </summary>
        protected override void Execute()
        {
            Result = ReadSection((TSection)ConfigurationManager.GetSection(_key));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        protected abstract TResult ReadSection(TSection section);
    }
}
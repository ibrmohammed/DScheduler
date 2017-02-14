using System.Configuration;

namespace DScheduler.Framework
{
    /// <summary>
    ///Read a configuration key and return the value
    /// </summary>
    public class ReadConfigurationKey : TaskOut<string>
    {
        private readonly string _key;

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        public ReadConfigurationKey(string key)
        {
            _key = key;
        }

        /// <summary>
        ///
        /// </summary>
        protected override string Function()
        {
            return ConfigurationManager.AppSettings.Get(_key);
        }
    }
}
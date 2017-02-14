using System.Collections.Generic;
using System.Threading.Tasks;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class ReadConfigKeysTask : TaskOut<IDictionary<string, string>>
    {
        private readonly string[] _keys;

        /// <summary>
        ///
        /// </summary>
        /// <param name="keys"></param>
        public ReadConfigKeysTask(params string[] keys)
        {
            _keys = keys;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<string, string> Function()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            Parallel.ForEach(_keys, key => AddConfigKey(key, result));
            return result;
        }

        private static void AddConfigKey(string key, IDictionary<string, string> result)
        {
            var value = RunTask(new ReadConfigurationKey(key)).Result;
            result.Add(key, value);
        }
    }
}
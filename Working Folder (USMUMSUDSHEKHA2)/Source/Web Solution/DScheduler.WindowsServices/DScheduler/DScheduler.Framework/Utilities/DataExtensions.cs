using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public static class DataExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="convert"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> ToEntities<T>(this DataTable dataTable, Func<DataRow, T> convert)
        {
            return dataTable.Rows.Cast<DataRow>().Select(convert);
        }
    }
}
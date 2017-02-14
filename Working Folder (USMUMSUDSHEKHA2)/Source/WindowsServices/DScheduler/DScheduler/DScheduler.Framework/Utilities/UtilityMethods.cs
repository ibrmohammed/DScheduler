using System.ComponentModel;
using System.Collections.Generic;
using System.Data;

namespace DScheduler.Common
{
    public static class UtilityMethods
    {
        /// <summary>
        /// Convert list of object into datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ConvertToDatatable<T>(this IList<T> data)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            for (var i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            var values = new object[props.Count];
            foreach (T item in data)
            {
                for (var i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        /// <summary>
        /// This function is used for validating and XML file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
        {
            int i = 0;
            var list = new List<T>(size);
            foreach (T item in source)
            {
                list.Add(item);
                if (++i == size)
                {
                    yield return list;
                    list = new List<T>(size);
                    i = 0;
                }
            }

            if (list.Count > 0)
                yield return list;
        }

    }
}
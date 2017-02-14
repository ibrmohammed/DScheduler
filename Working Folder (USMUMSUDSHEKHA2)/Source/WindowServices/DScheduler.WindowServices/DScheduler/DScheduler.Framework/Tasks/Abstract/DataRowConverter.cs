
using System;
using System.Data;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DataRowConverter<T> where T : new()
    {
        private bool _assigned;
        private DataRow _dataRow;
        private T _result;

        /// <summary>
        ///
        /// </summary>
        public T GetEntity(DataRow dataRow)
        {
            _dataRow = dataRow;
            if (!_assigned)
            {
                _result = new T();
                Assign();
                _assigned = true;
            }

            return _result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static TEnum Parse<TEnum>(string value)
        {
            return value.Parse<TEnum>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static TEnum Parse<TEnum>(int value)
        {
            return value.Parse<TEnum>();
        }

        /// <summary>
        ///
        /// </summary>
        protected abstract void Assign();

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="name"></param>
        /// <param name="setValue"></param>
        protected void SetField<TProperty>(string name, Action<T, TProperty> setValue)
        {
            setValue(_result, _dataRow.Field<TProperty>(name));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="name"></param>
        /// <param name="setValue"></param>
        /// <param name="transform"></param>
        protected void SetField<TData, TProperty>(string name, Action<T, TProperty> setValue, Func<TData, TProperty> transform)
        {
            setValue(_result, transform(_dataRow.Field<TData>(name)));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="setProperties"></param>
        protected void SetProperties(Action<T> setProperties)
        {
            setProperties(_result);
        }
    }
}
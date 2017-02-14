
using System;
using System.Data;

using DScheduler.Framework.Helpers;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public abstract class GetDataTable : GetResult<DataTable>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        /// <param name="databaseHelper"></param>
        public GetDataTable(string text, Func<IDatabaseHelper, string, DataTable> getResult, IDatabaseHelper databaseHelper)
            : base(text, getResult, databaseHelper)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="getResult"></param>
        public GetDataTable(string text, Func<IDatabaseHelper, string, DataTable> getResult)
            : base(text, getResult)
        {
        }
    }

    ///// <summary>
    /////This class implements TaskInOut by taking a SQL statement and returning a DataTable from a database.
    /////It uses other helper functions from the Enterprise Data Access Application Block under the hood.
    ///// </summary>
    //public class GetDataTable : TaskInOut<string, DataTable>
    //{
    //    private readonly IDatabaseHelper _databaseHelper;
    //    private readonly Func<IDatabaseHelper, string, DataTable> _getDataTable;
    //    private readonly string _text;

    //    ///   <summary>
    //    ///
    //    ///   </summary>
    //    ///  <param name="text"></param>
    //    /// <param name="getDataTable"></param>
    //    /// <param name="databaseHelper"></param>
    //    public GetDataTable(string text, Func<IDatabaseHelper, string, DataTable> getDataTable, IDatabaseHelper databaseHelper)
    //    {
    //        _databaseHelper = databaseHelper;
    //        _text = text;
    //        _getDataTable = getDataTable;
    //    }

    //    ///  <summary>
    //    /// The constructor takes an SQL statement in the parameter
    //    ///  </summary>
    //    ///  <param name="text">SQL statement</param>
    //    /// <param name="getDataTable"></param>
    //    public GetDataTable(string text, Func<IDatabaseHelper, string, DataTable> getDataTable)
    //        : this(text, getDataTable, new SqlServerHelper())
    //    {
    //    }

    //    /// <summary>
    //    /// The argument to be passed to the task
    //    /// </summary>
    //    public override string Argument
    //    {
    //        get
    //        {
    //            return _text;
    //        }
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="argument"></param>
    //    /// <returns></returns>
    //    protected override DataTable Execute(string argument)
    //    {
    //        var result = _getDataTable(_databaseHelper, argument);
    //        return result;
    //    }
    //}
}
using System;
using System.Collections.Generic;
using System.Data;

namespace DCODatabase.ToolBox.Database
{
    public interface IConnection
    {
        int ExecuteNonQuery(DBCommand command);
        IEnumerable<TResult> ExecuteReader<TResult>(DBCommand command, Func<IDataRecord, TResult> selector);
        object ExecuteScalar(DBCommand command);
        DataTable GetDataTable(DBCommand command);   
    }
}

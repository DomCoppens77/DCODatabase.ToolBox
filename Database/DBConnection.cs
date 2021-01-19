using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace DCODatabase.ToolBox.Database
{
    public class DBConnection : IConnection
    {
        private string _connectionString;
        private DbProviderFactory _factory;

        public DBConnection(IConnectionInfo connectionString, DbProviderFactory factory)
        {
            _connectionString = connectionString.ConnectionString;
            _factory = factory;
            using (DbConnection Connection = CreateConnection())
            {
                Connection.Open();
            }
        }

        private DbConnection CreateConnection()
        {
            DbConnection dbconnection = _factory.CreateConnection();
            dbconnection.ConnectionString = _connectionString;
            return dbconnection;
        }

        public object ExecuteScalar(DBCommand command)
        {
            using (DbConnection Connection = CreateConnection())
            {

                using (DbCommand Command = CreateCommand(command, Connection))
                {
                    Connection.Open();
                    Object obj_tmp = Command.ExecuteScalar();
                    return (obj_tmp is DBNull) ? null : obj_tmp;
                }
            }
        }

        public int ExecuteNonQuery(DBCommand command)
        {
            using (DbConnection Connection = CreateConnection())
            {
                using (DbCommand Command = CreateCommand(command, Connection))
                {
                    Connection.Open();
                    int Rows = Command.ExecuteNonQuery();
                    return Rows;
                }
            }
        }

        public DataTable GetDataTable(DBCommand command)
        {
            using (DbConnection Connection = CreateConnection())
            {
                using (DbCommand Command = CreateCommand(command, Connection))
                {
                    using (DbDataAdapter da = _factory.CreateDataAdapter())
                    {
                        DataTable dt = new DataTable();
                        da.SelectCommand = Command;
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(DBCommand command, Func<IDataRecord, TResult> selector)
        {
            using (DbConnection Connection = CreateConnection())
            {
                using (DbCommand Command = CreateCommand(command, Connection))
                {
                    Connection.Open();
                    using (DbDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            yield return selector(Reader);
                        }
                    }
                }
            }
        }

        private DbCommand CreateCommand(DBCommand command, DbConnection connection)
        {

            DbCommand Command = connection.CreateCommand();
            Command.CommandText = command.Query;
            if (command.IsStoredProcedure) Command.CommandType = CommandType.StoredProcedure;
            foreach (KeyValuePair<String, Object> kvp in command.Parameters)
            {
                DbParameter PID = _factory.CreateParameter();
                {
                    PID.ParameterName = kvp.Key;
                    PID.Value = kvp.Value;
                }
                Command.Parameters.Add(PID);
            }
            return Command;
        }

    }
}

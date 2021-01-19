using System;
using System.Collections.Generic;
using System.Text;

namespace DCODatabase.ToolBox.Database
{
    public class DBCommand
    {
        internal string Query { get; private set; }
        internal bool IsStoredProcedure { get; private set; }
        internal IDictionary<string, object> Parameters { get; private set; }

        /// <summary>
        /// Instancie un object de type Command
        /// </summary>
        /// <param name="PQuery">SQL Command to Generate</param>
        /// <param name="PIsStoredProcedure">Define if A store Procedure (DEFAULT=FALSE)</param>
        public DBCommand(String query, bool isStoredProcedure = false)
        {
            Query = query;
            IsStoredProcedure = isStoredProcedure;
            Parameters = new Dictionary<string, object>();
        }
        /// <summary>
        /// Add a parameter to the command
        /// </summary>
        /// <param name="ParameterName">Parameter NAme</param>
        /// <param name="Value">Parameter Value</param>
        public void AddParameter(String parameterName, Object value)
        {
            Parameters.Add(parameterName, value ?? DBNull.Value);
        }
    }
}

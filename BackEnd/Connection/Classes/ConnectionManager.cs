using System;
using System.Data;
using System.Data.SqlClient;

namespace Connection.Classes
{
    public class ConnectionManager
    {
        #region Definition of Properties
        private Connection connection { get; set; }
        #endregion
        #region Definition of Constructors
        public ConnectionManager(string connection)
        {
            this.connection = new Connection(connection);
        }
        #endregion
        #region Definition of Public Methods
        public long RowsAffedted()
        {
            return connection.rowsAffected;
        }

        public string GetConnectionString()
        {
            return connection.connectionString;
        }

        public MySql.Data.MySqlClient.MySqlConnection GetConnection()
        {
            try
            {
                return (MySql.Data.MySqlClient.MySqlConnection)this.connection.GetSqlConnection();
            }
            catch
            {
                return null;
            }
        }

        public void Open()
        {
            if (!connection.Open())
            {
                throw new Exception(connection.error);
            }
        }

        public void Close()
        {
            if (!connection.Close())
            {
                throw new Exception(connection.error);
            }
        }

        public MySql.Data.MySqlClient.MySqlTransaction GetTransaction()
        {
            try
            {
                return (MySql.Data.MySqlClient.MySqlTransaction)this.connection.GetTransaction();
            }
            catch
            {
                return null;
            }
        }

        public void BeginTransaction()
        {
            if (!connection.BeginTransaction())
            {
                throw new Exception(connection.error);
            }
        }

        public void CommitTransaction()
        {
            if (!connection.CommitTransaction())
            {
                throw new Exception(connection.error);
            }
        }

        public void RollBackTransaction()
        {
            if (!connection.RollBackTransaction())
            {
                throw new Exception(connection.error);
            }
        }

        public void Execute(string query)
        {
            if (!connection.Execute(query))
            {
                throw new Exception(connection.error);
            }
        }

        public DataTable Select(string query)
        {
            DataTable table = connection.Select(query);

            if (connection.error != "")
            {
                throw new Exception(connection.error);
            }

            return table;
        }

        public DataSet SelectData(string query)
        {
            DataSet data = connection.SelectData(query);

            if (connection.error != "")
            {
                throw new Exception(connection.error);
            }

            return data;
        }        
        #endregion
    }
}
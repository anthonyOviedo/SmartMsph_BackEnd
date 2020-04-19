using System;
using System.Data;
using System.Data.SqlClient;

namespace Connection.Classes
{
    public class Connection
    {
        #region Definition of Properties
        internal string connectionString { get; set; }
        internal string error { get; set; }
        internal MySql.Data.MySqlClient.MySqlConnection connection { get; set; }

        internal MySql.Data.MySqlClient.MySqlCommand command { get; set; }
        internal MySql.Data.MySqlClient.MySqlDataAdapter adapter { get; set; }
        internal MySql.Data.MySqlClient.MySqlTransaction transaction { get; set; }
        internal long rowsAffected { get; set; }
        #endregion
        #region Definition of Constructors
        public Connection(string connection)
        {
            try
            {
                error = "";
                connectionString = connection;
                rowsAffected = 0;
                this.connection = new MySql.Data.MySqlClient.MySqlConnection(connection);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
        }
        #endregion
        #region Definition of Private Methods
        private MySql.Data.MySqlClient.MySqlCommand CreateCommand(string query)
        {
            return new MySql.Data.MySqlClient.MySqlCommand(query, connection);
        }

        private MySql.Data.MySqlClient.MySqlDataAdapter CreateAdapter()
        {
            return new MySql.Data.MySqlClient.MySqlDataAdapter(command);
        }
        #endregion
        #region Definition of Public Methods
        public IDbConnection GetSqlConnection()
        {
            try
            {
                return connection;
            }
            catch
            {
                return null;
            }
        }

        public bool Open()
        {
            try
            {
                error = "";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool Close()
        {
            try
            {
                error = "";
                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public IDbTransaction GetTransaction()
        {
            try
            {
                return transaction;
            }
            catch
            {
                return null;
            }
        }

        public bool BeginTransaction()
        {
            try
            {
                error = "";
                transaction = connection.BeginTransaction();

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool CommitTransaction()
        {
            try
            {
                error = "";
                transaction.Commit();
                transaction.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool RollBackTransaction()
        {
            try
            {
                error = "";
                transaction.Rollback();
                transaction.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public DataTable Select(string query)
        {
            DataSet data;
            DataTable table = null;

            try
            {
                error = "";
                rowsAffected = 0;

                if (command == null)
                {
                    command = CreateCommand(query);
                }
                else
                {
                    command.CommandText = query;
                }

                command.CommandTimeout = 0;

                if (transaction != null)
                {
                    command.Transaction = transaction;
                }

                adapter = CreateAdapter();
                data = new DataSet();
                adapter.Fill(data);
                table = data.Tables[0].Copy();

                if (table != null)
                {
                    rowsAffected = table.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return table;
        }

        public DataSet SelectData(string query)
        {
            DataSet data = new DataSet();

            try
            {
                error = "";
                rowsAffected = 0;

                if (command == null)
                {
                    command = CreateCommand(query);
                }
                else
                {
                    command.CommandText = query;
                }

                command.CommandTimeout = 0;

                if (transaction != null)
                {
                    command.Transaction = transaction;
                }

                adapter = CreateAdapter();
                data = new DataSet();
                adapter.Fill(data);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return data;
        }

        public bool Execute(string query)
        {
            try
            {
                error = "";
                rowsAffected = 0;

                if (command == null)
                {
                    command = CreateCommand(query);
                }
                else
                {
                    command.CommandText = query;
                }

                command.CommandTimeout = 0;

                if (transaction != null)
                {
                    command.Transaction = transaction;
                }

                rowsAffected = command.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        #endregion
    }
}
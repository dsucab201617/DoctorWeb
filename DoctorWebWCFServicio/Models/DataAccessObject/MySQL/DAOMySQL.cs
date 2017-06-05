using DoctorWebWCFServicio.Models.DataAccessObject.Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebWCFServicio.Models.DataAccessObject.MySQL
{
    public class DAOMySQL : IDAO
    {
        private MySqlConnection Connection { get; set; }

        public DAOMySQL()
        {
            Connection = null;
        }

        public void CloseConnection()
        {
            if (Connection != null)
                Connection.Close();
        }

        private bool ConnectionsIsReady()
        {
            return Connection != null && Connection.State == System.Data.ConnectionState.Open;
        }

        private void RenderRows(MySqlCommand command, ForEachRow doThis)
        {
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (doThis == null)
                        break;
                    doThis(reader);
                }
            }
        }

        public void ExecuteNoQuery(string query, object parameters = null)
        {
            if (ConnectionsIsReady())
            {
                var command = new MySqlCommand();
                var transaction = Connection.BeginTransaction();

                command.CommandText = query;
                command.Connection = Connection;
                command.Transaction = transaction;

                command.SetParametersByObject(parameters);

                command.ExecuteNonQuery();

                transaction.Commit();
                command.Dispose();
            }
            else
                throw new DAOException("The connection is not open.");
        }

        public void ExecuteQuery(string query, object parameters = null, ForEachRow doThis = null)
        {
            if (ConnectionsIsReady())
            {
                var command = new MySqlCommand();
                var transaction = Connection.BeginTransaction();

                command.CommandText = query;
                command.Connection = Connection;
                command.Transaction = transaction;

                command.SetParametersByObject(parameters);

                RenderRows(command, doThis);

                transaction.Commit();
                command.Dispose();
            }
            else
                throw new DAOException("The connection is not open.");
        }

        public void ExecuteStoreProcedure(string name, object parameters)
        {
            if (ConnectionsIsReady())
            {
                var command = new MySqlCommand();
                var transaction = Connection.BeginTransaction();

                command.CommandText = name;
                command.CommandTimeout = 0;
                command.Connection = Connection;
                command.Transaction = transaction;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.SetParametersByObject(parameters);

                command.ExecuteNonQuery();

                transaction.Commit();
                command.Dispose();
            }
            else
                throw new DAOException("The connection is not open.");
        }

        public void ExecuteStoreProcedureWithResult(string name, object parameters = null, ForEachRow doThis = null)
        {
            if (ConnectionsIsReady())
            {
                var command = new MySqlCommand();
                var transaction = Connection.BeginTransaction();

                command.CommandText = name;
                command.Connection = Connection;
                command.Transaction = transaction;

                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.SetParametersByObject(parameters);

                RenderRows(command, doThis);

                transaction.Commit();
                command.Dispose();
            }
            else
                throw new DAOException("The connection is not open.");
        }

        public void OpenConnection()
        {
            if (Connection == null)
            {
                Connection = new MySqlConnection(DAOMySQLUtils.GetStringConnection());
            }
            Connection.Open();
        }
    }
}
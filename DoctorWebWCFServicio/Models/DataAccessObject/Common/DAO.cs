using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoctorWebWCFServicio.Models.DataAccessObject.Common
{
    public delegate void ForEachRow(DbDataReader reader);

    public class DAO
    {
        private IDAO handler;

        public DAO()
        {
            handler = FabricDAO.CreateDAO();
        }

        #region Implementacion de Primitivas DAO.
        protected void CloseConnection()
        {
            handler.CloseConnection();
        }

        protected void ExecuteNoQuery(string query)
        {
            handler.ExecuteNoQuery(query);
        }

        protected void ExecuteQuery(string query, ForEachRow doThis = null)
        {
            handler.ExecuteQuery(query, doThis);
        }

        protected void ExecuteStoreProcedure(string name, object parameters)
        {
            handler.ExecuteStoreProcedure(name, parameters);
        }

        protected void ExecuteStoreProcedureWithResult(string name, object parameters, ForEachRow doThis = null)
        {
            handler.ExecuteStoreProcedureWithResult(name, parameters, doThis);
        }

        protected void OpenConnection()
        {
            handler.OpenConnection();
        }
        #endregion
    }
}
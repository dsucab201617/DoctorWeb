using DoctorWebWCFServicio.Models.DataAccessObject.MySQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DoctorWebWCFServicio.Models.DataAccessObject.Common
{
    public enum TypeDAO
    {
        MSS, MySql
    }

    public class FabricDAO
    {
        #region Global
        public static IDAO CreateDAO(TypeDAO type = TypeDAO.MySql)
        {
            switch (type)
            {
                case TypeDAO.MySql: return FabricDAO.CreateMySql();
            }
            throw new DAOException($"The Instance DAO {type}, not founded..");
        }


        private static IDAO CreateMySql()
        {
            return new DAOMySQL();
        }
        #endregion
    }
}
using DoctorWebWCFServicio.Models.DataAccessObject.Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DoctorWebWCFServicio.Models.DataAccessObject.MySQL
{
    public static class DAOMySQLUtils
    {
        
        public static void SetParametersByObject(this MySqlCommand command, object parameters = null)
        {
            if (parameters != null)
            {
                MySqlDbType dbType = MySqlDbType.Int64;
                foreach (var field in parameters.GetType().GetProperties())
                {
                    if (field.PropertyType == typeof(byte) || field.PropertyType == typeof(Nullable<byte>))
                    {
                        dbType = MySqlDbType.Int16;
                    }
                    else if (field.PropertyType == typeof(short) || field.PropertyType == typeof(Nullable<short>))
                    {
                        dbType = MySqlDbType.Int24;
                    }
                    else if (field.PropertyType == typeof(int) || field.PropertyType == typeof(Nullable<int>))
                    {
                        dbType = MySqlDbType.Int32;
                    }
                    else if (field.PropertyType == typeof(long) || field.PropertyType == typeof(Nullable<long>))
                    {
                        dbType = MySqlDbType.Int64;
                    }
                    else if (field.PropertyType == typeof(float) || field.PropertyType == typeof(Nullable<float>))
                    {
                        dbType = MySqlDbType.Float;
                    }
                    else if (field.PropertyType == typeof(string))
                    {
                        dbType = MySqlDbType.VarChar;
                    }
                    else if (field.PropertyType == typeof(DateTime) || field.PropertyType == typeof(Nullable<DateTime>))
                    {
                        dbType = MySqlDbType.DateTime;
                    }
                    else if (field.PropertyType.IsEnum || (field.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && field.PropertyType.GetGenericArguments()[0].IsEnum))
                    {
                        dbType = MySqlDbType.Int32;
                    }
                    else
                    {
                        throw new DAOException(String.Format("It was not possible to determine an equivalent data type ({0}).", field.PropertyType.Name));
                    }

                    var value = field.GetValue(parameters);
                    command.Parameters.Add(new MySqlParameter(field.Name, dbType)).Value = value;
                }
            }
        }

        internal static string GetStringConnection()
        {
            var host = ConfigurationManager.AppSettings["MySqlHost"].ToString();
            var port = ConfigurationManager.AppSettings["MySqlPort"].ToString();
            var db = ConfigurationManager.AppSettings["MySqlDb"].ToString();
            var user = ConfigurationManager.AppSettings["MySqlUser"].ToString();
            var pass = ConfigurationManager.AppSettings["MySqlPass"].ToString();            

            var stringConnection =$"Server={host};Port={port};Database={db};Uid={user};Pwd = {pass}";

            return stringConnection;
        }
        
    }
}
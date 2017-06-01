using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoctorWebWCFServicio.Models.DataAccessObject.Common
{
    public static class DAOUtils
    {
        public static void SetParametersByObject(this SqlCommand cmd, object parameters = null)
        {
            if (parameters != null)
            {
                SqlDbType dbType = SqlDbType.BigInt;
                foreach (var field in parameters.GetType().GetProperties())
                {
                    if (field.PropertyType == typeof(byte) || field.PropertyType == typeof(Nullable<byte>))
                    {
                        dbType = SqlDbType.TinyInt;
                    }
                    else if (field.PropertyType == typeof(short) || field.PropertyType == typeof(Nullable<short>))
                    {
                        dbType = SqlDbType.SmallInt;
                    }
                    else if (field.PropertyType == typeof(int) || field.PropertyType == typeof(Nullable<int>))
                    {
                        dbType = SqlDbType.Int;
                    }
                    else if (field.PropertyType == typeof(long) || field.PropertyType == typeof(Nullable<long>))
                    {
                        dbType = SqlDbType.BigInt;
                    }
                    else if (field.PropertyType == typeof(float) || field.PropertyType == typeof(Nullable<float>))
                    {
                        dbType = SqlDbType.Real;
                    }
                    else if (field.PropertyType == typeof(string))
                    {
                        dbType = SqlDbType.VarChar;
                    }
                    else if (field.PropertyType == typeof(DateTime) || field.PropertyType == typeof(Nullable<DateTime>))
                    {
                        dbType = SqlDbType.DateTime2;
                    }
                    else if (field.PropertyType.IsEnum || (field.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && field.PropertyType.GetGenericArguments()[0].IsEnum))
                    {
                        dbType = SqlDbType.Int;
                    }
                    else
                    {
                        throw new DAOException(String.Format("It was not possible to determine an equivalent data type ({0}).", field.PropertyType.Name));
                    }

                    try {
                        var value = field.GetValue(parameters);
                        cmd.Parameters.Add(new SqlParameter(field.Name, dbType)).Value = value;
                    } catch (Exception exp) {
                        throw exp;
                    }
                }
            }
        }

    }
}
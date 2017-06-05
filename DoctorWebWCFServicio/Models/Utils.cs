using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DoctorWebWCFServicio.Models
{
    public class Utils
    {
        public static string GetConfig(string key)
        {
            var path = ConfigurationManager.AppSettings[key];
            if (String.IsNullOrEmpty(path))
                throw new KeyNotFoundException($"No se encontro la clave '{key}' en el archivo de configuracion.");
            return path;
        }
    }
}
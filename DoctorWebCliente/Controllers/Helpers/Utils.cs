using DoctorWebCliente.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebCliente.Controllers.Helpers
{
    public static class Utils
    {

        #region Grupo09
        public static void FillDownDropList(this NotificacionesController controller, Notificacion model)
        {
            controller.ViewData["TiposDeDestinatario"] = new SelectList(
                      new List<Object>{
                           new { value = EnumEventoTipoDestinatarios.Fijo , text = EnumEventoTipoDestinatarios.Fijo.ToString()  },
                           new { value = EnumEventoTipoDestinatarios.Procedimiento , text = EnumEventoTipoDestinatarios.Procedimiento.ToString()  }
                        },
                      "value",
                      "text",
                      model.TipoDestinatarios
                );

            controller.ViewData["TiposDeContenido"] = new SelectList(
                  new List<Object>{
                           new { value = EnumEventoTipoContenido.Fijo , text = EnumEventoTipoContenido.Fijo.ToString()  },
                           new { value = EnumEventoTipoContenido.Procedimiento , text = EnumEventoTipoContenido.Procedimiento.ToString()  }
                    },
                  "value",
                  "text",
                  model.TipoContenido
           );
        }

        internal static Uri GetUrlBase(string servicio)
        {
            var host = GetConfig("WebServiceUrl");
            var builder = new UriBuilder(host);
            builder.Path = $"Services/{servicio}.svc/";
            return builder.Uri;
        }

        internal static string GetConfig(string key)
        {
            var path = ConfigurationManager.AppSettings[key];
            if (String.IsNullOrEmpty(path))
                throw new KeyNotFoundException($"No se encontro un valor para '{key}', en el archivo de configuracion.");
            return path;
        }

        internal static void FillModel(Notificacion model, FormCollection collection)
        {
            model.Nombre = collection["Nombre"];
            model.Descripcion = collection["Descripcion"];
            model.Contenido = collection["Contenido"];
            model.Asunto = collection["Asunto"];
            model.Destinatarios = collection["Destinatarios"];
            model.TipoContenido = (EnumEventoTipoContenido)Enum.Parse(typeof(EnumEventoTipoContenido), collection["TipoContenido"]);
            model.TipoDestinatarios = (EnumEventoTipoDestinatarios)Enum.Parse(typeof(EnumEventoTipoDestinatarios), collection["TipoDestinatarios"]);
        }
        #endregion
    }
}
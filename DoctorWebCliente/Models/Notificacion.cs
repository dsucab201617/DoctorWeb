using DoctorWebCliente.Controllers.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebCliente.Models
{
    public class Notificacion
    {
        #region Estatico : Servicios Web
        public static List<Notificacion> ObtenerTodos(string nombre = null, int indicePagina = 0, int numeroFilas = 30)
        {
            var lista = new List<Notificacion>();
            var client = new RestClient(baseUrl: Utils.GetUrlBase("Notificaciones"));
            try
            {
                var action = "ObtenerTodos";
                var request = new RestRequest(resource: action, method: Method.GET);

                if (!String.IsNullOrEmpty(nombre))
                    request.AddQueryParameter("nombre", nombre);
                request.AddQueryParameter("indice", indicePagina.ToString());
                request.AddQueryParameter("filas", numeroFilas.ToString());

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var conjunto = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var elementos = (JArray)conjunto["ObtenerTodosResult"];
                    foreach (var elemento in elementos) {
                        lista.Add(elemento.ToObject<Notificacion>());
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lista;
        }

        internal static Notificacion Obtener(long codigo)
        {
            var client = new RestClient(baseUrl: Utils.GetUrlBase("Notificaciones"));
            try
            {
                var action = $"Obtener/{codigo}";
                var request = new RestRequest(resource: action, method: Method.GET);
                
                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var conjunto = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var notificacion = (JObject)conjunto["ObtenerResult"];
                    if (notificacion != null)
                    {
                        return notificacion.ToObject<Notificacion>();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        internal static void Guardar(Notificacion datos)
        {
            var lista = new List<Notificacion>();
            var client = new RestClient(baseUrl: Utils.GetUrlBase("Notificaciones"));
            try
            {
                var action = "Guardar";
                var request = new RestRequest(resource: action, method: Method.POST);
                var body = new { notificacion = datos };
                var json = JsonConvert.SerializeObject(body);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(body);
                var response = client.Execute(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static void Borrar(long codigo)
        {
            var client = new RestClient(baseUrl: Utils.GetUrlBase("Notificaciones"));
            try
            {
                var action = $"Borrar/{codigo}";
                var request = new RestRequest(resource: action, method: Method.DELETE);

                var response = client.Execute(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Instancia
        public long Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Asunto { get; set; }
        public string Destinatarios { get; set; }
        public EnumEventoTipoDestinatarios TipoDestinatarios { get; set; }
        [AllowHtml]
        public string Contenido { get; set; }
        public EnumEventoTipoContenido TipoContenido { get; set; }

        public Notificacion()
        {
            this.Codigo = 0;
            this.Nombre = String.Empty;
            this.Descripcion = String.Empty;
            this.Asunto = String.Empty;
            this.Destinatarios = String.Empty;
            this.TipoDestinatarios = EnumEventoTipoDestinatarios.Fijo;
            this.Contenido = String.Empty;
            this.TipoContenido = EnumEventoTipoContenido.Fijo;
        }
        #endregion
    }
}
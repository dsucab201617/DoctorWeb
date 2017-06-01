using DoctorWebWCFServicio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DoctorWebWCFServicio.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "INotificaciones" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface INotificaciones
    {
        [OperationContract]
        [WebGet(UriTemplate = "/obtenerTodos?nombre={nombre}&indice={indicePagina}&filas={numeroFilas}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Notificacion> ObtenerTodos(string nombre, int indicePagina = 0, int numeroFilas = 30);

        [OperationContract]
        [WebGet(UriTemplate = "/obtener/{codigo}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        Notificacion Obtener(string codigo);

        [OperationContract]
        [WebInvoke(UriTemplate = "/guardar", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool Guardar(Notificacion notificacion);

        [OperationContract]
        [WebInvoke(UriTemplate = "/borrar/{codigo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "DELETE")]
        bool Borrar(string codigo);

        [OperationContract]
        [WebGet(UriTemplate = "/Prueba/{codigo}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void Prueba(string codigo);
    }
}

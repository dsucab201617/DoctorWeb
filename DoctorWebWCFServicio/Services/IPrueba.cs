using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DoctorWebWCFServicio.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IPrueba" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IPrueba
    {
        [OperationContract]
        [WebGet(
            UriTemplate = "/Ejecutar", 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DoWork();
    }
}

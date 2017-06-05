using EASendMail;
using DoctorWebWCFServicio.Models.DataAccessObject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using static DoctorWebWCFServicio.Helpers.Dominios;

namespace DoctorWebWCFServicio.Models
{
    public class Notificacion
    {
        #region Estatico
        internal static List<Notificacion> ObtenerTodos(Nullable<long> codigo = null, string nombre = null, int indicePagina = 0, int numeroFilas = 30)
        {
            var lista = new List<Notificacion>();
            var dao = FabricDAO.CreateDAO();
            try
            {
                dao.OpenConnection();
                dao.ExecuteQuery(
                    query: "SELECT * FROM notificaciones WHERE (@nombre IS NULL OR nombre LIKE CONCAT('%', @nombre ,'%')) AND (@codigo IS NULL OR codigo = @codigo) LIMIT @filas OFFSET @indice",
                    parameters: new
                    {
                        codigo = codigo,
                        nombre = nombre,
                        indice = indicePagina * numeroFilas,
                        filas = numeroFilas
                    },
                    doThis: (reader) =>
                    {
                        lista.Add(new Notificacion
                        {
                            Codigo = reader.GetInt64(0),
                            Nombre = reader.GetString(1),
                            Descripcion = reader.GetString(2),
                            Asunto = reader.GetString(3),
                            Destinatarios = reader.GetString(4),
                            Contenido = reader.GetString(5),
                            TipoDestinatarios = (EnumEventoTipoDestinatarios)reader.GetInt32(6),
                            TipoContenido = (EnumEventoTipoContenido)reader.GetInt32(7)
                        });
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dao.CloseConnection();
            }

            return lista;
        }

        internal static void Borrar(long codigo)
        {
            var dao = FabricDAO.CreateDAO();
            try
            {
                var query = String.Empty;
                if (codigo > 0)
                {
                    query = "DELETE FROM notificaciones WHERE @codigo = codigo";

                    dao.OpenConnection();
                    dao.ExecuteNoQuery(
                        query: query,
                        parameters: new
                        {
                            codigo = codigo
                        });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dao.CloseConnection();
            }
        }

        internal static void Guardar(Notificacion notificacion)
        {
            var dao = FabricDAO.CreateDAO();
            try
            {
                var query = String.Empty;
                if (notificacion.Codigo == 0)
                    query = "INSERT INTO notificaciones (nombre, descripcion, asunto, destinatarios, contenido, tipodestinatarios, tipocontenido) VALUES (@nombre, @descripcion, @asunto, @destinatarios, @contenido, @tipodestinatarios, @tipocontenido)";
                else
                    query = "UPDATE notificaciones SET   nombre = coalesce(@nombre, nombre), descripcion = coalesce(@descripcion, descripcion), asunto = coalesce(@asunto, asunto), destinatarios = coalesce(@destinatarios, destinatarios), contenido = coalesce(@contenido, contenido), tipodestinatarios = coalesce(@tipodestinatarios, tipodestinatarios), tipocontenido = coalesce(@tipocontenido, tipocontenido) WHERE codigo = @codigo";

                dao.OpenConnection();
                dao.ExecuteNoQuery(
                    query: query,
                    parameters: new
                    {
                        codigo = notificacion.Codigo,
                        nombre = notificacion.Nombre,
                        descripcion = notificacion.Descripcion,
                        asunto = notificacion.Asunto,
                        destinatarios = notificacion.Destinatarios,
                        tipodestinatarios = notificacion.TipoDestinatarios,
                        contenido = notificacion.Contenido,
                        tipocontenido = notificacion.TipoContenido
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dao.CloseConnection();
            }
        }

        internal static void Enviar(long codigo) {
            SendMail("rasc.19@gmail.com", "DoctorWeb", "Hola que hace?");
        }

        private static void SendMail(string email, string subject, string content)
        {
            var host = Utils.GetConfig("SMTPServerHost");
            var port = Utils.GetConfig("SMTPServerPost");
            var display = Utils.GetConfig("SMTPDisplayName");
            var user = Utils.GetConfig("SMTPUserId");
            var pass = Utils.GetConfig("SMTPUserPassword");

            if (!String.IsNullOrEmpty(host) &&
                !String.IsNullOrEmpty(port) &&
                !String.IsNullOrEmpty(display) &&
                !String.IsNullOrEmpty(user) &&
                !String.IsNullOrEmpty(pass))
            {
                try
                {

                    var oMail = new SmtpMail("TryIt");
                    oMail.From = new MailAddress(display, user);
                    oMail.Priority = MailPriority.High;
                    oMail.To.Add(new MailAddress(email));
                    oMail.Subject = subject;
                    oMail.HtmlBody = content;

                    SmtpServer oServer = new SmtpServer(host);
                    oServer.User = user;
                    oServer.Password = pass;
                    oServer.Port = 465;
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                    var oSmtp = new SmtpClient();
                    oSmtp.BatchSendMail(
                        maxThreads: 1,
                        servers: new SmtpServer[] { oServer },
                        mails: new SmtpMail[] { oMail });
                }
                catch (Exception)
                {

                }
            }
        }
        #endregion

        #region Instancia
        [DataMember]
        public Nullable<long> Codigo { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Destinatarios { get; set; }
        [DataMember]
        public string Asunto { get; set; }
        [DataMember]
        public EnumEventoTipoDestinatarios TipoDestinatarios { get; set; }
        [DataMember]
        public string Contenido { get; set; }
        [DataMember]
        public EnumEventoTipoContenido TipoContenido { get; set; }

        public Notificacion()
        {
            this.Codigo = 0;
            this.Nombre = null;
            this.Descripcion = null;
            this.Destinatarios = null;
            this.TipoDestinatarios = EnumEventoTipoDestinatarios.Fijo;
            this.Contenido = null;
            this.TipoContenido = EnumEventoTipoContenido.Fijo;
        }
        #endregion
    }
}
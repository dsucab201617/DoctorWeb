using DoctorWebCliente.Controllers.Helpers;
using DoctorWebCliente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebCliente.Controllers
{
    public class NotificacionesController : Controller
    {
        #region Mockup Lista de elementos
        private const string TAG = "MockUpEventos";
        private List<Notificacion> eventos
        {
            get
            {
                if (Session[TAG] == null)
                {
                    eventos = new List<Notificacion>();
                    eventos.AddRange(new[] {
                        new Notificacion
                        {
                            Codigo = -1,
                            Nombre = "Evento ECHO Fijo/Fijo",
                            Descripcion = "Es un evento de prueba que envia un mensaje a un unico destinatario, con un contenido fijo.",
                            Destinatarios = "rasc.19@gmail.com",
                            TipoDestinatarios = EnumEventoTipoDestinatarios.Fijo,
                            Contenido = "<h1>Evento ECHO Fijo</h1><p> Hola!</p>",
                            TipoContenido = EnumEventoTipoContenido.Fijo
                        },
                        new Notificacion
                        {
                            Codigo = -2,
                            Nombre = "Evento ECHO Fijo/Proc",
                            Descripcion = "Es un evento de prueba que envia un mensaje a un unico fijo, con un contenido variable.",
                            Destinatarios = "rasc.19@gmail.com",
                            TipoDestinatarios = EnumEventoTipoDestinatarios.Fijo,
                            Contenido = "SPNotificacionContenidoEcho",
                            TipoContenido = EnumEventoTipoContenido.Procedimiento
                        },
                        new Notificacion
                        {
                            Codigo = -3,
                            Nombre = "Evento ECHO Proc/Proc",
                            Descripcion = "Es un evento de prueba que envia un mensaje a destinatarios variables, con un contenido variable.",
                            Destinatarios = "SPEventoDestinatarioEcho",
                            TipoDestinatarios = EnumEventoTipoDestinatarios.Procedimiento,
                            Contenido = "SPNotificacionContenidoEcho",
                            TipoContenido = EnumEventoTipoContenido.Procedimiento
                        }
                    });
                }
                return (List<Notificacion>)Session[TAG];
            }

            set
            {
                if (value is List<Notificacion>)
                    Session[TAG] = value;
                if (Session[TAG] == null)
                    Session[TAG] = new List<Notificacion>();
            }
        }
        #endregion

        // GET: Notificaciones
        public ActionResult Index(string nombre = null, int indice = 0, int filas = 5)
        {
            var notificaciones = Notificacion.ObtenerTodos(nombre, indice, filas);
            ViewData["nombre"] = nombre;            
            ViewData["filas"] = filas;
            ViewData["permitirSiguiente"] = notificaciones.Count == filas;
            ViewData["siguienteIndice"] = indice+1;
            ViewData["permitirAnterior"] = indice > 0;
            ViewData["anteriorIndice"] = indice-1;
            return View(model: notificaciones);
        }

        public ActionResult Detail(long codigo)
        {
            Notificacion model = null;
            if (codigo != 0)
            {
                model = Notificacion.Obtener(codigo);
            }
            else
            {
                model = new Notificacion();
            }
            if (model != null)
            {
                Utils.FillDownDropList(this, model);
                return View(model: model);
            }
            else
                return RedirectToAction("Index");
        }
        
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            var model = new Notificacion();
            try
            {
                model.Codigo = 0;
                Utils.FillModel(model, collection);
                Notificacion.Guardar(model);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("Error", exception.Message);

                Utils.FillDownDropList(this, model);
                return View("Detail", model);
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(long codigo, FormCollection collection)
        {
            var model = new Notificacion()
            {
                Codigo = codigo
            };
            try
            {
                Utils.FillModel(model, collection);
                Notificacion.Guardar(model);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                if (model == null)
                    return RedirectToAction("Index");

                ModelState.AddModelError("Error", exception.Message);

                Utils.FillDownDropList(this, model);
                return View("Detail", model);
            }
        }

        public ActionResult Delete(long codigo)
        {
            var model = Notificacion.Obtener(codigo);
            if (model != null)
                return View(model: model);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(long codigo, FormCollection collection)
        {
            try
            {
                Notificacion.Borrar(codigo);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
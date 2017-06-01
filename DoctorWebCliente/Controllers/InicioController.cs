using DoctorWebCliente.Controllers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebCliente.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Index()
        {
            ViewData.indicarPaginaActual(EnumMenuItems.Inicio);
            return View();
        }
    }
}
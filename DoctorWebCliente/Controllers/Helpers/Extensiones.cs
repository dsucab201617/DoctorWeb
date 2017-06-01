using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebCliente.Controllers.Helpers
{
    public static class Extensiones
    {
        public static bool esPaginaActual(this ViewDataDictionary bag, EnumMenuItems menu)
        {
            var paginaActual = bag[$"{EnumViewBagItems.PaginaActual}"];
            if (paginaActual != null && paginaActual is EnumMenuItems && ((EnumMenuItems)paginaActual) == menu)
                return true;
            return false;
        }

        public static string AgregarClaseSi(this HtmlHelper bag, bool expresion, string si, string sino = "")
        {
            /*var paginaActual = bag[$"{EnumViewBagItems.PaginaActual}"];
            if (paginaActual != null && paginaActual is EnumMenuItems && ((EnumMenuItems)paginaActual) == menu)
                return true;
            return false;*/
            if (expresion)
                return si;
            return sino;
        }

        public static void indicarPaginaActual(this ViewDataDictionary bag, EnumMenuItems menu)
        {
            bag[$"{EnumViewBagItems.PaginaActual}"] = menu;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DoctorWebWCFServicio.Models;

namespace DoctorWebWCFServicio.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Notificaciones" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Notificaciones.svc o Notificaciones.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Notificaciones : INotificaciones
    {
        public bool Guardar(Notificacion notificacion)
        {
            try
            {
                Notificacion.Guardar(notificacion);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Borrar(string codigo)
        {
            try
            {
                Notificacion.Borrar(codigo: String.IsNullOrEmpty(codigo) ? 0 : long.Parse(codigo));
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public Notificacion Obtener(string codigo)
        {
            var lista = Notificacion.ObtenerTodos(codigo: String.IsNullOrEmpty(codigo) ? 0 : long.Parse(codigo));
            Notificacion notificacion = null;
            if (lista.Count > 0)
                notificacion = lista.First();
            return notificacion;
        }

        public List<Notificacion> ObtenerTodos(string nombre, int indicePagina = 0, int numeroFilas = 30)
        {
            var lista = Notificacion.ObtenerTodos(nombre: nombre, indicePagina: indicePagina, numeroFilas: numeroFilas);
            return lista;
        }

        public void Prueba(string codigo)
        {
            long _codigo = 0;
            if (long.TryParse(codigo, out _codigo))
            {
                
                Notificacion.Enviar(codigo: _codigo);
            }
        }
    }
}

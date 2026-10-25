using System;
using System.Collections.Generic;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Controlador
{
    public class IncidenciaValidator
    {
        public ResultadoOperacion<Incidencia> Validar(
            string titulo,
            string descripcion,
            string responsable,
            Prioridad prioridad)
        {
            titulo = (titulo ?? string.Empty).Trim();
            descripcion = (descripcion ?? string.Empty).Trim();
            responsable = (responsable ?? string.Empty).Trim();

            if (titulo.Length < 5 || titulo.Length > 100)
            {
                return Error("El título debe tener entre 5 y 100 caracteres.");
            }

            if (descripcion.Length < 10 || descripcion.Length > 1000)
            {
                return Error("La descripción debe tener entre 10 y 1000 caracteres.");
            }

            if (responsable.Length < 2 || responsable.Length > 80)
            {
                return Error("El responsable debe tener entre 2 y 80 caracteres.");
            }

            if (!Enum.IsDefined(typeof(Prioridad), prioridad))
            {
                return Error("La prioridad debe ser Baja, Media o Alta.");
            }

            return new ResultadoOperacion<Incidencia>
            {
                Exito = true,
                Mensaje = "Validación correcta."
            };
        }

        private static ResultadoOperacion<Incidencia> Error(string mensaje)
        {
            return new ResultadoOperacion<Incidencia>
            {
                Exito = false,
                Mensaje = mensaje,
                Errores = new List<string> { mensaje }
            };
        }
    }
}

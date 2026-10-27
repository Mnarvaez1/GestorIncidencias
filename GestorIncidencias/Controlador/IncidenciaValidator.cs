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
            return ValidarInterno(titulo, descripcion, responsable, prioridad, null, false);
        }

        public ResultadoOperacion<Incidencia> Validar(
            string titulo,
            string descripcion,
            string responsable,
            Prioridad prioridad,
            int? responsableEmpleadoId)
        {
            return ValidarInterno(titulo, descripcion, responsable, prioridad, null, false, responsableEmpleadoId);
        }

        public ResultadoOperacion<Incidencia> Validar(
            string titulo,
            string descripcion,
            string responsable,
            Prioridad prioridad,
            string reportadoPor)
        {
            return ValidarInterno(titulo, descripcion, responsable, prioridad, reportadoPor, true);
        }

        public ResultadoOperacion<Incidencia> Validar(
            string titulo,
            string descripcion,
            string responsable,
            Prioridad prioridad,
            string reportadoPor,
            int? responsableEmpleadoId)
        {
            return ValidarInterno(titulo, descripcion, responsable, prioridad, reportadoPor, true, responsableEmpleadoId);
        }

        private static ResultadoOperacion<Incidencia> ValidarInterno(
            string titulo,
            string descripcion,
            string responsable,
            Prioridad prioridad,
            string reportadoPor,
            bool validarReportadoPor,
            int? responsableEmpleadoId = null)
        {
            titulo = (titulo ?? string.Empty).Trim();
            descripcion = (descripcion ?? string.Empty).Trim();
            responsable = (responsable ?? string.Empty).Trim();
            reportadoPor = (reportadoPor ?? string.Empty).Trim();

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

            if (validarReportadoPor && (reportadoPor.Length < 2 || reportadoPor.Length > 80))
            {
                return Error("El reportado por debe tener entre 2 y 80 caracteres.");
            }

            if (!Enum.IsDefined(typeof(Prioridad), prioridad))
            {
                return Error("La prioridad debe ser Baja, Media o Alta.");
            }

            if (responsableEmpleadoId.HasValue && responsableEmpleadoId.Value <= 0)
            {
                return Error("El identificador del responsable empleado debe ser un número positivo.");
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

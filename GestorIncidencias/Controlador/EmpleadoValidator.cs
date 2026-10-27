using System.Collections.Generic;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Controlador
{
    public class EmpleadoValidator
    {
        public ResultadoOperacion<Empleado> Validar(Empleado empleado)
        {
            if (empleado == null)
            {
                return Error("El empleado es obligatorio.");
            }

            if (empleado.Identificador <= 0)
            {
                return Error("El identificador del empleado debe ser un número positivo.");
            }

            var nombreCompleto = (empleado.NombreCompleto ?? string.Empty).Trim();

            if (nombreCompleto.Length < 2 || nombreCompleto.Length > 120)
            {
                return Error("El nombre completo del empleado debe tener entre 2 y 120 caracteres.");
            }

            return new ResultadoOperacion<Empleado>
            {
                Exito = true,
                Mensaje = "Validación correcta."
            };
        }

        private static ResultadoOperacion<Empleado> Error(string mensaje)
        {
            return new ResultadoOperacion<Empleado>
            {
                Exito = false,
                Mensaje = mensaje,
                Errores = new List<string> { mensaje }
            };
        }
    }
}

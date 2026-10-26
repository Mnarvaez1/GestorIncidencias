using System;
using System.Collections.Generic;
using System.Linq;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Tests.Controlador
{
    internal class EmpleadoRepositoryDoble : IEmpleadoRepository
    {
        public IList<Empleado> Registros { get; } = new List<Empleado>();

        public bool PuedeGuardarConfigurado { get; set; } = true;

        public bool GuardarDebeFallar { get; set; }

        public bool CargarDebeFallar { get; set; }

        public string CargarMensajeError { get; set; } = "Error simulado de carga de empleados.";

        public string GuardarMensajeError { get; set; } = "Error simulado de guardado de empleados.";

        public int IntentosGuardar { get; private set; }

        public bool PuedeGuardar
        {
            get { return PuedeGuardarConfigurado; }
        }

        public ResultadoOperacion<IList<Empleado>> Cargar()
        {
            if (CargarDebeFallar)
            {
                return new ResultadoOperacion<IList<Empleado>>
                {
                    Exito = false,
                    Mensaje = CargarMensajeError,
                    Errores = new List<string> { CargarMensajeError },
                    Datos = new List<Empleado>()
                };
            }

            return new ResultadoOperacion<IList<Empleado>>
            {
                Exito = true,
                Datos = ClonarLista(Registros)
            };
        }

        public ResultadoOperacion Guardar(IList<Empleado> empleados)
        {
            IntentosGuardar++;

            if (GuardarDebeFallar)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = GuardarMensajeError,
                    Errores = new List<string> { GuardarMensajeError }
                };
            }

            var copia = ClonarLista(empleados ?? new List<Empleado>());

            Registros.Clear();
            foreach (var empleado in copia)
            {
                Registros.Add(empleado);
            }

            return new ResultadoOperacion
            {
                Exito = true,
                Mensaje = "Guardado simulado correcto."
            };
        }

        private static IList<Empleado> ClonarLista(IList<Empleado> origen)
        {
            var lista = origen ?? new List<Empleado>();
            return lista
                .Select(Clonar)
                .ToList();
        }

        private static Empleado Clonar(Empleado empleado)
        {
            if (empleado == null)
            {
                return null;
            }

            return new Empleado(
                empleado.Identificador,
                empleado.NombreCompleto,
                empleado.EsTecnicoSoporte,
                empleado.Activo,
                empleado.FechaCreacion);
        }
    }
}

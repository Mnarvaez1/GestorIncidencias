using System;
using System.Collections.Generic;
using System.Linq;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Controlador
{
    public class EmpleadoController
    {
        private readonly IEmpleadoRepository repositorio;

        public EmpleadoController(IEmpleadoRepository repositorio)
        {
            this.repositorio = repositorio;
        }

        public ResultadoOperacion<Empleado> Registrar(string nombreCompleto, bool esTecnicoSoporte)
        {
            if (!repositorio.PuedeGuardar)
            {
                return Error("El repositorio no permite guardar empleados.");
            }

            var carga = repositorio.Cargar();
            if (!carga.Exito || carga.Datos == null)
            {
                return Error(carga.Mensaje ?? "No se pudieron cargar los empleados.");
            }

            var siguienteIdentificador = 1;
            foreach (var existente in carga.Datos)
            {
                if (existente.Identificador >= siguienteIdentificador)
                {
                    siguienteIdentificador = existente.Identificador + 1;
                }
            }

            var empleado = new Empleado(
                siguienteIdentificador,
                (nombreCompleto ?? string.Empty).Trim(),
                esTecnicoSoporte,
                true,
                DateTime.Now);

            var validacion = new EmpleadoValidator().Validar(empleado);
            if (!validacion.Exito)
            {
                return validacion;
            }

            carga.Datos.Add(empleado);
            var guardado = repositorio.Guardar(carga.Datos);
            if (!guardado.Exito)
            {
                return Error(guardado.Mensaje ?? "No se pudo guardar el empleado.");
            }

            return new ResultadoOperacion<Empleado>
            {
                Exito = true,
                Mensaje = "Empleado registrado correctamente.",
                Datos = Clonar(empleado)
            };
        }

        public ResultadoOperacion<IList<Empleado>> Consultar()
        {
            var carga = repositorio.Cargar();
            if (!carga.Exito)
            {
                return new ResultadoOperacion<IList<Empleado>>
                {
                    Exito = false,
                    Mensaje = carga.Mensaje,
                    Errores = carga.Errores,
                    Datos = new List<Empleado>()
                };
            }

            var empleados = ClonarLista(carga.Datos ?? new List<Empleado>())
                .OrderBy(e => e.NombreCompleto)
                .ThenBy(e => e.Identificador)
                .ToList();

            return new ResultadoOperacion<IList<Empleado>>
            {
                Exito = true,
                Mensaje = empleados.Count == 0
                    ? "No hay empleados registrados."
                    : "Consulta completada correctamente.",
                Datos = empleados
            };
        }

        public ResultadoOperacion<IList<Empleado>> Buscar(string textoBusqueda)
        {
            var consultaBase = Consultar();
            if (!consultaBase.Exito)
            {
                return consultaBase;
            }

            var empleados = consultaBase.Datos ?? new List<Empleado>();
            var texto = (textoBusqueda ?? string.Empty).Trim();

            if (texto.Length == 0)
            {
                return new ResultadoOperacion<IList<Empleado>>
                {
                    Exito = true,
                    Mensaje = empleados.Count == 0
                        ? "No hay empleados registrados."
                        : "Consulta completada correctamente.",
                    Datos = empleados
                };
            }

            var filtrados = empleados
                .Where(e => e.NombreCompleto.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            return new ResultadoOperacion<IList<Empleado>>
            {
                Exito = true,
                Mensaje = filtrados.Count == 0
                    ? "No se encontraron empleados para los criterios indicados."
                    : "Consulta completada correctamente.",
                Datos = filtrados
            };
        }

        public ResultadoOperacion<DisponibilidadOperativa> ConsultarDisponibilidad(
            int identificador,
            IList<Incidencia> incidencias)
        {
            var carga = repositorio.Cargar();
            if (!carga.Exito || carga.Datos == null)
            {
                return new ResultadoOperacion<DisponibilidadOperativa>
                {
                    Exito = false,
                    Mensaje = carga.Mensaje ?? "No se pudieron cargar los empleados.",
                    Errores = carga.Errores,
                    Datos = DisponibilidadOperativa.NoCalculada
                };
            }

            var empleado = carga.Datos.FirstOrDefault(e => e.Identificador == identificador);
            if (empleado == null)
            {
                return new ResultadoOperacion<DisponibilidadOperativa>
                {
                    Exito = false,
                    Mensaje = "No existe un empleado con el identificador indicado.",
                    Errores = new List<string> { "No existe un empleado con el identificador indicado." },
                    Datos = DisponibilidadOperativa.NoCalculada
                };
            }

            return new ResultadoOperacion<DisponibilidadOperativa>
            {
                Exito = true,
                Mensaje = "Disponibilidad calculada correctamente.",
                Datos = CalcularDisponibilidadOperativa(empleado, incidencias)
            };
        }

        private static DisponibilidadOperativa CalcularDisponibilidadOperativa(
            Empleado empleado,
            IList<Incidencia> incidencias)
        {
            if (empleado == null || !empleado.EsTecnicoSoporte || !empleado.Activo)
            {
                return DisponibilidadOperativa.NoCalculada;
            }

            var fuente = incidencias ?? new List<Incidencia>();
            var tieneIncidenciasAbiertas = fuente.Any(i =>
                i != null
                && i.ResponsableEmpleadoId.HasValue
                && i.ResponsableEmpleadoId.Value == empleado.Identificador
                && (i.Estado == EstadoIncidencia.Pendiente || i.Estado == EstadoIncidencia.EnProceso));

            return tieneIncidenciasAbiertas
                ? DisponibilidadOperativa.Ocupado
                : DisponibilidadOperativa.Libre;
        }

        public ResultadoOperacion<Empleado> Editar(int identificador, string nombreCompleto, bool esTecnicoSoporte, bool activo)
        {
            var carga = repositorio.Cargar();
            if (!carga.Exito || carga.Datos == null)
            {
                return Error(carga.Mensaje ?? "No se pudieron cargar los empleados.");
            }

            var empleado = carga.Datos.FirstOrDefault(e => e.Identificador == identificador);
            if (empleado == null)
            {
                return Error("No existe un empleado con el identificador indicado.");
            }

            if (!repositorio.PuedeGuardar)
            {
                return Error("El repositorio no permite guardar empleados.");
            }

            var propuesto = new Empleado(
                empleado.Identificador,
                (nombreCompleto ?? string.Empty).Trim(),
                esTecnicoSoporte,
                activo,
                empleado.FechaCreacion);

            var validacion = new EmpleadoValidator().Validar(propuesto);
            if (!validacion.Exito)
            {
                return validacion;
            }

            empleado.NombreCompleto = propuesto.NombreCompleto;
            empleado.EsTecnicoSoporte = propuesto.EsTecnicoSoporte;
            empleado.Activo = propuesto.Activo;

            var guardado = repositorio.Guardar(carga.Datos);
            if (!guardado.Exito)
            {
                return Error(guardado.Mensaje ?? "No se pudo guardar el empleado editado.");
            }

            return new ResultadoOperacion<Empleado>
            {
                Exito = true,
                Mensaje = "Empleado editado correctamente.",
                Datos = Clonar(empleado)
            };
        }

        public ResultadoOperacion<Empleado> Activar(int identificador)
        {
            return CambiarEstadoActivo(identificador, true);
        }

        public ResultadoOperacion<Empleado> Inactivar(int identificador)
        {
            return CambiarEstadoActivo(identificador, false);
        }

        private ResultadoOperacion<Empleado> CambiarEstadoActivo(int identificador, bool activo)
        {
            var carga = repositorio.Cargar();
            if (!carga.Exito || carga.Datos == null)
            {
                return Error(carga.Mensaje ?? "No se pudieron cargar los empleados.");
            }

            var empleado = carga.Datos.FirstOrDefault(e => e.Identificador == identificador);
            if (empleado == null)
            {
                return Error("No existe un empleado con el identificador indicado.");
            }

            if (empleado.Activo == activo)
            {
                return new ResultadoOperacion<Empleado>
                {
                    Exito = true,
                    Mensaje = activo
                        ? "El empleado ya está activo."
                        : "El empleado ya está inactivo.",
                    Datos = Clonar(empleado)
                };
            }

            if (!repositorio.PuedeGuardar)
            {
                return Error("El repositorio no permite guardar empleados.");
            }

            empleado.Activo = activo;

            var guardado = repositorio.Guardar(carga.Datos);
            if (!guardado.Exito)
            {
                return Error(guardado.Mensaje ?? "No se pudo guardar el cambio de estado del empleado.");
            }

            return new ResultadoOperacion<Empleado>
            {
                Exito = true,
                Mensaje = activo
                    ? "Empleado activado correctamente."
                    : "Empleado inactivado correctamente.",
                Datos = Clonar(empleado)
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

        private static IList<Empleado> ClonarLista(IList<Empleado> empleados)
        {
            var origen = empleados ?? new List<Empleado>();
            var copia = new List<Empleado>(origen.Count);

            foreach (var empleado in origen)
            {
                copia.Add(Clonar(empleado));
            }

            return copia;
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

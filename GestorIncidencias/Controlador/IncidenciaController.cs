using System;
using System.Collections.Generic;
using System.Linq;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Controlador
{
    public class IncidenciaController
    {
        private const string ReportadoPorLegacy = "No informado (registro legado)";
        private readonly IIncidenciaRepository repositorio;

        public IncidenciaController(IIncidenciaRepository repositorio)
        {
            this.repositorio = repositorio;
        }

        public ResultadoOperacion<Incidencia> Registrar(
            string titulo,
            string descripcion,
            string responsable,
            Prioridad prioridad)
        {
            return RegistrarInterno(titulo, descripcion, null, responsable, prioridad, false, null);
        }

        public ResultadoOperacion<Incidencia> Registrar(
            string titulo,
            string descripcion,
            string reportadoPor,
            string responsable,
            Prioridad prioridad)
        {
            return RegistrarInterno(titulo, descripcion, reportadoPor, responsable, prioridad, true, null);
        }

        public ResultadoOperacion<Incidencia> Registrar(
            string titulo,
            string descripcion,
            string reportadoPor,
            string responsable,
            Prioridad prioridad,
            int? responsableEmpleadoId)
        {
            return RegistrarInterno(titulo, descripcion, reportadoPor, responsable, prioridad, true, responsableEmpleadoId);
        }

        private ResultadoOperacion<Incidencia> RegistrarInterno(
            string titulo,
            string descripcion,
            string reportadoPor,
            string responsable,
            Prioridad prioridad,
            bool validarReportadoPor,
            int? responsableEmpleadoId)
        {
            var validador = new IncidenciaValidator();
            var validacion = validarReportadoPor
                ? validador.Validar(titulo, descripcion, responsable, prioridad, reportadoPor, responsableEmpleadoId)
                : validador.Validar(titulo, descripcion, responsable, prioridad, responsableEmpleadoId);

            if (!validacion.Exito)
            {
                return validacion;
            }

            if (!repositorio.PuedeGuardar)
            {
                return Error("El repositorio no permite guardar incidencias.");
            }

            var carga = repositorio.Cargar();

            if (!carga.Exito || carga.Datos == null)
            {
                return Error(carga.Mensaje ?? "No se pudieron cargar las incidencias.");
            }

            var siguienteIdentificador = 1;

            foreach (var incidenciaExistente in carga.Datos)
            {
                if (incidenciaExistente.Identificador >= siguienteIdentificador)
                {
                    siguienteIdentificador = incidenciaExistente.Identificador + 1;
                }
            }

            var incidencia = new Incidencia
            {
                Identificador = siguienteIdentificador,
                Titulo = (titulo ?? string.Empty).Trim(),
                Descripcion = (descripcion ?? string.Empty).Trim(),
                ReportadoPor = validarReportadoPor ? (reportadoPor ?? string.Empty).Trim() : null,
                Responsable = (responsable ?? string.Empty).Trim(),
                ResponsableEmpleadoId = responsableEmpleadoId,
                Prioridad = prioridad,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = DateTime.Now
            };

            carga.Datos.Add(incidencia);

            var guardado = repositorio.Guardar(carga.Datos);

            if (!guardado.Exito)
            {
                return Error(guardado.Mensaje ?? "No se pudo guardar la incidencia.");
            }

            return new ResultadoOperacion<Incidencia>
            {
                Exito = true,
                Mensaje = "Incidencia registrada correctamente.",
                Datos = incidencia
            };
        }

        public ResultadoOperacion<IList<Incidencia>> Consultar()
        {
            var carga = repositorio.Cargar();

            if (!carga.Exito)
            {
                return new ResultadoOperacion<IList<Incidencia>>
                {
                    Exito = false,
                    Mensaje = carga.Mensaje,
                    Errores = carga.Errores,
                    Datos = new List<Incidencia>()
                };
            }

            var incidencias = carga.Datos ?? new List<Incidencia>();
            var ordenadas = incidencias
                .OrderByDescending(i => i.FechaCreacion)
                .ThenByDescending(i => i.Identificador)
                .Select(i => new Incidencia
                {
                    Identificador = i.Identificador,
                    Titulo = i.Titulo,
                    Descripcion = i.Descripcion,
                    ReportadoPor = string.IsNullOrWhiteSpace(i.ReportadoPor)
                        ? ReportadoPorLegacy
                        : i.ReportadoPor,
                    Responsable = i.Responsable,
                    ResponsableEmpleadoId = i.ResponsableEmpleadoId,
                    Prioridad = i.Prioridad,
                    Estado = i.Estado,
                    FechaCreacion = i.FechaCreacion
                })
                .ToList();

            return new ResultadoOperacion<IList<Incidencia>>
            {
                Exito = true,
                Mensaje = ordenadas.Count == 0
                    ? "No hay incidencias registradas."
                    : "Consulta completada correctamente.",
                Datos = ordenadas
            };
        }

        public ResultadoOperacion<IList<Incidencia>> Consultar(
            string textoBusqueda,
            EstadoIncidencia? filtroEstado)
        {
            var consultaBase = Consultar();

            if (!consultaBase.Exito)
            {
                return consultaBase;
            }

            var incidencias = consultaBase.Datos ?? new List<Incidencia>();
            var texto = (textoBusqueda ?? string.Empty).Trim();
            var tieneTexto = texto.Length > 0;

            var filtradas = incidencias.Where(i =>
                (!filtroEstado.HasValue || i.Estado == filtroEstado.Value) &&
                (!tieneTexto ||
                    i.Titulo.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    i.Responsable.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0))
                .ToList();

            return new ResultadoOperacion<IList<Incidencia>>
            {
                Exito = true,
                Mensaje = filtradas.Count == 0
                    ? "No se encontraron incidencias para los criterios indicados."
                    : "Consulta completada correctamente.",
                Datos = filtradas
            };
        }

        public ResultadoOperacion<Incidencia> Editar(
            int identificador,
            string titulo,
            string descripcion,
            string responsable,
            Prioridad prioridad)
        {
            return EditarInterno(identificador, titulo, descripcion, null, responsable, prioridad, false, null, false);
        }

        public ResultadoOperacion<Incidencia> Editar(
            int identificador,
            string titulo,
            string descripcion,
            string reportadoPor,
            string responsable,
            Prioridad prioridad)
        {
            return EditarInterno(identificador, titulo, descripcion, reportadoPor, responsable, prioridad, true, null, false);
        }

        public ResultadoOperacion<Incidencia> Editar(
            int identificador,
            string titulo,
            string descripcion,
            string reportadoPor,
            string responsable,
            Prioridad prioridad,
            int? responsableEmpleadoId)
        {
            return EditarInterno(identificador, titulo, descripcion, reportadoPor, responsable, prioridad, true, responsableEmpleadoId, true);
        }

        private ResultadoOperacion<Incidencia> EditarInterno(
            int identificador,
            string titulo,
            string descripcion,
            string reportadoPor,
            string responsable,
            Prioridad prioridad,
            bool validarReportadoPor,
            int? responsableEmpleadoId,
            bool actualizarResponsableEmpleadoId)
        {
            var carga = repositorio.Cargar();

            if (!carga.Exito || carga.Datos == null)
            {
                return Error(carga.Mensaje ?? "No se pudieron cargar las incidencias.");
            }

            var incidencia = carga.Datos.FirstOrDefault(i => i.Identificador == identificador);

            if (incidencia == null)
            {
                return Error("No existe una incidencia con el identificador indicado.");
            }

            var validador = new IncidenciaValidator();
            var responsableEmpleadoIdValidacion = actualizarResponsableEmpleadoId
                ? responsableEmpleadoId
                : incidencia.ResponsableEmpleadoId;
            var validacion = validarReportadoPor
                ? validador.Validar(titulo, descripcion, responsable, prioridad, reportadoPor, responsableEmpleadoIdValidacion)
                : validador.Validar(titulo, descripcion, responsable, prioridad, responsableEmpleadoIdValidacion);

            if (!validacion.Exito)
            {
                return validacion;
            }

            if (!repositorio.PuedeGuardar)
            {
                return Error("El repositorio no permite guardar incidencias.");
            }

            incidencia.Titulo = (titulo ?? string.Empty).Trim();
            incidencia.Descripcion = (descripcion ?? string.Empty).Trim();
            if (validarReportadoPor)
            {
                incidencia.ReportadoPor = (reportadoPor ?? string.Empty).Trim();
            }

            incidencia.Responsable = (responsable ?? string.Empty).Trim();
            if (actualizarResponsableEmpleadoId)
            {
                incidencia.ResponsableEmpleadoId = responsableEmpleadoId;
            }

            incidencia.Prioridad = prioridad;

            var guardado = repositorio.Guardar(carga.Datos);

            if (!guardado.Exito)
            {
                return Error(guardado.Mensaje ?? "No se pudo guardar la incidencia editada.");
            }

            return new ResultadoOperacion<Incidencia>
            {
                Exito = true,
                Mensaje = "Incidencia editada correctamente.",
                Datos = incidencia
            };
        }

        public ResultadoOperacion<Incidencia> CambiarEstado(
            int identificador,
            EstadoIncidencia estadoSolicitado)
        {
            if (!Enum.IsDefined(typeof(EstadoIncidencia), estadoSolicitado))
            {
                return Error("El estado solicitado no es válido.");
            }

            var carga = repositorio.Cargar();

            if (!carga.Exito || carga.Datos == null)
            {
                return Error(carga.Mensaje ?? "No se pudieron cargar las incidencias.");
            }

            var incidencia = carga.Datos.FirstOrDefault(i => i.Identificador == identificador);

            if (incidencia == null)
            {
                return Error("No existe una incidencia con el identificador indicado.");
            }

            if (incidencia.Estado == estadoSolicitado)
            {
                return new ResultadoOperacion<Incidencia>
                {
                    Exito = true,
                    Mensaje = "La incidencia ya tiene el estado solicitado.",
                    Datos = incidencia
                };
            }

            var transicionPermitida =
                (incidencia.Estado == EstadoIncidencia.Pendiente && estadoSolicitado == EstadoIncidencia.EnProceso) ||
                (incidencia.Estado == EstadoIncidencia.EnProceso && estadoSolicitado == EstadoIncidencia.Resuelta) ||
                (incidencia.Estado == EstadoIncidencia.Resuelta && estadoSolicitado == EstadoIncidencia.EnProceso);

            if (!transicionPermitida)
            {
                return Error("La transición de estado solicitada no está permitida.");
            }

            if (!repositorio.PuedeGuardar)
            {
                return Error("El repositorio no permite guardar incidencias.");
            }

            incidencia.Estado = estadoSolicitado;

            var guardado = repositorio.Guardar(carga.Datos);

            if (!guardado.Exito)
            {
                return Error(guardado.Mensaje ?? "No se pudo guardar el cambio de estado.");
            }

            return new ResultadoOperacion<Incidencia>
            {
                Exito = true,
                Mensaje = "Estado de incidencia actualizado correctamente.",
                Datos = incidencia
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

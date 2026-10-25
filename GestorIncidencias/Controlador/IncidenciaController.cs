using System;
using System.Collections.Generic;
using System.Linq;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Controlador
{
    public class IncidenciaController
    {
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
            var validacion = new IncidenciaValidator().Validar(
                titulo,
                descripcion,
                responsable,
                prioridad);

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
                Responsable = (responsable ?? string.Empty).Trim(),
                Prioridad = prioridad,
                Estado = EstadoIncidencia.Pendiente,
                FechaCreacion = DateTime.Now
            };

            var guardado = repositorio.Guardar(new List<Incidencia> { incidencia });

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

        public ResultadoOperacion<Incidencia> Editar(
            int identificador,
            string titulo,
            string descripcion,
            string responsable,
            Prioridad prioridad)
        {
            return Error("Edición no implementada.");
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

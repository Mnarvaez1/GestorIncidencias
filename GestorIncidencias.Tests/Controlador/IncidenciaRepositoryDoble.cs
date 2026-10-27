using System.Collections.Generic;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Tests.Controlador
{
    internal class IncidenciaRepositoryDoble : IIncidenciaRepository
    {
        public IList<Incidencia> Registros { get; } = new List<Incidencia>();

        public bool PuedeGuardarConfigurado { get; set; } = true;

        public bool GuardarDebeFallar { get; set; }

        public bool CargarDebeFallar { get; set; }

        public string CargarMensajeError { get; set; } = "Error simulado de carga.";

        public int IntentosGuardar { get; private set; }

        public bool PuedeGuardar
        {
            get { return PuedeGuardarConfigurado; }
        }

        public ResultadoOperacion<IList<Incidencia>> Cargar()
        {
            if (CargarDebeFallar)
            {
                return new ResultadoOperacion<IList<Incidencia>>
                {
                    Exito = false,
                    Mensaje = CargarMensajeError,
                    Errores = new List<string> { CargarMensajeError },
                    Datos = new List<Incidencia>()
                };
            }

            return new ResultadoOperacion<IList<Incidencia>>
            {
                Exito = true,
                Datos = ClonarLista(Registros)
            };
        }

        public ResultadoOperacion Guardar(IList<Incidencia> incidencias)
        {
            IntentosGuardar++;

            if (GuardarDebeFallar)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "Error simulado de guardado."
                };
            }

            var copia = ClonarLista(incidencias ?? new List<Incidencia>());

            Registros.Clear();

            foreach (var incidencia in copia)
            {
                Registros.Add(incidencia);
            }

            return new ResultadoOperacion
            {
                Exito = true
            };
        }

        private static IList<Incidencia> ClonarLista(IList<Incidencia> origen)
        {
            var resultado = new List<Incidencia>();

            foreach (var incidencia in origen)
            {
                resultado.Add(new Incidencia
                {
                    Identificador = incidencia.Identificador,
                    Titulo = incidencia.Titulo,
                    Descripcion = incidencia.Descripcion,
                    ReportadoPor = incidencia.ReportadoPor,
                    Responsable = incidencia.Responsable,
                    ResponsableEmpleadoId = incidencia.ResponsableEmpleadoId,
                    Prioridad = incidencia.Prioridad,
                    Estado = incidencia.Estado,
                    FechaCreacion = incidencia.FechaCreacion
                });
            }

            return resultado;
        }
    }
}

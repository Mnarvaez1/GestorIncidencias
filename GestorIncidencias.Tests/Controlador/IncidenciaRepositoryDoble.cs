using System.Collections.Generic;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Tests.Controlador
{
    internal class IncidenciaRepositoryDoble : IIncidenciaRepository
    {
        public IList<Incidencia> Registros { get; } = new List<Incidencia>();

        public bool PuedeGuardar
        {
            get { return true; }
        }

        public ResultadoOperacion<IList<Incidencia>> Cargar()
        {
            return new ResultadoOperacion<IList<Incidencia>>
            {
                Exito = true,
                Datos = Registros
            };
        }

        public ResultadoOperacion Guardar(IList<Incidencia> incidencias)
        {
            foreach (var incidencia in incidencias)
            {
                Registros.Add(incidencia);
            }

            return new ResultadoOperacion
            {
                Exito = true
            };
        }
    }
}

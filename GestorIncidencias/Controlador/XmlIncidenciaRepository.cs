using System.Collections.Generic;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Controlador
{
    public class XmlIncidenciaRepository : IIncidenciaRepository
    {
        private readonly string rutaArchivo;

        public XmlIncidenciaRepository(string rutaArchivo)
        {
            this.rutaArchivo = rutaArchivo;
        }

        public bool PuedeGuardar
        {
            get { return true; }
        }

        public ResultadoOperacion<IList<Incidencia>> Cargar()
        {
            return new ResultadoOperacion<IList<Incidencia>>
            {
                Exito = true,
                Datos = new List<Incidencia>()
            };
        }

        public ResultadoOperacion Guardar(IList<Incidencia> incidencias)
        {
            return new ResultadoOperacion
            {
                Exito = true
            };
        }
    }
}

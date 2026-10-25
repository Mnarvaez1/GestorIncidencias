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
            return new ResultadoOperacion<Incidencia>
            {
                Exito = true,
                Mensaje = "Registro provisional."
            };
        }
    }
}

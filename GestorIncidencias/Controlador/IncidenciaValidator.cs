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
            return new ResultadoOperacion<Incidencia>
            {
                Exito = true,
                Mensaje = "Validación provisional correcta."
            };
        }
    }
}

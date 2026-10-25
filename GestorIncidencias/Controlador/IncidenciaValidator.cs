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
            if (string.IsNullOrWhiteSpace(titulo) || titulo.Trim().Length < 5)
            {
                return new ResultadoOperacion<Incidencia>
                {
                    Exito = false,
                    Mensaje = "El título debe tener al menos cinco caracteres."
                };
            }

            return new ResultadoOperacion<Incidencia>
            {
                Exito = true,
                Mensaje = "Validación correcta."
            };
        }
    }
}

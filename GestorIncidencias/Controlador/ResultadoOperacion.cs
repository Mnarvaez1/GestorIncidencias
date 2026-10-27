using System.Collections.Generic;

namespace GestorIncidencias.Controlador
{
    public class ResultadoOperacion
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public IList<string> Errores { get; set; }
    }

    public class ResultadoOperacion<T> : ResultadoOperacion
    {
        public T Datos { get; set; }
    }
}

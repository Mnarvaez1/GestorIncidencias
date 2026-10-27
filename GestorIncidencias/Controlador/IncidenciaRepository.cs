using System.Collections.Generic;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Controlador
{
    public interface IIncidenciaRepository
    {
        bool PuedeGuardar { get; }

        ResultadoOperacion<IList<Incidencia>> Cargar();

        ResultadoOperacion Guardar(IList<Incidencia> incidencias);
    }
}

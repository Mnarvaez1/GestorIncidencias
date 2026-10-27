using System.Collections.Generic;
using GestorIncidencias.Entidades;

namespace GestorIncidencias.Controlador
{
    public interface IEmpleadoRepository
    {
        bool PuedeGuardar { get; }

        ResultadoOperacion<IList<Empleado>> Cargar();

        ResultadoOperacion Guardar(IList<Empleado> empleados);
    }
}

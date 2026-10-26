using System;

namespace GestorIncidencias.Entidades
{
    public class Empleado
    {
        public Empleado(int identificador, string nombreCompleto, bool esTecnicoSoporte, bool activo, DateTime fechaCreacion)
        {
            Identificador = identificador;
            NombreCompleto = nombreCompleto;
            EsTecnicoSoporte = esTecnicoSoporte;
            Activo = activo;
            FechaCreacion = fechaCreacion;
        }

        public int Identificador { get; }

        public string NombreCompleto { get; set; }

        public bool EsTecnicoSoporte { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; }
    }
}

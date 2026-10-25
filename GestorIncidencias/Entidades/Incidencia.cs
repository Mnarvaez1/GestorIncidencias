using System;

namespace GestorIncidencias.Entidades
{
    public class Incidencia
    {
        public int Identificador { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ReportadoPor { get; set; }
        public string Responsable { get; set; }
        public Prioridad Prioridad { get; set; }
        public EstadoIncidencia Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

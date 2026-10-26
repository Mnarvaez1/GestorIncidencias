using GestorIncidencias.Entidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestorIncidencias
{
    public partial class frmVistaPreviaReporte : Form
    {
        private const string ReportadoPorLegacy = "No informado (registro legado)";
        private readonly List<Incidencia> incidencias;

        public frmVistaPreviaReporte()
            : this(new List<Incidencia>())
        {
        }

        public frmVistaPreviaReporte(IList<Incidencia> incidencias)
        {
            InitializeComponent();
            this.incidencias = ClonarIncidencias(incidencias);
            CargarVistaPrevia();

            btnCerrar.Click += BtnCerrar_Click;
        }

        private void CargarVistaPrevia()
        {
            lblFechaGeneracion.Text = "Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            lblResumen.Text = "Total de incidencias incluidas: " + incidencias.Count;

            dgvIncidenciasReporte.Rows.Clear();

            foreach (var incidencia in incidencias)
            {
                dgvIncidenciasReporte.Rows.Add(
                    incidencia.Identificador,
                    incidencia.Titulo,
                    NormalizarReportadoPor(incidencia.ReportadoPor),
                    incidencia.Responsable,
                    incidencia.Prioridad,
                    ObtenerTextoEstado(incidencia.Estado),
                    incidencia.FechaCreacion.ToString("dd/MM/yyyy HH:mm"));
            }

            var hayIncidencias = incidencias.Count > 0;
            dgvIncidenciasReporte.Visible = hayIncidencias;
            lblEstadoVacio.Visible = !hayIncidencias;
        }

        private static List<Incidencia> ClonarIncidencias(IList<Incidencia> incidencias)
        {
            var origen = incidencias ?? new List<Incidencia>();
            var copia = new List<Incidencia>(origen.Count);

            foreach (var incidencia in origen)
            {
                if (incidencia == null)
                {
                    continue;
                }

                copia.Add(new Incidencia
                {
                    Identificador = incidencia.Identificador,
                    Titulo = incidencia.Titulo,
                    Descripcion = incidencia.Descripcion,
                    ReportadoPor = incidencia.ReportadoPor,
                    Responsable = incidencia.Responsable,
                    Prioridad = incidencia.Prioridad,
                    Estado = incidencia.Estado,
                    FechaCreacion = incidencia.FechaCreacion
                });
            }

            return copia;
        }

        private static string NormalizarReportadoPor(string reportadoPor)
        {
            return string.IsNullOrWhiteSpace(reportadoPor)
                ? ReportadoPorLegacy
                : reportadoPor;
        }

        private static string ObtenerTextoEstado(EstadoIncidencia estado)
        {
            return estado == EstadoIncidencia.EnProceso
                ? "En proceso"
                : estado.ToString();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

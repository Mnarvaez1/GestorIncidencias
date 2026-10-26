using GestorIncidencias.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace GestorIncidencias
{
    public partial class frmVistaPreviaReporte : Form
    {
        private const string ReportadoPorLegacy = "No informado (registro legado)";
        private readonly List<Incidencia> incidencias;
        private DateTime fechaGeneracion;

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
            btnExportarHtml.Click += BtnExportarHtml_Click;
        }

        private void CargarVistaPrevia()
        {
            fechaGeneracion = DateTime.Now;
            lblFechaGeneracion.Text = "Generado: " + fechaGeneracion.ToString("dd/MM/yyyy HH:mm");
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

        private void BtnExportarHtml_Click(object sender, EventArgs e)
        {
            using (var dialogo = new SaveFileDialog())
            {
                dialogo.Title = "Exportar reporte a HTML";
                dialogo.Filter = "Archivo HTML (*.html)|*.html";
                dialogo.DefaultExt = "html";
                dialogo.AddExtension = true;
                dialogo.FileName = "reporte-incidencias-" + fechaGeneracion.ToString("yyyyMMdd-HHmm") + ".html";

                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    MessageBox.Show(
                        this,
                        "Exportación cancelada por el operador.",
                        "Vista previa de reporte",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    var contenidoHtml = ConstruirDocumentoHtml();
                    File.WriteAllText(dialogo.FileName, contenidoHtml, Encoding.UTF8);

                    MessageBox.Show(
                        this,
                        "Reporte exportado correctamente a HTML.",
                        "Vista previa de reporte",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        this,
                        "No se pudo exportar el reporte a HTML. " + ex.Message,
                        "Vista previa de reporte",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private string ConstruirDocumentoHtml()
        {
            var html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html lang=\"es\">");
            html.AppendLine("<head>");
            html.AppendLine("  <meta charset=\"utf-8\" />");
            html.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />");
            html.AppendLine("  <title>Vista previa de reporte - GestorIncidencias</title>");
            html.AppendLine("  <style>");
            html.AppendLine("    body { font-family: 'Segoe UI', Arial, sans-serif; background: #f4f7fc; color: #1a2d4a; margin: 0; }");
            html.AppendLine("    .contenedor { max-width: 1180px; margin: 0 auto; padding: 24px; }");
            html.AppendLine("    .encabezado { background: #1a2d4a; color: #ffffff; padding: 18px 22px; border-radius: 8px; }");
            html.AppendLine("    .titulo { margin: 0; font-size: 28px; font-weight: 700; }");
            html.AppendLine("    .subtitulo { margin: 6px 0 0; color: #d2dff2; }");
            html.AppendLine("    .meta { margin-top: 10px; font-size: 14px; }");
            html.AppendLine("    .resumen { background: #ffffff; border-radius: 8px; margin-top: 14px; padding: 14px 18px; font-weight: 600; }");
            html.AppendLine("    table { width: 100%; border-collapse: collapse; margin-top: 14px; background: #ffffff; border-radius: 8px; overflow: hidden; }");
            html.AppendLine("    th { background: #223b5c; color: #ffffff; text-align: left; padding: 10px; font-size: 14px; }");
            html.AppendLine("    td { border-top: 1px solid #dce1e8; padding: 10px; font-size: 14px; }");
            html.AppendLine("    .vacio { background: #ffffff; border-radius: 8px; margin-top: 14px; padding: 36px 18px; text-align: center; color: #647080; }");
            html.AppendLine("  </style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("  <div class=\"contenedor\">");
            html.AppendLine("    <section class=\"encabezado\">");
            html.AppendLine("      <h1 class=\"titulo\">VISTA PREVIA DE REPORTE</h1>");
            html.AppendLine("      <p class=\"subtitulo\">GestorIncidencias - Reporte operativo</p>");
            html.AppendLine("      <div class=\"meta\">Generado: " + EscapeHtml(fechaGeneracion.ToString("dd/MM/yyyy HH:mm")) + "</div>");
            html.AppendLine("    </section>");
            html.AppendLine("    <section class=\"resumen\">Total de incidencias incluidas: " + incidencias.Count + "</section>");

            if (incidencias.Count == 0)
            {
                html.AppendLine("    <section class=\"vacio\">No hay incidencias para mostrar en la vista previa del reporte.</section>");
            }
            else
            {
                html.AppendLine("    <table>");
                html.AppendLine("      <thead>");
                html.AppendLine("        <tr>");
                html.AppendLine("          <th>ID</th>");
                html.AppendLine("          <th>Título</th>");
                html.AppendLine("          <th>Reportado por</th>");
                html.AppendLine("          <th>Responsable</th>");
                html.AppendLine("          <th>Prioridad</th>");
                html.AppendLine("          <th>Estado</th>");
                html.AppendLine("          <th>Fecha</th>");
                html.AppendLine("        </tr>");
                html.AppendLine("      </thead>");
                html.AppendLine("      <tbody>");

                foreach (var incidencia in incidencias)
                {
                    html.AppendLine("        <tr>");
                    html.AppendLine("          <td>" + incidencia.Identificador + "</td>");
                    html.AppendLine("          <td>" + EscapeHtml(incidencia.Titulo) + "</td>");
                    html.AppendLine("          <td>" + EscapeHtml(NormalizarReportadoPor(incidencia.ReportadoPor)) + "</td>");
                    html.AppendLine("          <td>" + EscapeHtml(incidencia.Responsable) + "</td>");
                    html.AppendLine("          <td>" + EscapeHtml(incidencia.Prioridad.ToString()) + "</td>");
                    html.AppendLine("          <td>" + EscapeHtml(ObtenerTextoEstado(incidencia.Estado)) + "</td>");
                    html.AppendLine("          <td>" + EscapeHtml(incidencia.FechaCreacion.ToString("dd/MM/yyyy HH:mm")) + "</td>");
                    html.AppendLine("        </tr>");
                }

                html.AppendLine("      </tbody>");
                html.AppendLine("    </table>");
            }

            html.AppendLine("  </div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
        }

        private static string EscapeHtml(string valor)
        {
            return WebUtility.HtmlEncode(valor ?? string.Empty);
        }
    }
}

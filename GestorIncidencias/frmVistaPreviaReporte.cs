using GestorIncidencias.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using GestorIncidencias.Controlador;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;

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

            InicializarEstilosVisuales();
            CargarVistaPrevia();

            btnCerrar.Click += BtnCerrar_Click;
            btnExportarHtml.Click += BtnExportarHtml_Click;
            lnkVerDetalle.LinkClicked += LnkVerDetalle_LinkClicked;
        }

        private void InicializarEstilosVisuales()
        {
            AplicarEsquinasRedondeadas(pnlDetalle, 14);
            AplicarEsquinasRedondeadas(pnlCardTotal, 10);
            AplicarEsquinasRedondeadas(pnlCardPendientes, 10);
            AplicarEsquinasRedondeadas(pnlCardEnProceso, 10);
            AplicarEsquinasRedondeadas(pnlCardResueltas, 10);

            pnlIconoDocumento.Paint += PnlIconoDocumento_Paint;
            Resize += FrmVistaPreviaReporte_Resize;
        }

        private void FrmVistaPreviaReporte_Resize(object sender, EventArgs e)
        {
            AjustarTarjetasIncidencia();
            AplicarEsquinasRedondeadas(pnlDetalle, 14);
            AplicarEsquinasRedondeadas(pnlCardTotal, 10);
            AplicarEsquinasRedondeadas(pnlCardPendientes, 10);
            AplicarEsquinasRedondeadas(pnlCardEnProceso, 10);
            AplicarEsquinasRedondeadas(pnlCardResueltas, 10);
        }

        private void CargarVistaPrevia()
        {
            fechaGeneracion = DateTime.Now;
            lblFechaGeneracion.Text = fechaGeneracion.ToString("dd/MM/yyyy · HH:mm");
            lblResumen.Text = "Detalle de incidencias";

            ActualizarIndicadores();
            ActualizarConteoRegistros();

            dgvIncidenciasReporte.Rows.Clear();
            flpIncidencias.Controls.Clear();

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

                flpIncidencias.Controls.Add(CrearTarjetaIncidencia(incidencia));
            }

            AjustarTarjetasIncidencia();

            var hayIncidencias = incidencias.Count > 0;
            flpIncidencias.Visible = hayIncidencias;
            dgvIncidenciasReporte.Visible = hayIncidencias && pnlTabla.Visible;

            lblEstadoVacio.Visible = !hayIncidencias;
            lblEstadoVacio.BringToFront();
            flpIncidencias.BringToFront();

            if (!hayIncidencias)
            {
                lnkVerDetalle.Enabled = false;
                pnlTabla.Visible = false;
                lnkVerDetalle.Text = "Ver detalle técnico";
            }
            else
            {
                lnkVerDetalle.Enabled = true;
            }
        }

        private void AjustarTarjetasIncidencia()
        {
            var anchoTarjeta = ObtenerAnchoTarjeta();
            foreach (Control control in flpIncidencias.Controls)
            {
                control.Width = anchoTarjeta;
                AplicarEsquinasRedondeadas(control, 8);
            }
        }

        private void ActualizarIndicadores()
        {
            var total = incidencias.Count;
            var pendientes = incidencias.Count(i => i.Estado == EstadoIncidencia.Pendiente);
            var enProceso = incidencias.Count(i => i.Estado == EstadoIncidencia.EnProceso);
            var resueltas = incidencias.Count(i => i.Estado == EstadoIncidencia.Resuelta);

            lblTotalValor.Text = total.ToString("00");
            lblPendientesValor.Text = pendientes.ToString("00");
            lblEnProcesoValor.Text = enProceso.ToString("00");
            lblResueltasValor.Text = resueltas.ToString("00");
        }

        private void ActualizarConteoRegistros()
        {
            var total = incidencias.Count;
            lblConteoRegistros.Text = total.ToString("00") + (total == 1 ? " registro" : " registros");
        }

        private Control CrearTarjetaIncidencia(Incidencia incidencia)
        {
            var tarjeta = new Panel
            {
                Width = ObtenerAnchoTarjeta(),
                Height = 72,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(16, 12, 16, 10)
            };

            AplicarEsquinasRedondeadas(tarjeta, 8);

            var lblEstado = new Label
            {
                Dock = DockStyle.Right,
                Width = 170,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(197, 161, 92),
                Text = ObtenerTextoEstado(incidencia.Estado)
            };

            var lblTituloIncidencia = new Label
            {
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 39, 41),
                Text = string.Format("#{0} — {1}", incidencia.Identificador, incidencia.Titulo ?? string.Empty)
            };

            var lblMeta = new Label
            {
                Dock = DockStyle.Top,
                Height = 22,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(119, 115, 107),
                Text = string.Format(
                    "{0} · {1} · Prioridad {2}",
                    NormalizarReportadoPor(incidencia.ReportadoPor),
                    incidencia.Responsable ?? string.Empty,
                    incidencia.Prioridad.ToString().ToLowerInvariant())
            };

            var panelTexto = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            panelTexto.Controls.Add(lblMeta);
            panelTexto.Controls.Add(lblTituloIncidencia);

            tarjeta.Controls.Add(panelTexto);
            tarjeta.Controls.Add(lblEstado);

            return tarjeta;
        }

        private int ObtenerAnchoTarjeta()
        {
            var anchoDisponible = flpIncidencias.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 6;
            return Math.Max(700, anchoDisponible);
        }

        private void AplicarEsquinasRedondeadas(Control control, int radio)
        {
            if (control == null || control.Width <= 0 || control.Height <= 0)
            {
                return;
            }

            var rectangulo = new Rectangle(0, 0, control.Width, control.Height);
            var trazado = CrearTrazadoRedondeado(rectangulo, radio);

            var regionAnterior = control.Region;
            control.Region = new Region(trazado);
            if (regionAnterior != null)
            {
                regionAnterior.Dispose();
            }
        }

        private static GraphicsPath CrearTrazadoRedondeado(Rectangle rectangulo, int radio)
        {
            var diametro = radio * 2;
            var trazado = new GraphicsPath();

            trazado.AddArc(rectangulo.X, rectangulo.Y, diametro, diametro, 180, 90);
            trazado.AddArc(rectangulo.Right - diametro, rectangulo.Y, diametro, diametro, 270, 90);
            trazado.AddArc(rectangulo.Right - diametro, rectangulo.Bottom - diametro, diametro, diametro, 0, 90);
            trazado.AddArc(rectangulo.X, rectangulo.Bottom - diametro, diametro, diametro, 90, 90);
            trazado.CloseFigure();

            return trazado;
        }

        private void PnlIconoDocumento_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Transparent);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var borde = Color.FromArgb(197, 161, 92);
            using (var lapiz = new Pen(borde, 2f))
            {
                var rect = new Rectangle(10, 7, 24, 32);
                e.Graphics.DrawRectangle(lapiz, rect);

                e.Graphics.DrawLine(lapiz, 24, 7, 34, 17);
                e.Graphics.DrawLine(lapiz, 34, 17, 24, 17);

                e.Graphics.DrawLine(lapiz, 14, 21, 30, 21);
                e.Graphics.DrawLine(lapiz, 14, 26, 30, 26);
                e.Graphics.DrawLine(lapiz, 14, 31, 26, 31);
            }
        }

        private void LnkVerDetalle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlTabla.Visible = !pnlTabla.Visible;
            pnlTabla.Height = pnlTabla.Visible ? 190 : 0;
            dgvIncidenciasReporte.Visible = pnlTabla.Visible && incidencias.Count > 0;
            lblEstadoVacio.Visible = pnlTabla.Visible && incidencias.Count == 0;
            lnkVerDetalle.Text = pnlTabla.Visible ? "Ocultar detalle técnico" : "Ver detalle técnico";
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
                    var exportacion = ExportarHtml(dialogo.FileName, incidencias, fechaGeneracion);

                    if (!exportacion.Exito)
                    {
                        MessageBox.Show(
                            this,
                            exportacion.Mensaje ?? "No se pudo exportar el reporte a HTML.",
                            "Vista previa de reporte",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

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

        internal static ResultadoOperacion ExportarHtml(string rutaArchivo, IList<Incidencia> incidencias, DateTime fechaGeneracion)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo))
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "No se especificó una ruta válida para exportar el reporte."
                };
            }

            try
            {
                var contenidoHtml = ConstruirDocumentoHtml(incidencias, fechaGeneracion);
                File.WriteAllText(rutaArchivo, contenidoHtml, Encoding.UTF8);

                return new ResultadoOperacion
                {
                    Exito = true,
                    Mensaje = "Reporte exportado correctamente a HTML."
                };
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = "No se pudo exportar el reporte a HTML. " + ex.Message
                };
            }
        }

        internal static string ConstruirDocumentoHtml(IList<Incidencia> incidencias, DateTime fechaGeneracion)
        {
            var incidenciasReporte = ClonarIncidencias(incidencias);
            var total = incidenciasReporte.Count;
            var pendientes = incidenciasReporte.Count(i => i.Estado == EstadoIncidencia.Pendiente);
            var enProceso = incidenciasReporte.Count(i => i.Estado == EstadoIncidencia.EnProceso);
            var resueltas = incidenciasReporte.Count(i => i.Estado == EstadoIncidencia.Resuelta);
            var conteoRegistros = total.ToString("00") + (total == 1 ? " registro" : " registros");

            var html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html lang=\"es\">");
            html.AppendLine("<head>");
            html.AppendLine("  <meta charset=\"utf-8\" />");
            html.AppendLine("  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />");
            html.AppendLine("  <title>Informe ejecutivo - XAMY TECHNOLOGIES</title>");
            html.AppendLine("  <style>");
            html.AppendLine("    * { box-sizing: border-box; }");
            html.AppendLine("    body { margin: 0; font-family: 'Segoe UI', Arial, sans-serif; background: #F5F3EC; color: #252729; }");
            html.AppendLine("    .contenedor { max-width: 1120px; margin: 0 auto; padding: 18px; }");
            html.AppendLine("    .encabezado { background: #252729; color: #FFFFFF; border-radius: 12px; padding: 16px 20px 12px; }");
            html.AppendLine("    .marca { margin: 0; color: #C5A15C; font-size: 13px; font-weight: 700; letter-spacing: 0.5px; }");
            html.AppendLine("    .titulo { margin: 6px 0 0; color: #FFFFFF; font-size: 34px; line-height: 1.1; font-weight: 700; }");
            html.AppendLine("    .subtitulo { margin: 6px 0 8px; color: #E7E3D8; font-size: 15px; }");
            html.AppendLine("    .linea-dorada { height: 1px; background: #C5A15C; margin: 8px 0 10px; }");
            html.AppendLine("    .encabezado-pie { display: flex; justify-content: space-between; gap: 12px; flex-wrap: wrap; color: #F5F3EC; font-size: 13px; }");
            html.AppendLine("    .encabezado-pie .operativo { font-weight: 700; letter-spacing: 0.5px; }");
            html.AppendLine("    .encabezado-pie .fecha { color: #E7E3D8; }");
            html.AppendLine("    .indicadores { display: grid; grid-template-columns: repeat(4, minmax(0, 1fr)); gap: 10px; margin-top: 12px; }");
            html.AppendLine("    .indicador { background: #FFFFFF; border: 1px solid #E6DFD3; border-radius: 10px; padding: 10px 8px; text-align: center; min-height: 86px; }");
            html.AppendLine("    .indicador-valor { font-size: 32px; line-height: 1; font-weight: 700; color: #252729; margin: 0 0 6px; }");
            html.AppendLine("    .indicador-etiqueta { margin: 0; font-size: 13px; color: #77736B; }");
            html.AppendLine("    .detalle { margin-top: 12px; background: #FFFFFF; border-radius: 12px; padding: 14px 16px; border: 1px solid #ECE6DA; }");
            html.AppendLine("    .detalle-cabecera { display: flex; justify-content: space-between; align-items: baseline; gap: 10px; }");
            html.AppendLine("    .detalle-titulo { margin: 0; font-size: 20px; color: #252729; }");
            html.AppendLine("    .detalle-conteo { margin: 0; font-size: 14px; color: #C5A15C; font-weight: 700; }");
            html.AppendLine("    .detalle-linea { height: 1px; background: #C5A15C; margin: 10px 0; }");
            html.AppendLine("    .resumen { margin: 0 0 10px; color: #77736B; font-size: 13px; }");
            html.AppendLine("    .lista-incidencias { display: flex; flex-direction: column; gap: 8px; }");
            html.AppendLine("    .incidencia { display: flex; justify-content: space-between; align-items: center; gap: 12px; border: 1px solid #E9E3D8; border-radius: 10px; padding: 10px 12px; background: #FFFFFF; }");
            html.AppendLine("    .incidencia-titulo { margin: 0; color: #252729; font-size: 18px; font-weight: 700; }");
            html.AppendLine("    .incidencia-meta { margin: 4px 0 0; color: #77736B; font-size: 13px; }");
            html.AppendLine("    .incidencia-fecha { margin: 3px 0 0; color: #77736B; font-size: 12px; }");
            html.AppendLine("    .incidencia-estado { margin: 0; color: #C5A15C; font-weight: 700; min-width: 130px; text-align: right; }");
            html.AppendLine("    .detalle-tecnico { margin-top: 12px; }");
            html.AppendLine("    .detalle-tecnico > summary { color: #77736B; cursor: pointer; font-weight: 600; }");
            html.AppendLine("    table { width: 100%; border-collapse: collapse; margin-top: 8px; background: #FFFFFF; }");
            html.AppendLine("    th { background: #252729; color: #C5A15C; text-align: left; padding: 10px; font-size: 13px; }");
            html.AppendLine("    td { border-top: 1px solid #E7E0D3; padding: 10px; font-size: 13px; color: #252729; }");
            html.AppendLine("    .vacio { background: #FFFFFF; border: 1px dashed #D8CDB8; border-radius: 10px; margin-top: 8px; padding: 26px 16px; text-align: center; color: #77736B; }");
            html.AppendLine("    @media (max-width: 900px) {");
            html.AppendLine("      .indicadores { grid-template-columns: repeat(2, minmax(0, 1fr)); }");
            html.AppendLine("      .incidencia { flex-direction: column; align-items: flex-start; }");
            html.AppendLine("      .incidencia-estado { text-align: left; min-width: 0; }");
            html.AppendLine("    }");
            html.AppendLine("    @media (max-width: 520px) {");
            html.AppendLine("      .indicadores { grid-template-columns: 1fr; }");
            html.AppendLine("      .titulo { font-size: 28px; }");
            html.AppendLine("    }");
            html.AppendLine("  </style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("  <div class=\"contenedor\">");
            html.AppendLine("    <section class=\"encabezado\">");
            html.AppendLine("      <p class=\"marca\">XAMY TECHNOLOGIES · SOPORTE TI</p>");
            html.AppendLine("      <h1 class=\"titulo\">INFORME EJECUTIVO</h1>");
            html.AppendLine("      <p class=\"subtitulo\">Seguimiento de incidencias empresariales</p>");
            html.AppendLine("      <div class=\"linea-dorada\"></div>");
            html.AppendLine("      <div class=\"encabezado-pie\">");
            html.AppendLine("        <span class=\"operativo\">REPORTE OPERATIVO</span>");
            html.AppendLine("        <span class=\"fecha\">" + EscapeHtml(fechaGeneracion.ToString("dd/MM/yyyy · HH:mm")) + "</span>");
            html.AppendLine("      </div>");
            html.AppendLine("    </section>");
            html.AppendLine("    <section class=\"indicadores\">");
            html.AppendLine("      <article class=\"indicador\"><p class=\"indicador-valor\">" + total.ToString("00") + "</p><p class=\"indicador-etiqueta\">Total</p></article>");
            html.AppendLine("      <article class=\"indicador\"><p class=\"indicador-valor\">" + pendientes.ToString("00") + "</p><p class=\"indicador-etiqueta\">Pendientes</p></article>");
            html.AppendLine("      <article class=\"indicador\"><p class=\"indicador-valor\">" + enProceso.ToString("00") + "</p><p class=\"indicador-etiqueta\">En proceso</p></article>");
            html.AppendLine("      <article class=\"indicador\"><p class=\"indicador-valor\">" + resueltas.ToString("00") + "</p><p class=\"indicador-etiqueta\">Resueltas</p></article>");
            html.AppendLine("    </section>");
            html.AppendLine("    <section class=\"detalle\">");
            html.AppendLine("      <div class=\"detalle-cabecera\">");
            html.AppendLine("        <h2 class=\"detalle-titulo\">Detalle de incidencias</h2>");
            html.AppendLine("        <p class=\"detalle-conteo\">" + EscapeHtml(conteoRegistros) + "</p>");
            html.AppendLine("      </div>");
            html.AppendLine("      <div class=\"detalle-linea\"></div>");
            html.AppendLine("      <p class=\"resumen\">Total de incidencias incluidas: " + total + "</p>");

            if (total == 0)
            {
                html.AppendLine("      <section class=\"vacio\">No hay incidencias para mostrar en la vista previa del reporte.</section>");
            }
            else
            {
                html.AppendLine("      <section class=\"lista-incidencias\">");

                foreach (var incidencia in incidenciasReporte)
                {
                    html.AppendLine("        <article class=\"incidencia\">");
                    html.AppendLine("          <div>");
                    html.AppendLine("            <p class=\"incidencia-titulo\">#" + incidencia.Identificador + " — " + EscapeHtml(incidencia.Titulo) + "</p>");
                    html.AppendLine("            <p class=\"incidencia-meta\">" + EscapeHtml(NormalizarReportadoPor(incidencia.ReportadoPor)) + " · " + EscapeHtml(incidencia.Responsable) + " · Prioridad " + EscapeHtml(incidencia.Prioridad.ToString().ToLowerInvariant()) + "</p>");
                    html.AppendLine("            <p class=\"incidencia-fecha\">Fecha: " + EscapeHtml(incidencia.FechaCreacion.ToString("dd/MM/yyyy HH:mm")) + "</p>");
                    html.AppendLine("          </div>");
                    html.AppendLine("          <p class=\"incidencia-estado\">" + EscapeHtml(ObtenerTextoEstado(incidencia.Estado)) + "</p>");
                    html.AppendLine("        </article>");
                }

                html.AppendLine("      </section>");
                html.AppendLine("      <details class=\"detalle-tecnico\">");
                html.AppendLine("        <summary>Ver detalle técnico (tabla completa)</summary>");
                html.AppendLine("        <table>");
                html.AppendLine("          <thead>");
                html.AppendLine("            <tr>");
                html.AppendLine("              <th>ID</th>");
                html.AppendLine("              <th>Título</th>");
                html.AppendLine("              <th>Reportado por</th>");
                html.AppendLine("              <th>Responsable</th>");
                html.AppendLine("              <th>Prioridad</th>");
                html.AppendLine("              <th>Estado</th>");
                html.AppendLine("              <th>Fecha</th>");
                html.AppendLine("            </tr>");
                html.AppendLine("          </thead>");
                html.AppendLine("          <tbody>");

                foreach (var incidencia in incidenciasReporte)
                {
                    html.AppendLine("            <tr>");
                    html.AppendLine("              <td>" + incidencia.Identificador + "</td>");
                    html.AppendLine("              <td>" + EscapeHtml(incidencia.Titulo) + "</td>");
                    html.AppendLine("              <td>" + EscapeHtml(NormalizarReportadoPor(incidencia.ReportadoPor)) + "</td>");
                    html.AppendLine("              <td>" + EscapeHtml(incidencia.Responsable) + "</td>");
                    html.AppendLine("              <td>" + EscapeHtml(incidencia.Prioridad.ToString()) + "</td>");
                    html.AppendLine("              <td>" + EscapeHtml(ObtenerTextoEstado(incidencia.Estado)) + "</td>");
                    html.AppendLine("              <td>" + EscapeHtml(incidencia.FechaCreacion.ToString("dd/MM/yyyy HH:mm")) + "</td>");
                    html.AppendLine("            </tr>");
                }

                html.AppendLine("          </tbody>");
                html.AppendLine("        </table>");
                html.AppendLine("      </details>");
            }

            html.AppendLine("    </section>");
            html.AppendLine("  </div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
        }

        private static string EscapeHtml(string valor)
        {
            return WebUtility.HtmlEncode(valor ?? string.Empty);
        }

        private void lblSubtitulo_Click(object sender, EventArgs e)
        {

        }

        private void lblPendientesValor_Click(object sender, EventArgs e)
        {

        }
    }
}

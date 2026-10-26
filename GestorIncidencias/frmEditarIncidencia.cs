using System;
using System.Windows.Forms;
using GestorIncidencias.Entidades;

namespace GestorIncidencias
{
    public partial class frmEditarIncidencia : Form
    {
        private const string ReportadoPorLegacy = "No informado (registro legado)";

        public frmEditarIncidencia()
        {
            InitializeComponent();
            InicializarPrioridades();
            ConfigurarModoNuevo();

            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += BtnCancelar_Click;
        }

        public frmEditarIncidencia(Incidencia incidencia)
            : this()
        {
            ConfigurarModoEdicion(incidencia);
        }

        public string TituloIncidencia
        {
            get { return txtTitulo.Text; }
        }

        public string DescripcionIncidencia
        {
            get { return txtDescripcion.Text; }
        }

        public string ReportadoPor
        {
            get { return txtReportadoPor.Text; }
        }

        public string Responsable
        {
            get { return txtResponsable.Text; }
        }

        public Prioridad PrioridadSeleccionada
        {
            get
            {
                if (cboPrioridad.SelectedItem is Prioridad)
                {
                    return (Prioridad)cboPrioridad.SelectedItem;
                }

                return Prioridad.Media;
            }
        }

        private void InicializarPrioridades()
        {
            cboPrioridad.DataSource = Enum.GetValues(typeof(Prioridad));
            cboPrioridad.SelectedItem = Prioridad.Media;
        }

        private void ConfigurarModoNuevo()
        {
            lblTitulo.Text = "Nueva incidencia";
            Text = "XAMY Technologies - Nueva incidencia";
            txtTitulo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtReportadoPor.Text = string.Empty;
            txtResponsable.Text = string.Empty;
            cboPrioridad.SelectedItem = Prioridad.Media;
        }

        private void ConfigurarModoEdicion(Incidencia incidencia)
        {
            if (incidencia == null)
            {
                ConfigurarModoNuevo();
                return;
            }

            lblTitulo.Text = "Modificar incidencia";
            Text = "XAMY Technologies - Modificar incidencia";
            txtTitulo.Text = incidencia.Titulo ?? string.Empty;
            txtDescripcion.Text = incidencia.Descripcion ?? string.Empty;
            txtReportadoPor.Text = NormalizarReportadoPorParaEdicion(incidencia.ReportadoPor);
            txtResponsable.Text = incidencia.Responsable ?? string.Empty;
            cboPrioridad.SelectedItem = incidencia.Prioridad;
        }

        private static string NormalizarReportadoPorParaEdicion(string reportadoPor)
        {
            if (string.IsNullOrWhiteSpace(reportadoPor))
            {
                return ReportadoPorLegacy;
            }

            return reportadoPor;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;

namespace GestorIncidencias
{
    public partial class frmEditarIncidencia : Form
    {
        private const string ReportadoPorLegacy = "No informado (registro legado)";
        private readonly EmpleadoController controladorEmpleados;
        private readonly bool modoEdicion;
        private readonly Incidencia incidenciaOriginal;

        private List<Empleado> tecnicosElegibles;
        private bool catalogoTecnicosDisponible;
        private string mensajeErrorCatalogo;
        private int? responsableEmpleadoIdSeleccionado;
        private string responsableSeleccionado;

        public frmEditarIncidencia()
        {
            InitializeComponent();
            InicializarPrioridades();
            ConfigurarModoNuevo();

            tecnicosElegibles = new List<Empleado>();
            catalogoTecnicosDisponible = false;
            mensajeErrorCatalogo = string.Empty;

            if (!EsModoDiseno())
            {
                controladorEmpleados = new EmpleadoController(new XmlEmpleadoRepository());
            }

            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            Load += FrmEditarIncidencia_Load;
        }

        public frmEditarIncidencia(Incidencia incidencia)
            : this()
        {
            modoEdicion = true;
            incidenciaOriginal = ClonarIncidencia(incidencia);
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
            get { return responsableSeleccionado ?? string.Empty; }
        }

        public int? ResponsableEmpleadoId
        {
            get { return responsableEmpleadoIdSeleccionado; }
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
            responsableSeleccionado = string.Empty;
            responsableEmpleadoIdSeleccionado = null;
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
            responsableSeleccionado = incidencia.Responsable ?? string.Empty;
            responsableEmpleadoIdSeleccionado = incidencia.ResponsableEmpleadoId;
            cboPrioridad.SelectedItem = incidencia.Prioridad;
        }

        private void FrmEditarIncidencia_Load(object sender, EventArgs e)
        {
            CargarCatalogoTecnicos();
            InicializarSelectorResponsable();
        }

        private void CargarCatalogoTecnicos()
        {
            tecnicosElegibles = new List<Empleado>();
            catalogoTecnicosDisponible = false;
            mensajeErrorCatalogo = string.Empty;

            if (controladorEmpleados == null)
            {
                return;
            }

            var consulta = controladorEmpleados.Consultar();
            if (!consulta.Exito)
            {
                mensajeErrorCatalogo = consulta.Mensaje ?? "No se pudo cargar el catálogo de empleados.";
                return;
            }

            tecnicosElegibles = (consulta.Datos ?? new List<Empleado>())
                .Where(e => e != null && e.Activo && e.EsTecnicoSoporte)
                .OrderBy(e => e.NombreCompleto)
                .ThenBy(e => e.Identificador)
                .ToList();

            catalogoTecnicosDisponible = true;
        }

        private void InicializarSelectorResponsable()
        {
            cboResponsableTecnico.Items.Clear();

            if (!catalogoTecnicosDisponible)
            {
                cboResponsableTecnico.Enabled = false;

                if (modoEdicion && incidenciaOriginal != null)
                {
                    lblAvisoResponsable.Text =
                        "No se pudo cargar el catálogo de técnicos. Se conservará el responsable actual si no reasigna.";
                    lblResponsableActual.Text = string.Format(
                        "Responsable actual: {0}",
                        string.IsNullOrWhiteSpace(incidenciaOriginal.Responsable)
                            ? "(sin responsable)"
                            : incidenciaOriginal.Responsable);
                    return;
                }

                lblAvisoResponsable.Text =
                    string.IsNullOrWhiteSpace(mensajeErrorCatalogo)
                        ? "No se pudo cargar el catálogo de técnicos activos."
                        : mensajeErrorCatalogo;
                lblResponsableActual.Text = "No hay técnicos disponibles para asignación.";
                return;
            }

            foreach (var tecnico in tecnicosElegibles)
            {
                cboResponsableTecnico.Items.Add(new OpcionTecnicoResponsable(tecnico.Identificador, tecnico.NombreCompleto, false));
            }

            if (modoEdicion && incidenciaOriginal != null)
            {
                InicializarSelectorModoEdicion();
                return;
            }

            if (tecnicosElegibles.Count == 0)
            {
                cboResponsableTecnico.Enabled = false;
                lblAvisoResponsable.Text = "No hay técnicos activos disponibles para nuevas asignaciones.";
                lblResponsableActual.Text = "Debe registrar y activar técnicos de soporte para asignar responsable.";
                return;
            }

            cboResponsableTecnico.Enabled = true;
            cboResponsableTecnico.SelectedIndex = -1;
            lblAvisoResponsable.Text = "Seleccione un técnico activo para asignar el responsable.";
            lblResponsableActual.Text = "";
        }

        private void InicializarSelectorModoEdicion()
        {
            var responsableActual = string.IsNullOrWhiteSpace(incidenciaOriginal.Responsable)
                ? "(sin responsable)"
                : incidenciaOriginal.Responsable;

            lblResponsableActual.Text = "Responsable actual: " + responsableActual;

            if (tecnicosElegibles.Count == 0)
            {
                cboResponsableTecnico.Enabled = false;
                cboResponsableTecnico.Items.Add(
                    new OpcionTecnicoResponsable(incidenciaOriginal.ResponsableEmpleadoId, responsableActual, true));
                cboResponsableTecnico.SelectedIndex = 0;
                lblAvisoResponsable.Text = "No hay técnicos activos para reasignar. Se conservará el responsable actual.";
                return;
            }

            var tecnicoActual = incidenciaOriginal.ResponsableEmpleadoId.HasValue
                ? tecnicosElegibles.FirstOrDefault(t => t.Identificador == incidenciaOriginal.ResponsableEmpleadoId.Value)
                : null;

            if (tecnicoActual != null)
            {
                SeleccionarTecnicoPorId(tecnicoActual.Identificador);
                cboResponsableTecnico.Enabled = true;
                lblAvisoResponsable.Text = "Puede conservar o reasignar el técnico responsable.";
                return;
            }

            cboResponsableTecnico.Items.Insert(
                0,
                new OpcionTecnicoResponsable(incidenciaOriginal.ResponsableEmpleadoId, responsableActual, true));
            cboResponsableTecnico.SelectedIndex = 0;
            cboResponsableTecnico.Enabled = true;
            lblAvisoResponsable.Text =
                "El responsable actual no es elegible para nuevas asignaciones. Puede conservarlo o reasignar a un técnico activo.";
        }

        private void SeleccionarTecnicoPorId(int identificadorTecnico)
        {
            for (var i = 0; i < cboResponsableTecnico.Items.Count; i++)
            {
                var opcion = cboResponsableTecnico.Items[i] as OpcionTecnicoResponsable;
                if (opcion == null)
                {
                    continue;
                }

                if (opcion.IdentificadorEmpleado == identificadorTecnico)
                {
                    cboResponsableTecnico.SelectedIndex = i;
                    return;
                }
            }

            cboResponsableTecnico.SelectedIndex = -1;
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
            string responsable;
            int? responsableEmpleadoId;
            string error;

            if (!IntentarResolverResponsable(out responsable, out responsableEmpleadoId, out error))
            {
                MessageBox.Show(
                    this,
                    error,
                    "Validación de responsable",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            responsableSeleccionado = responsable;
            responsableEmpleadoIdSeleccionado = responsableEmpleadoId;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool IntentarResolverResponsable(out string responsable, out int? responsableEmpleadoId, out string error)
        {
            responsable = string.Empty;
            responsableEmpleadoId = null;
            error = string.Empty;

            if (!catalogoTecnicosDisponible)
            {
                if (modoEdicion && incidenciaOriginal != null)
                {
                    responsable = incidenciaOriginal.Responsable ?? string.Empty;
                    responsableEmpleadoId = incidenciaOriginal.ResponsableEmpleadoId;
                    return true;
                }

                error = string.IsNullOrWhiteSpace(mensajeErrorCatalogo)
                    ? "No fue posible cargar el catálogo de técnicos activos. Intente nuevamente."
                    : mensajeErrorCatalogo;
                return false;
            }

            var opcionSeleccionada = cboResponsableTecnico.SelectedItem as OpcionTecnicoResponsable;
            if (opcionSeleccionada == null)
            {
                if (modoEdicion && incidenciaOriginal != null && !string.IsNullOrWhiteSpace(incidenciaOriginal.Responsable))
                {
                    responsable = incidenciaOriginal.Responsable;
                    responsableEmpleadoId = incidenciaOriginal.ResponsableEmpleadoId;
                    return true;
                }

                error = "Debe seleccionar un técnico de soporte activo para asignar el responsable.";
                return false;
            }

            if (opcionSeleccionada.EsHistorico)
            {
                if (incidenciaOriginal == null)
                {
                    error = "No se pudo conservar el responsable histórico de la incidencia.";
                    return false;
                }

                responsable = incidenciaOriginal.Responsable ?? string.Empty;
                responsableEmpleadoId = incidenciaOriginal.ResponsableEmpleadoId;
                return true;
            }

            var validacionTecnicos = controladorEmpleados.Consultar();
            if (!validacionTecnicos.Exito)
            {
                error = validacionTecnicos.Mensaje ?? "No se pudo validar el técnico seleccionado.";
                return false;
            }

            var tecnicoVigente = (validacionTecnicos.Datos ?? new List<Empleado>())
                .FirstOrDefault(e =>
                    e != null &&
                    e.Identificador == opcionSeleccionada.IdentificadorEmpleado &&
                    e.Activo &&
                    e.EsTecnicoSoporte);

            if (tecnicoVigente == null)
            {
                error = "El técnico seleccionado ya no está activo o no está habilitado como soporte.";
                return false;
            }

            responsable = tecnicoVigente.NombreCompleto;
            responsableEmpleadoId = tecnicoVigente.Identificador;
            return true;
        }

        private static bool EsModoDiseno()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }

        private static Incidencia ClonarIncidencia(Incidencia incidencia)
        {
            if (incidencia == null)
            {
                return null;
            }

            return new Incidencia
            {
                Identificador = incidencia.Identificador,
                Titulo = incidencia.Titulo,
                Descripcion = incidencia.Descripcion,
                ReportadoPor = incidencia.ReportadoPor,
                Responsable = incidencia.Responsable,
                ResponsableEmpleadoId = incidencia.ResponsableEmpleadoId,
                Prioridad = incidencia.Prioridad,
                Estado = incidencia.Estado,
                FechaCreacion = incidencia.FechaCreacion
            };
        }

        private class OpcionTecnicoResponsable
        {
            public OpcionTecnicoResponsable(int? identificadorEmpleado, string nombre, bool esHistorico)
            {
                IdentificadorEmpleado = identificadorEmpleado;
                Nombre = nombre;
                EsHistorico = esHistorico;
            }

            public int? IdentificadorEmpleado { get; private set; }

            public string Nombre { get; private set; }

            public bool EsHistorico { get; private set; }

            public override string ToString()
            {
                if (EsHistorico)
                {
                    return "Conservar responsable actual: " + Nombre;
                }

                return Nombre;
            }
        }
    }
}

using System;
using System.Windows.Forms;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;

namespace GestorIncidencias
{
    public partial class frmEditarEmpleado : Form
    {
        private bool modoEdicion;
        private int identificadorEmpleado;
        private DateTime fechaCreacionEmpleado;

        public frmEditarEmpleado()
        {
            InitializeComponent();

            modoEdicion = false;
            identificadorEmpleado = 0;
            fechaCreacionEmpleado = DateTime.MinValue;

            ConfigurarModoNuevo();

            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            chkActivo.CheckedChanged += ChkActivo_CheckedChanged;
        }

        public frmEditarEmpleado(Empleado empleado)
            : this()
        {
            if (empleado == null)
            {
                return;
            }

            modoEdicion = true;
            identificadorEmpleado = empleado.Identificador;
            fechaCreacionEmpleado = empleado.FechaCreacion;

            ConfigurarModoEdicion(empleado);
        }

        public string NombreCompletoEmpleado
        {
            get { return (txtNombreCompleto.Text ?? string.Empty).Trim(); }
        }

        public bool EsTecnicoSoporteEmpleado
        {
            get { return chkEsTecnicoSoporte.Checked; }
        }

        public bool ActivoEmpleado
        {
            get { return chkActivo.Checked; }
        }

        public int IdentificadorEmpleado
        {
            get { return identificadorEmpleado; }
        }

        public DateTime FechaCreacionEmpleado
        {
            get { return fechaCreacionEmpleado; }
        }

        private void ConfigurarModoNuevo()
        {
            lblTitulo.Text = "REGISTRAR EMPLEADO";
            Text = "XAMY TECHNOLOGIES - Registrar empleado";

            txtNombreCompleto.Text = string.Empty;
            chkEsTecnicoSoporte.Checked = false;
            chkActivo.Checked = true;
            ActualizarTextoEstadoLaboral();
            lblMensajeValidacion.Text = "";
        }

        private void ConfigurarModoEdicion(Empleado empleado)
        {
            lblTitulo.Text = "MODIFICAR EMPLEADO";
            Text = "XAMY TECHNOLOGIES - Modificar empleado";

            txtNombreCompleto.Text = empleado.NombreCompleto ?? string.Empty;
            chkEsTecnicoSoporte.Checked = empleado.EsTecnicoSoporte;
            chkActivo.Checked = empleado.Activo;
            ActualizarTextoEstadoLaboral();
            lblMensajeValidacion.Text = "";
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            var validacion = ValidarEntradaFormulario();
            if (!validacion.Exito)
            {
                lblMensajeValidacion.Text = validacion.Mensaje ?? "Revise los datos del empleado.";
                txtNombreCompleto.Focus();
                txtNombreCompleto.SelectAll();
                return;
            }

            lblMensajeValidacion.Text = string.Empty;
            DialogResult = DialogResult.OK;
            Close();
        }

        private ResultadoOperacion<Empleado> ValidarEntradaFormulario()
        {
            var nombre = (txtNombreCompleto.Text ?? string.Empty).Trim();
            var idValidacion = modoEdicion && identificadorEmpleado > 0 ? identificadorEmpleado : 1;
            var fechaValidacion = modoEdicion && fechaCreacionEmpleado != DateTime.MinValue
                ? fechaCreacionEmpleado
                : DateTime.Now;

            var empleado = new Empleado(
                idValidacion,
                nombre,
                chkEsTecnicoSoporte.Checked,
                chkActivo.Checked,
                fechaValidacion);

            return new EmpleadoValidator().Validar(empleado);
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ChkActivo_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarTextoEstadoLaboral();
        }

        private void ActualizarTextoEstadoLaboral()
        {
            chkActivo.Text = chkActivo.Checked ? "Estado: Activo" : "Estado: Inactivo";
        }
    }
}

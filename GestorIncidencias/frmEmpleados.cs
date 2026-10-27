using System;
using System.Windows.Forms;

namespace GestorIncidencias
{
    public partial class frmEmpleados : Form
    {
        public frmEmpleados()
        {
            InitializeComponent();

            btnCerrar.Click += BtnCerrar_Click;
            btnBuscarEmpleado.Click += BtnAccionPendiente_Click;
            btnActualizar.Click += BtnAccionPendiente_Click;
            btnNuevoEmpleado.Click += BtnAccionPendiente_Click;
            btnModificarEmpleado.Click += BtnAccionPendiente_Click;
            btnActivarInactivar.Click += BtnAccionPendiente_Click;
            Load += FrmEmpleados_Load;
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            lblEstadoOperacion.Text = "Módulo de empleados preparado. Integración funcional pendiente en tareas siguientes.";
            lblEstadoVacio.Visible = dgvEmpleados.Rows.Count == 0;
            lblEstadoVacio.BringToFront();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAccionPendiente_Click(object sender, EventArgs e)
        {
            lblEstadoOperacion.Text = "Acción disponible cuando se complete la integración del módulo de empleados.";
        }
    }
}

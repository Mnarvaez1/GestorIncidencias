using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;

namespace GestorIncidencias
{
    public partial class frmEmpleados : Form
    {
        private EmpleadoController controlador;
        private List<Empleado> empleadosCargados;

        public frmEmpleados()
        {
            InitializeComponent();

            btnCerrar.Click += BtnCerrar_Click;
            btnBuscarEmpleado.Click += BtnBuscarEmpleado_Click;
            btnActualizar.Click += BtnActualizar_Click;
            btnNuevoEmpleado.Click += BtnNuevoEmpleado_Click;
            btnModificarEmpleado.Click += BtnModificarEmpleado_Click;
            btnActivarInactivar.Click += BtnActivarInactivar_Click;
            txtBuscarEmpleado.TextChanged += TxtBuscarEmpleado_TextChanged;
            txtBuscarEmpleado.KeyDown += TxtBuscarEmpleado_KeyDown;
            dgvEmpleados.SelectionChanged += DgvEmpleados_SelectionChanged;

            empleadosCargados = new List<Empleado>();

            if (!EsModoDiseno())
            {
                controlador = new EmpleadoController(new XmlEmpleadoRepository(), new XmlIncidenciaRepository());
            }

            Load += FrmEmpleados_Load;
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            InicializarColumnasGrilla();
            HabilitarControles(true);
            RecargarEmpleados();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            txtBuscarEmpleado.Text = string.Empty;
            RecargarEmpleados();
        }

        private void BtnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            BuscarEmpleados();
        }

        private void TxtBuscarEmpleado_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscarEmpleado.Text))
            {
                return;
            }

            RecargarEmpleados();
        }

        private void TxtBuscarEmpleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.Handled = true;
            e.SuppressKeyPress = true;
            BuscarEmpleados();
        }

        private void BtnNuevoEmpleado_Click(object sender, EventArgs e)
        {
            using (var formulario = new frmEditarEmpleado())
            {
                if (formulario.ShowDialog(this) != DialogResult.OK)
                {
                    lblEstadoOperacion.Text = "Registro de empleado cancelado por el operador.";
                    return;
                }

                var resultado = controlador.Registrar(
                    formulario.NombreCompletoEmpleado,
                    formulario.EsTecnicoSoporteEmpleado);

                if (!resultado.Exito)
                {
                    lblEstadoOperacion.Text = resultado.Mensaje ?? "No se pudo registrar el empleado.";
                    return;
                }

                RecargarEmpleados();
                SeleccionarEmpleadoPorId(resultado.Datos != null ? resultado.Datos.Identificador : 0);
                lblEstadoOperacion.Text = resultado.Mensaje ?? "Empleado registrado correctamente.";
            }
        }

        private void BtnModificarEmpleado_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerEmpleadoSeleccionado();
            if (seleccionado == null)
            {
                lblEstadoOperacion.Text = "Seleccione un empleado para modificar.";
                return;
            }

            using (var formulario = new frmEditarEmpleado(seleccionado))
            {
                if (formulario.ShowDialog(this) != DialogResult.OK)
                {
                    lblEstadoOperacion.Text = "Modificación de empleado cancelada por el operador.";
                    return;
                }

                var resultado = controlador.Editar(
                    seleccionado.Identificador,
                    formulario.NombreCompletoEmpleado,
                    formulario.EsTecnicoSoporteEmpleado,
                    formulario.ActivoEmpleado);

                if (!resultado.Exito)
                {
                    lblEstadoOperacion.Text = resultado.Mensaje ?? "No se pudo modificar el empleado.";
                    return;
                }

                RecargarEmpleados();
                SeleccionarEmpleadoPorId(seleccionado.Identificador);
                lblEstadoOperacion.Text = resultado.Mensaje ?? "Empleado modificado correctamente.";
            }
        }

        private void BtnActivarInactivar_Click(object sender, EventArgs e)
        {
            var seleccionado = ObtenerEmpleadoSeleccionado();
            if (seleccionado == null)
            {
                lblEstadoOperacion.Text = "Seleccione un empleado para activar o inactivar.";
                return;
            }

            var resultado = seleccionado.Activo
                ? controlador.Inactivar(seleccionado.Identificador)
                : controlador.Activar(seleccionado.Identificador);

            if (!resultado.Exito)
            {
                lblEstadoOperacion.Text = resultado.Mensaje ?? "No se pudo cambiar el estado del empleado.";
                return;
            }

            RecargarEmpleados();
            SeleccionarEmpleadoPorId(seleccionado.Identificador);
            lblEstadoOperacion.Text = resultado.Mensaje ?? "Estado laboral actualizado correctamente.";
        }

        private void DgvEmpleados_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarEstadoBotonesSegunSeleccion();
        }

        private void InicializarColumnasGrilla()
        {
            colIdentificador.DataPropertyName = string.Empty;
            colNombreCompleto.DataPropertyName = string.Empty;
            colTecnico.DataPropertyName = string.Empty;
            colEstadoLaboral.DataPropertyName = string.Empty;
            colDisponibilidad.DataPropertyName = string.Empty;
        }

        private void HabilitarControles(bool habilitar)
        {
            btnBuscarEmpleado.Enabled = habilitar;
            btnActualizar.Enabled = habilitar;
            btnNuevoEmpleado.Enabled = habilitar;

            if (!habilitar)
            {
                btnModificarEmpleado.Enabled = false;
                btnActivarInactivar.Enabled = false;
                return;
            }

            ActualizarEstadoBotonesSegunSeleccion();
        }

        private void RecargarEmpleados()
        {
            if (controlador == null)
            {
                return;
            }

            var consulta = controlador.Consultar();
            if (!consulta.Exito)
            {
                MostrarErrorOperacion(consulta.Mensaje ?? "No se pudieron cargar los empleados.");
                return;
            }

            var datos = consulta.Datos ?? new List<Empleado>();
            empleadosCargados = ClonarLista(datos);
            CargarFilas(empleadosCargados);
            lblEstadoOperacion.Text = consulta.Mensaje ?? "Consulta completada correctamente.";
        }

        private void BuscarEmpleados()
        {
            if (controlador == null)
            {
                return;
            }

            var resultado = controlador.Buscar(txtBuscarEmpleado.Text);
            if (!resultado.Exito)
            {
                MostrarErrorOperacion(resultado.Mensaje ?? "No se pudo realizar la búsqueda de empleados.");
                return;
            }

            var datos = resultado.Datos ?? new List<Empleado>();
            empleadosCargados = ClonarLista(datos);
            CargarFilas(empleadosCargados);
            lblEstadoOperacion.Text = resultado.Mensaje ?? "Consulta completada correctamente.";
        }

        private void CargarFilas(IList<Empleado> empleados)
        {
            dgvEmpleados.Rows.Clear();

            foreach (var empleado in empleados)
            {
                var indice = dgvEmpleados.Rows.Add(
                    empleado.Identificador,
                    empleado.NombreCompleto,
                    empleado.EsTecnicoSoporte ? "Sí" : "No",
                    empleado.Activo ? "Activo" : "Inactivo",
                    ObtenerDisponibilidadVisual(empleado));

                dgvEmpleados.Rows[indice].Tag = empleado;
            }

            lblEstadoVacio.Visible = dgvEmpleados.Rows.Count == 0;
            lblEstadoVacio.BringToFront();
            ActualizarEstadoBotonesSegunSeleccion();
        }

        private Empleado ObtenerEmpleadoSeleccionado()
        {
            if (dgvEmpleados.CurrentRow == null)
            {
                return null;
            }

            return dgvEmpleados.CurrentRow.Tag as Empleado;
        }

        private void SeleccionarEmpleadoPorId(int identificador)
        {
            if (identificador <= 0)
            {
                return;
            }

            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                var empleado = fila.Tag as Empleado;
                if (empleado == null || empleado.Identificador != identificador)
                {
                    continue;
                }

                fila.Selected = true;
                dgvEmpleados.CurrentCell = fila.Cells[0];
                return;
            }
        }

        private void ActualizarEstadoBotonesSegunSeleccion()
        {
            var empleado = ObtenerEmpleadoSeleccionado();
            var haySeleccion = empleado != null;

            btnModificarEmpleado.Enabled = haySeleccion;
            btnActivarInactivar.Enabled = haySeleccion;

            if (!haySeleccion)
            {
                btnActivarInactivar.Text = "Activar / Inactivar";
                return;
            }

            btnActivarInactivar.Text = empleado.Activo ? "Inactivar" : "Activar";
        }

        private void MostrarErrorOperacion(string mensaje)
        {
            lblEstadoOperacion.Text = mensaje;
            empleadosCargados = new List<Empleado>();
            dgvEmpleados.Rows.Clear();
            lblEstadoVacio.Visible = true;
            lblEstadoVacio.BringToFront();
            ActualizarEstadoBotonesSegunSeleccion();
        }

        private static List<Empleado> ClonarLista(IList<Empleado> empleados)
        {
            var origen = empleados ?? new List<Empleado>();
            var copia = new List<Empleado>(origen.Count);

            foreach (var empleado in origen)
            {
                if (empleado == null)
                {
                    continue;
                }

                copia.Add(new Empleado(
                    empleado.Identificador,
                    empleado.NombreCompleto,
                    empleado.EsTecnicoSoporte,
                    empleado.Activo,
                    empleado.FechaCreacion));
            }

            return copia;
        }

        private string ObtenerDisponibilidadVisual(Empleado empleado)
        {
            if (controlador == null || empleado == null)
            {
                return "No calculada";
            }

            var disponibilidad = controlador.ConsultarDisponibilidad(empleado.Identificador);
            if (!disponibilidad.Exito)
            {
                return "No calculada";
            }

            switch (disponibilidad.Datos)
            {
                case DisponibilidadOperativa.Libre:
                    return "Libre";
                case DisponibilidadOperativa.Ocupado:
                    return "Ocupado";
                default:
                    return "No calculada";
            }
        }

        private static bool EsModoDiseno()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }
    }
}

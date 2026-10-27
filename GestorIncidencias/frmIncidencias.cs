using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GestorIncidencias
{
    public partial class frmIncidencias : Form
    {
        private const string ReportadoPorLegacy = "No informado (registro legado)";
        private const string FiltroActivas = "Activas";
        private readonly IncidenciaController controlador;

        public frmIncidencias()
        {
            InitializeComponent();

            controlador = new IncidenciaController(new XmlIncidenciaRepository());

            btnNuevaIncidencia.Click += BtnNuevaIncidencia_Click;
            btnModificarIncidencia.Click += BtnModificarIncidencia_Click;
            btnCambiarEstado.Click += BtnCambiarEstado_Click;
            btnVistaPreviaReporte.Click += BtnVistaPreviaReporte_Click;
            btnAdministrarEmpleados.Click += BtnAdministrarEmpleados_Click;
            btnSalir.Click += BtnSalir_Click;
            txtBuscar.TextChanged += TxtBuscar_TextChanged;
            cboFiltroEstado.SelectedIndexChanged += CboFiltroEstado_SelectedIndexChanged;
            Load += FrmIncidencias_Load;
        }

        private void BtnAdministrarEmpleados_Click(object sender, EventArgs e)
        {
            using (var formulario = new frmEmpleados())
            {
                formulario.ShowDialog(this);
            }

            lblEstadoOperacion.Text = "Administración de empleados cerrada.";
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show(
                this,
                "¿Está seguro de que desea salir de XAMY Technologies?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirmacion != DialogResult.Yes)
            {
                return;
            }

            Close();
        }

        private void FrmIncidencias_Load(object sender, EventArgs e)
        {
            EstablecerFiltroPredeterminadoActivas();
            RecargarGrilla();
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            AplicarBusquedaYFiltro();
        }

        private void CboFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarBusquedaYFiltro();
        }

        private void BtnNuevaIncidencia_Click(object sender, EventArgs e)
        {
            using (var formulario = new frmEditarIncidencia())
            {
                var resultadoDialogo = formulario.ShowDialog(this);

                if (resultadoDialogo != DialogResult.OK)
                {
                    lblEstadoOperacion.Text = "Operación cancelada por el operador.";
                    return;
                }

                var registro = controlador.Registrar(
                    formulario.TituloIncidencia,
                    formulario.DescripcionIncidencia,
                    NormalizarReportadoPor(formulario.ReportadoPor),
                    formulario.Responsable,
                    formulario.PrioridadSeleccionada,
                    formulario.ResponsableEmpleadoId);

                if (!registro.Exito)
                {
                    lblEstadoOperacion.Text = registro.Mensaje ?? "No se pudo registrar la incidencia.";
                    return;
                }

                RecargarGrilla();
                SeleccionarFilaPorId(registro.Datos != null ? registro.Datos.Identificador : 0);
                lblEstadoOperacion.Text = registro.Mensaje ?? "Incidencia registrada correctamente.";
            }
        }

        private void BtnModificarIncidencia_Click(object sender, EventArgs e)
        {
            var incidenciaSeleccionada = ObtenerIncidenciaSeleccionada();

            if (incidenciaSeleccionada == null)
            {
                lblEstadoOperacion.Text = "Seleccione una incidencia para modificar.";
                return;
            }

            using (var formulario = new frmEditarIncidencia(incidenciaSeleccionada))
            {
                var resultadoDialogo = formulario.ShowDialog(this);

                if (resultadoDialogo != DialogResult.OK)
                {
                    lblEstadoOperacion.Text = "Operación cancelada por el operador.";
                    return;
                }

                var edicion = controlador.Editar(
                    incidenciaSeleccionada.Identificador,
                    formulario.TituloIncidencia,
                    formulario.DescripcionIncidencia,
                    NormalizarReportadoPor(formulario.ReportadoPor),
                    formulario.Responsable,
                    formulario.PrioridadSeleccionada,
                    formulario.ResponsableEmpleadoId);

                if (!edicion.Exito)
                {
                    lblEstadoOperacion.Text = edicion.Mensaje ?? "No se pudo modificar la incidencia.";
                    return;
                }

                RecargarGrilla();
                SeleccionarFilaPorId(incidenciaSeleccionada.Identificador);
                lblEstadoOperacion.Text = edicion.Mensaje ?? "Incidencia editada correctamente.";
            }
        }

        private void BtnCambiarEstado_Click(object sender, EventArgs e)
        {
            var incidenciaSeleccionada = ObtenerIncidenciaSeleccionada();

            if (incidenciaSeleccionada == null)
            {
                lblEstadoOperacion.Text = "Seleccione una incidencia para cambiar el estado.";
                return;
            }

            EstadoIncidencia estadoDestino;
            var confirmaCambio = IntentarSeleccionarEstadoDestino(incidenciaSeleccionada.Estado, out estadoDestino);

            if (!confirmaCambio)
            {
                lblEstadoOperacion.Text = "Cambio de estado cancelado por el operador.";
                return;
            }

            var cambio = controlador.CambiarEstado(incidenciaSeleccionada.Identificador, estadoDestino);

            if (!cambio.Exito)
            {
                lblEstadoOperacion.Text = cambio.Mensaje ?? "No se pudo cambiar el estado de la incidencia.";
                return;
            }

            RecargarGrilla();
            SeleccionarFilaPorId(incidenciaSeleccionada.Identificador);
            lblEstadoOperacion.Text = cambio.Mensaje ?? "Estado de incidencia actualizado correctamente.";
        }

        private void BtnVistaPreviaReporte_Click(object sender, EventArgs e)
        {
            var incidenciasVisibles = ObtenerIncidenciasVisiblesEnGrilla();

            using (var vistaPrevia = new frmVistaPreviaReporte(incidenciasVisibles))
            {
                vistaPrevia.ShowDialog(this);
            }
        }

        private bool IntentarSeleccionarEstadoDestino(EstadoIncidencia estadoActual, out EstadoIncidencia estadoDestino)
        {
            var opciones = ObtenerOpcionesEstado(estadoActual);
            var selector = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = opciones,
                DisplayMember = "Texto",
                ValueMember = "Valor",
                Left = 20,
                Top = 20,
                Width = 260
            };

            var btnAceptar = new Button
            {
                Text = "Aceptar",
                DialogResult = DialogResult.OK,
                Left = 120,
                Top = 60,
                Width = 80
            };

            var btnCancelar = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Left = 205,
                Top = 60,
                Width = 80
            };

            using (var dialogo = new Form())
            {
                dialogo.Text = "Cambiar estado";
                dialogo.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialogo.StartPosition = FormStartPosition.CenterParent;
                dialogo.MinimizeBox = false;
                dialogo.MaximizeBox = false;
                dialogo.ShowInTaskbar = false;
                dialogo.ClientSize = new System.Drawing.Size(305, 105);
                dialogo.Controls.Add(selector);
                dialogo.Controls.Add(btnAceptar);
                dialogo.Controls.Add(btnCancelar);
                dialogo.AcceptButton = btnAceptar;
                dialogo.CancelButton = btnCancelar;

                if (dialogo.ShowDialog(this) != DialogResult.OK || selector.SelectedValue == null)
                {
                    estadoDestino = estadoActual;
                    return false;
                }

                estadoDestino = (EstadoIncidencia)selector.SelectedValue;
                return true;
            }
        }

        private static List<OpcionEstado> ObtenerOpcionesEstado(EstadoIncidencia estadoActual)
        {
            var opciones = new List<OpcionEstado>
            {
                new OpcionEstado(estadoActual, ObtenerTextoEstado(estadoActual) + " (sin cambios)")
            };

            if (estadoActual == EstadoIncidencia.Pendiente)
            {
                opciones.Add(new OpcionEstado(EstadoIncidencia.EnProceso, ObtenerTextoEstado(EstadoIncidencia.EnProceso)));
            }
            else if (estadoActual == EstadoIncidencia.EnProceso)
            {
                opciones.Add(new OpcionEstado(EstadoIncidencia.Resuelta, ObtenerTextoEstado(EstadoIncidencia.Resuelta)));
            }
            else if (estadoActual == EstadoIncidencia.Resuelta)
            {
                opciones.Add(new OpcionEstado(EstadoIncidencia.EnProceso, ObtenerTextoEstado(EstadoIncidencia.EnProceso)));
            }

            return opciones;
        }

        private static string ObtenerTextoEstado(EstadoIncidencia estado)
        {
            if (estado == EstadoIncidencia.EnProceso)
            {
                return "En proceso";
            }

            return estado.ToString();
        }

        private void RecargarGrilla()
        {
            AplicarBusquedaYFiltro();
        }

        private void AplicarBusquedaYFiltro()
        {
            var consulta = controlador.Consultar();

            if (!consulta.Exito)
            {
                lblEstadoOperacion.Text = consulta.Mensaje ?? "No se pudieron cargar las incidencias.";
                return;
            }

            var incidencias = consulta.Datos ?? new List<Incidencia>();
            var texto = (txtBuscar.Text ?? string.Empty).Trim();
            var filtroEstado = ObtenerFiltroEstadoSeleccionado();
            var filtroActivas = EsFiltroActivasSeleccionado();
            var tieneTexto = texto.Length > 0;


            var filtradas = incidencias
                .Where(i =>
                    CumpleFiltroEstado(i, filtroEstado, filtroActivas) &&
                    (!tieneTexto ||
                     ContieneTexto(i.Titulo, texto) ||
                     ContieneTexto(i.ReportadoPor, texto) ||
                     ContieneTexto(i.Responsable, texto)))
                .ToList();


            CargarFilas(filtradas);


            if (filtradas.Count == 0)
            {
                if (!tieneTexto && !filtroEstado.HasValue && !filtroActivas)
                {
                    lblEstadoOperacion.Text = "No hay incidencias registradas.";
                }
                else
                {
                    lblEstadoOperacion.Text = "No se encontraron incidencias para los criterios indicados.";
                }

                return;
            }


            if (tieneTexto || filtroEstado.HasValue || filtroActivas)
            {
                lblEstadoOperacion.Text = "Consulta completada correctamente.";
                return;
            }

            lblEstadoOperacion.Text = consulta.Mensaje;
        }

        private void CargarFilas(List<Incidencia> incidencias)
        {
            dgvIncidencias.Rows.Clear();

            foreach (var incidencia in incidencias)
            {
                var indice = dgvIncidencias.Rows.Add(
                    incidencia.Identificador,
                    incidencia.Titulo,
                    incidencia.Descripcion,
                    incidencia.ReportadoPor,
                    incidencia.Responsable,
                    incidencia.Prioridad,
                    incidencia.Estado,
                    incidencia.FechaCreacion);

                dgvIncidencias.Rows[indice].Tag = incidencia;
            }
        }

        private EstadoIncidencia? ObtenerFiltroEstadoSeleccionado()
        {
            var seleccionado = cboFiltroEstado.SelectedItem as string;

            if (string.IsNullOrWhiteSpace(seleccionado) ||
                string.Equals(seleccionado, FiltroActivas, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(seleccionado, "Todos", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            if (string.Equals(seleccionado, "En proceso", StringComparison.OrdinalIgnoreCase))
            {
                return EstadoIncidencia.EnProceso;
            }

            EstadoIncidencia estado;
            if (Enum.TryParse(seleccionado, true, out estado))
            {
                return estado;
            }

            return null;
        }

        private bool EsFiltroActivasSeleccionado()
        {
            var seleccionado = cboFiltroEstado.SelectedItem as string;
            return string.Equals(seleccionado, FiltroActivas, StringComparison.OrdinalIgnoreCase);
        }

        private static bool CumpleFiltroEstado(
            Incidencia incidencia,
            EstadoIncidencia? filtroEstado,
            bool filtroActivas)
        {
            if (incidencia == null)
            {
                return false;
            }

            if (filtroActivas)
            {
                return incidencia.Estado == EstadoIncidencia.Pendiente ||
                       incidencia.Estado == EstadoIncidencia.EnProceso;
            }

            return !filtroEstado.HasValue || incidencia.Estado == filtroEstado.Value;
        }

        private void EstablecerFiltroPredeterminadoActivas()
        {
            if (cboFiltroEstado.Items.Count == 0)
            {
                return;
            }

            for (var i = 0; i < cboFiltroEstado.Items.Count; i++)
            {
                var opcion = cboFiltroEstado.Items[i] as string;
                if (!string.Equals(opcion, FiltroActivas, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                cboFiltroEstado.SelectedIndex = i;
                return;
            }

            cboFiltroEstado.SelectedIndex = 0;
        }

        private static bool ContieneTexto(string valor, string texto)
        {
            if (string.IsNullOrEmpty(valor) || string.IsNullOrEmpty(texto))
            {
                return false;
            }

            return valor.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private Incidencia ObtenerIncidenciaSeleccionada()
        {
            if (dgvIncidencias.CurrentRow == null)
            {
                return null;
            }

            return dgvIncidencias.CurrentRow.Tag as Incidencia;
        }

        private List<Incidencia> ObtenerIncidenciasVisiblesEnGrilla()
        {
            var visibles = new List<Incidencia>();

            foreach (DataGridViewRow fila in dgvIncidencias.Rows)
            {
                var incidencia = fila.Tag as Incidencia;

                if (incidencia == null)
                {
                    continue;
                }

                visibles.Add(new Incidencia
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
                });
            }

            return visibles;
        }

        private void SeleccionarFilaPorId(int identificador)
        {
            if (identificador <= 0)
            {
                return;
            }

            foreach (DataGridViewRow fila in dgvIncidencias.Rows)
            {
                var incidencia = fila.Tag as Incidencia;

                if (incidencia != null && incidencia.Identificador == identificador)
                {
                    fila.Selected = true;
                    dgvIncidencias.CurrentCell = fila.Cells[0];
                    return;
                }
            }
        }

        private static string NormalizarReportadoPor(string reportadoPor)
        {
            if (string.IsNullOrWhiteSpace(reportadoPor))
            {
                return reportadoPor;
            }

            var recortado = reportadoPor.Trim();

            if (string.Equals(recortado, ReportadoPorLegacy, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return recortado;
        }

        private class OpcionEstado
        {
            public OpcionEstado(EstadoIncidencia valor, string texto)
            {
                Valor = valor;
                Texto = texto;
            }

            public EstadoIncidencia Valor { get; private set; }

            public string Texto { get; private set; }
        }
    }
}

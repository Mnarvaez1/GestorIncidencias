using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestorIncidencias.Controlador;
using GestorIncidencias.Entidades;

namespace GestorIncidencias
{
    public partial class frmIncidencias : Form
    {
        private const string ReportadoPorLegacy = "No informado (registro legado)";
        private readonly IncidenciaController controlador;

        public frmIncidencias()
        {
            InitializeComponent();

            controlador = new IncidenciaController(new XmlIncidenciaRepository());

            btnNuevaIncidencia.Click += BtnNuevaIncidencia_Click;
            btnModificarIncidencia.Click += BtnModificarIncidencia_Click;
            btnCambiarEstado.Click += BtnCambiarEstado_Click;
            Load += FrmIncidencias_Load;
        }

        private void FrmIncidencias_Load(object sender, EventArgs e)
        {
            RecargarGrilla();
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
                    formulario.PrioridadSeleccionada);

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
                    formulario.PrioridadSeleccionada);

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
            var consulta = controlador.Consultar();

            if (!consulta.Exito)
            {
                lblEstadoOperacion.Text = consulta.Mensaje ?? "No se pudieron cargar las incidencias.";
                return;
            }

            dgvIncidencias.Rows.Clear();

            foreach (var incidencia in consulta.Datos)
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

            lblEstadoOperacion.Text = consulta.Mensaje;
        }

        private Incidencia ObtenerIncidenciaSeleccionada()
        {
            if (dgvIncidencias.CurrentRow == null)
            {
                return null;
            }

            return dgvIncidencias.CurrentRow.Tag as Incidencia;
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

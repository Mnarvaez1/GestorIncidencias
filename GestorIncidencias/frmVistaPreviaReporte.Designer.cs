namespace GestorIncidencias
{
    partial class frmVistaPreviaReporte
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblFechaGeneracion = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlResumen = new System.Windows.Forms.Panel();
            this.lblResumen = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.lblEstadoVacio = new System.Windows.Forms.Label();
            this.dgvIncidenciasReporte = new System.Windows.Forms.DataGridView();
            this.colIdentificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReportadoPor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResponsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrioridad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.pnlEncabezado.SuspendLayout();
            this.pnlResumen.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidenciasReporte)).BeginInit();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlEncabezado
            //
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(45)))), ((int)(((byte)(74)))));
            this.pnlEncabezado.Controls.Add(this.lblFechaGeneracion);
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 14, 24, 14);
            this.pnlEncabezado.Size = new System.Drawing.Size(1180, 110);
            this.pnlEncabezado.TabIndex = 0;
            //
            // lblFechaGeneracion
            //
            this.lblFechaGeneracion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFechaGeneracion.ForeColor = System.Drawing.Color.White;
            this.lblFechaGeneracion.Location = new System.Drawing.Point(748, 42);
            this.lblFechaGeneracion.Name = "lblFechaGeneracion";
            this.lblFechaGeneracion.Size = new System.Drawing.Size(400, 25);
            this.lblFechaGeneracion.TabIndex = 2;
            this.lblFechaGeneracion.Text = "Generado: --/--/---- --:--";
            this.lblFechaGeneracion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(223)))), ((int)(((byte)(242)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 58);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(298, 25);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "GestorIncidencias - Reporte operativo";
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(24, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(366, 45);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "VISTA PREVIA DE REPORTE";
            //
            // pnlResumen
            //
            this.pnlResumen.BackColor = System.Drawing.Color.White;
            this.pnlResumen.Controls.Add(this.lblResumen);
            this.pnlResumen.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResumen.Location = new System.Drawing.Point(0, 110);
            this.pnlResumen.Name = "pnlResumen";
            this.pnlResumen.Padding = new System.Windows.Forms.Padding(24, 10, 24, 10);
            this.pnlResumen.Size = new System.Drawing.Size(1180, 52);
            this.pnlResumen.TabIndex = 1;
            //
            // lblResumen
            //
            this.lblResumen.AutoSize = true;
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResumen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(45)))), ((int)(((byte)(74)))));
            this.lblResumen.Location = new System.Drawing.Point(28, 14);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(243, 25);
            this.lblResumen.TabIndex = 0;
            this.lblResumen.Text = "Total de incidencias incluidas: 0";
            //
            // pnlContenido
            //
            this.pnlContenido.BackColor = System.Drawing.Color.White;
            this.pnlContenido.Controls.Add(this.lblEstadoVacio);
            this.pnlContenido.Controls.Add(this.dgvIncidenciasReporte);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 162);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 0, 24, 16);
            this.pnlContenido.Size = new System.Drawing.Size(1180, 466);
            this.pnlContenido.TabIndex = 2;
            //
            // lblEstadoVacio
            //
            this.lblEstadoVacio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstadoVacio.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoVacio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(112)))), ((int)(((byte)(128)))));
            this.lblEstadoVacio.Location = new System.Drawing.Point(24, 0);
            this.lblEstadoVacio.Name = "lblEstadoVacio";
            this.lblEstadoVacio.Size = new System.Drawing.Size(1132, 450);
            this.lblEstadoVacio.TabIndex = 1;
            this.lblEstadoVacio.Text = "No hay incidencias para mostrar en la vista previa del reporte.";
            this.lblEstadoVacio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEstadoVacio.Visible = false;
            //
            // dgvIncidenciasReporte
            //
            this.dgvIncidenciasReporte.AllowUserToAddRows = false;
            this.dgvIncidenciasReporte.AllowUserToDeleteRows = false;
            this.dgvIncidenciasReporte.AllowUserToResizeRows = false;
            this.dgvIncidenciasReporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIncidenciasReporte.BackgroundColor = System.Drawing.Color.White;
            this.dgvIncidenciasReporte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvIncidenciasReporte.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(59)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(59)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIncidenciasReporte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvIncidenciasReporte.ColumnHeadersHeight = 36;
            this.dgvIncidenciasReporte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdentificador,
            this.colTitulo,
            this.colReportadoPor,
            this.colResponsable,
            this.colPrioridad,
            this.colEstado,
            this.colFechaCreacion});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(45)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(232)))), ((int)(((byte)(248)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(45)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIncidenciasReporte.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvIncidenciasReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIncidenciasReporte.EnableHeadersVisualStyles = false;
            this.dgvIncidenciasReporte.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(232)))));
            this.dgvIncidenciasReporte.Location = new System.Drawing.Point(24, 0);
            this.dgvIncidenciasReporte.MultiSelect = false;
            this.dgvIncidenciasReporte.Name = "dgvIncidenciasReporte";
            this.dgvIncidenciasReporte.ReadOnly = true;
            this.dgvIncidenciasReporte.RowHeadersVisible = false;
            this.dgvIncidenciasReporte.RowTemplate.Height = 28;
            this.dgvIncidenciasReporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIncidenciasReporte.Size = new System.Drawing.Size(1132, 450);
            this.dgvIncidenciasReporte.TabIndex = 0;
            //
            // colIdentificador
            //
            this.colIdentificador.FillWeight = 60F;
            this.colIdentificador.HeaderText = "ID";
            this.colIdentificador.MinimumWidth = 8;
            this.colIdentificador.Name = "colIdentificador";
            this.colIdentificador.ReadOnly = true;
            //
            // colTitulo
            //
            this.colTitulo.FillWeight = 170F;
            this.colTitulo.HeaderText = "Título";
            this.colTitulo.MinimumWidth = 8;
            this.colTitulo.Name = "colTitulo";
            this.colTitulo.ReadOnly = true;
            //
            // colReportadoPor
            //
            this.colReportadoPor.FillWeight = 130F;
            this.colReportadoPor.HeaderText = "Reportado por";
            this.colReportadoPor.MinimumWidth = 8;
            this.colReportadoPor.Name = "colReportadoPor";
            this.colReportadoPor.ReadOnly = true;
            //
            // colResponsable
            //
            this.colResponsable.FillWeight = 130F;
            this.colResponsable.HeaderText = "Responsable";
            this.colResponsable.MinimumWidth = 8;
            this.colResponsable.Name = "colResponsable";
            this.colResponsable.ReadOnly = true;
            //
            // colPrioridad
            //
            this.colPrioridad.FillWeight = 90F;
            this.colPrioridad.HeaderText = "Prioridad";
            this.colPrioridad.MinimumWidth = 8;
            this.colPrioridad.Name = "colPrioridad";
            this.colPrioridad.ReadOnly = true;
            //
            // colEstado
            //
            this.colEstado.FillWeight = 95F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 8;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            //
            // colFechaCreacion
            //
            this.colFechaCreacion.FillWeight = 120F;
            this.colFechaCreacion.HeaderText = "Fecha";
            this.colFechaCreacion.MinimumWidth = 8;
            this.colFechaCreacion.Name = "colFechaCreacion";
            this.colFechaCreacion.ReadOnly = true;
            //
            // pnlAcciones
            //
            this.pnlAcciones.BackColor = System.Drawing.Color.White;
            this.pnlAcciones.Controls.Add(this.btnCerrar);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(0, 628);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Padding = new System.Windows.Forms.Padding(24, 10, 24, 14);
            this.pnlAcciones.Size = new System.Drawing.Size(1180, 72);
            this.pnlAcciones.TabIndex = 3;
            //
            // btnCerrar
            //
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(59)))), ((int)(((byte)(92)))));
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(1024, 16);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(124, 38);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            //
            // frmVistaPreviaReporte
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1180, 700);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.pnlResumen);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(980, 600);
            this.Name = "frmVistaPreviaReporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GestorIncidencias - Vista previa de reporte";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlResumen.ResumeLayout(false);
            this.pnlResumen.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidenciasReporte)).EndInit();
            this.pnlAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblFechaGeneracion;
        private System.Windows.Forms.Panel pnlResumen;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.DataGridView dgvIncidenciasReporte;
        private System.Windows.Forms.Label lblEstadoVacio;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdentificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReportadoPor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResponsable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrioridad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaCreacion;
    }
}

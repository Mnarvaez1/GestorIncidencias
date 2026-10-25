namespace GestorIncidencias
{
    partial class frmIncidencias
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTituloApp = new System.Windows.Forms.Label();
            this.pnlAdministracion = new System.Windows.Forms.Panel();
            this.cboFiltroEstado = new System.Windows.Forms.ComboBox();
            this.lblFiltroEstado = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.lblTituloAdministracion = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.dgvIncidencias = new System.Windows.Forms.DataGridView();
            this.colIdentificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResponsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrioridad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.btnCambiarEstado = new System.Windows.Forms.Button();
            this.btnModificarIncidencia = new System.Windows.Forms.Button();
            this.btnNuevaIncidencia = new System.Windows.Forms.Button();
            this.pnlEstado = new System.Windows.Forms.Panel();
            this.lblEstadoOperacion = new System.Windows.Forms.Label();
            this.pnlEncabezado.SuspendLayout();
            this.pnlAdministracion.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidencias)).BeginInit();
            this.pnlAcciones.SuspendLayout();
            this.pnlEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(45)))), ((int)(((byte)(74)))));
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTituloApp);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.pnlEncabezado.Size = new System.Drawing.Size(1280, 92);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(223)))), ((int)(((byte)(242)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 52);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(291, 25);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Sistema de Soporte Técnico Empresarial";
            // 
            // lblTituloApp
            // 
            this.lblTituloApp.AutoSize = true;
            this.lblTituloApp.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloApp.ForeColor = System.Drawing.Color.White;
            this.lblTituloApp.Location = new System.Drawing.Point(24, 12);
            this.lblTituloApp.Name = "lblTituloApp";
            this.lblTituloApp.Size = new System.Drawing.Size(301, 45);
            this.lblTituloApp.TabIndex = 0;
            this.lblTituloApp.Text = "GESTORINCIDENCIAS";
            // 
            // pnlAdministracion
            // 
            this.pnlAdministracion.BackColor = System.Drawing.Color.White;
            this.pnlAdministracion.Controls.Add(this.cboFiltroEstado);
            this.pnlAdministracion.Controls.Add(this.lblFiltroEstado);
            this.pnlAdministracion.Controls.Add(this.txtBuscar);
            this.pnlAdministracion.Controls.Add(this.lblBuscar);
            this.pnlAdministracion.Controls.Add(this.lblTituloAdministracion);
            this.pnlAdministracion.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAdministracion.Location = new System.Drawing.Point(0, 92);
            this.pnlAdministracion.Name = "pnlAdministracion";
            this.pnlAdministracion.Padding = new System.Windows.Forms.Padding(24, 16, 24, 12);
            this.pnlAdministracion.Size = new System.Drawing.Size(1280, 126);
            this.pnlAdministracion.TabIndex = 1;
            // 
            // cboFiltroEstado
            // 
            this.cboFiltroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltroEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFiltroEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFiltroEstado.FormattingEnabled = true;
            this.cboFiltroEstado.Items.AddRange(new object[] {
            "Todos",
            "Pendiente",
            "En proceso",
            "Resuelta"});
            this.cboFiltroEstado.Location = new System.Drawing.Point(914, 66);
            this.cboFiltroEstado.Name = "cboFiltroEstado";
            this.cboFiltroEstado.Size = new System.Drawing.Size(334, 33);
            this.cboFiltroEstado.TabIndex = 4;
            // 
            // lblFiltroEstado
            // 
            this.lblFiltroEstado.AutoSize = true;
            this.lblFiltroEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltroEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(80)))), ((int)(((byte)(104)))));
            this.lblFiltroEstado.Location = new System.Drawing.Point(909, 37);
            this.lblFiltroEstado.Name = "lblFiltroEstado";
            this.lblFiltroEstado.Size = new System.Drawing.Size(121, 25);
            this.lblFiltroEstado.TabIndex = 3;
            this.lblFiltroEstado.Text = "Filtrar estado:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(27, 66);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(870, 31);
            this.txtBuscar.TabIndex = 2;
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(80)))), ((int)(((byte)(104)))));
            this.lblBuscar.Location = new System.Drawing.Point(22, 37);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(266, 25);
            this.lblBuscar.TabIndex = 1;
            this.lblBuscar.Text = "Buscar por título o responsable:";
            // 
            // lblTituloAdministracion
            // 
            this.lblTituloAdministracion.AutoSize = true;
            this.lblTituloAdministracion.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloAdministracion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(59)))), ((int)(((byte)(92)))));
            this.lblTituloAdministracion.Location = new System.Drawing.Point(22, 8);
            this.lblTituloAdministracion.Name = "lblTituloAdministracion";
            this.lblTituloAdministracion.Size = new System.Drawing.Size(248, 28);
            this.lblTituloAdministracion.TabIndex = 0;
            this.lblTituloAdministracion.Text = "Administración de incidencias";
            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlContenido.Controls.Add(this.dgvIncidencias);
            this.pnlContenido.Controls.Add(this.pnlAcciones);
            this.pnlContenido.Controls.Add(this.pnlEstado);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 218);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 0, 24, 24);
            this.pnlContenido.Size = new System.Drawing.Size(1280, 582);
            this.pnlContenido.TabIndex = 2;
            // 
            // dgvIncidencias
            // 
            this.dgvIncidencias.AllowUserToAddRows = false;
            this.dgvIncidencias.AllowUserToDeleteRows = false;
            this.dgvIncidencias.AllowUserToResizeRows = false;
            this.dgvIncidencias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIncidencias.BackgroundColor = System.Drawing.Color.White;
            this.dgvIncidencias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvIncidencias.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(59)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(59)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIncidencias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvIncidencias.ColumnHeadersHeight = 38;
            this.dgvIncidencias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdentificador,
            this.colTitulo,
            this.colDescripcion,
            this.colResponsable,
            this.colPrioridad,
            this.colEstado,
            this.colFechaCreacion});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(232)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(59)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIncidencias.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvIncidencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIncidencias.EnableHeadersVisualStyles = false;
            this.dgvIncidencias.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.dgvIncidencias.Location = new System.Drawing.Point(24, 0);
            this.dgvIncidencias.MultiSelect = false;
            this.dgvIncidencias.Name = "dgvIncidencias";
            this.dgvIncidencias.ReadOnly = true;
            this.dgvIncidencias.RowHeadersVisible = false;
            this.dgvIncidencias.RowHeadersWidth = 62;
            this.dgvIncidencias.RowTemplate.Height = 30;
            this.dgvIncidencias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIncidencias.Size = new System.Drawing.Size(1232, 456);
            this.dgvIncidencias.TabIndex = 0;
            // 
            // colIdentificador
            // 
            this.colIdentificador.FillWeight = 60F;
            this.colIdentificador.HeaderText = "Identificador";
            this.colIdentificador.MinimumWidth = 8;
            this.colIdentificador.Name = "colIdentificador";
            this.colIdentificador.ReadOnly = true;
            // 
            // colTitulo
            // 
            this.colTitulo.FillWeight = 120F;
            this.colTitulo.HeaderText = "Título";
            this.colTitulo.MinimumWidth = 8;
            this.colTitulo.Name = "colTitulo";
            this.colTitulo.ReadOnly = true;
            // 
            // colDescripcion
            // 
            this.colDescripcion.FillWeight = 180F;
            this.colDescripcion.HeaderText = "Descripción";
            this.colDescripcion.MinimumWidth = 8;
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.ReadOnly = true;
            // 
            // colResponsable
            // 
            this.colResponsable.FillWeight = 95F;
            this.colResponsable.HeaderText = "Responsable";
            this.colResponsable.MinimumWidth = 8;
            this.colResponsable.Name = "colResponsable";
            this.colResponsable.ReadOnly = true;
            // 
            // colPrioridad
            // 
            this.colPrioridad.FillWeight = 75F;
            this.colPrioridad.HeaderText = "Prioridad";
            this.colPrioridad.MinimumWidth = 8;
            this.colPrioridad.Name = "colPrioridad";
            this.colPrioridad.ReadOnly = true;
            // 
            // colEstado
            // 
            this.colEstado.FillWeight = 90F;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 8;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            // 
            // colFechaCreacion
            // 
            this.colFechaCreacion.FillWeight = 115F;
            this.colFechaCreacion.HeaderText = "Fecha de creación";
            this.colFechaCreacion.MinimumWidth = 8;
            this.colFechaCreacion.Name = "colFechaCreacion";
            this.colFechaCreacion.ReadOnly = true;
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.Controls.Add(this.btnCambiarEstado);
            this.pnlAcciones.Controls.Add(this.btnModificarIncidencia);
            this.pnlAcciones.Controls.Add(this.btnNuevaIncidencia);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(24, 456);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Padding = new System.Windows.Forms.Padding(0, 16, 0, 8);
            this.pnlAcciones.Size = new System.Drawing.Size(1232, 58);
            this.pnlAcciones.TabIndex = 1;
            // 
            // btnCambiarEstado
            // 
            this.btnCambiarEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.btnCambiarEstado.FlatAppearance.BorderSize = 0;
            this.btnCambiarEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarEstado.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiarEstado.ForeColor = System.Drawing.Color.White;
            this.btnCambiarEstado.Location = new System.Drawing.Point(402, 16);
            this.btnCambiarEstado.Name = "btnCambiarEstado";
            this.btnCambiarEstado.Size = new System.Drawing.Size(190, 36);
            this.btnCambiarEstado.TabIndex = 2;
            this.btnCambiarEstado.Text = "Cambiar estado";
            this.btnCambiarEstado.UseVisualStyleBackColor = false;
            // 
            // btnModificarIncidencia
            // 
            this.btnModificarIncidencia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(86)))), ((int)(((byte)(179)))));
            this.btnModificarIncidencia.FlatAppearance.BorderSize = 0;
            this.btnModificarIncidencia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarIncidencia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarIncidencia.ForeColor = System.Drawing.Color.White;
            this.btnModificarIncidencia.Location = new System.Drawing.Point(206, 16);
            this.btnModificarIncidencia.Name = "btnModificarIncidencia";
            this.btnModificarIncidencia.Size = new System.Drawing.Size(190, 36);
            this.btnModificarIncidencia.TabIndex = 1;
            this.btnModificarIncidencia.Text = "Modificar incidencia";
            this.btnModificarIncidencia.UseVisualStyleBackColor = false;
            // 
            // btnNuevaIncidencia
            // 
            this.btnNuevaIncidencia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(122)))), ((int)(((byte)(73)))));
            this.btnNuevaIncidencia.FlatAppearance.BorderSize = 0;
            this.btnNuevaIncidencia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaIncidencia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevaIncidencia.ForeColor = System.Drawing.Color.White;
            this.btnNuevaIncidencia.Location = new System.Drawing.Point(10, 16);
            this.btnNuevaIncidencia.Name = "btnNuevaIncidencia";
            this.btnNuevaIncidencia.Size = new System.Drawing.Size(190, 36);
            this.btnNuevaIncidencia.TabIndex = 0;
            this.btnNuevaIncidencia.Text = "Nueva incidencia";
            this.btnNuevaIncidencia.UseVisualStyleBackColor = false;
            // 
            // pnlEstado
            // 
            this.pnlEstado.BackColor = System.Drawing.Color.White;
            this.pnlEstado.Controls.Add(this.lblEstadoOperacion);
            this.pnlEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEstado.Location = new System.Drawing.Point(24, 514);
            this.pnlEstado.Name = "pnlEstado";
            this.pnlEstado.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlEstado.Size = new System.Drawing.Size(1232, 44);
            this.pnlEstado.TabIndex = 2;
            // 
            // lblEstadoOperacion
            // 
            this.lblEstadoOperacion.AutoEllipsis = true;
            this.lblEstadoOperacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstadoOperacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoOperacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.lblEstadoOperacion.Location = new System.Drawing.Point(12, 8);
            this.lblEstadoOperacion.Name = "lblEstadoOperacion";
            this.lblEstadoOperacion.Size = new System.Drawing.Size(1208, 28);
            this.lblEstadoOperacion.TabIndex = 0;
            this.lblEstadoOperacion.Text = "Estado de operación: listo.";
            this.lblEstadoOperacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmIncidencias
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlAdministracion);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "frmIncidencias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GestorIncidencias";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlAdministracion.ResumeLayout(false);
            this.pnlAdministracion.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidencias)).EndInit();
            this.pnlAcciones.ResumeLayout(false);
            this.pnlEstado.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTituloApp;
        private System.Windows.Forms.Panel pnlAdministracion;
        private System.Windows.Forms.ComboBox cboFiltroEstado;
        private System.Windows.Forms.Label lblFiltroEstado;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.Label lblTituloAdministracion;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.DataGridView dgvIncidencias;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Button btnCambiarEstado;
        private System.Windows.Forms.Button btnModificarIncidencia;
        private System.Windows.Forms.Button btnNuevaIncidencia;
        private System.Windows.Forms.Panel pnlEstado;
        private System.Windows.Forms.Label lblEstadoOperacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdentificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResponsable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrioridad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaCreacion;
    }
}
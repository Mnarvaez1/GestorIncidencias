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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.colReportadoPor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResponsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrioridad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnVistaPreviaReporte = new System.Windows.Forms.Button();
            this.btnAdministrarEmpleados = new System.Windows.Forms.Button();
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
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTituloApp);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.pnlEncabezado.Size = new System.Drawing.Size(1240, 92);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = false;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 45);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(420, 42);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Sistema de Gestión de Incidencias\r\nSoporte Técnico Empresarial";
            //
            // lblTituloApp
            //
            this.lblTituloApp.AutoSize = true;
            this.lblTituloApp.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloApp.ForeColor = System.Drawing.Color.White;
            this.lblTituloApp.Location = new System.Drawing.Point(24, 12);
            this.lblTituloApp.Name = "lblTituloApp";
            this.lblTituloApp.Size = new System.Drawing.Size(352, 45);
            this.lblTituloApp.TabIndex = 0;
            this.lblTituloApp.Text = "XAMY TECHNOLOGIES";
            //
            // pnlAdministracion
            //
            this.pnlAdministracion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.pnlAdministracion.Controls.Add(this.cboFiltroEstado);
            this.pnlAdministracion.Controls.Add(this.lblFiltroEstado);
            this.pnlAdministracion.Controls.Add(this.txtBuscar);
            this.pnlAdministracion.Controls.Add(this.lblBuscar);
            this.pnlAdministracion.Controls.Add(this.lblTituloAdministracion);
            this.pnlAdministracion.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAdministracion.Location = new System.Drawing.Point(0, 92);
            this.pnlAdministracion.Name = "pnlAdministracion";
            this.pnlAdministracion.Padding = new System.Windows.Forms.Padding(24, 16, 24, 12);
            this.pnlAdministracion.Size = new System.Drawing.Size(1240, 126);
            this.pnlAdministracion.TabIndex = 1;
            //
            // cboFiltroEstado
            //
            this.cboFiltroEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFiltroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltroEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFiltroEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFiltroEstado.FormattingEnabled = true;
            this.cboFiltroEstado.Items.AddRange(new object[] {
            "Todos",
            "Pendiente",
            "En proceso",
            "Resuelta"});
            this.cboFiltroEstado.Location = new System.Drawing.Point(1018, 66);
            this.cboFiltroEstado.Name = "cboFiltroEstado";
            this.cboFiltroEstado.Size = new System.Drawing.Size(190, 33);
            this.cboFiltroEstado.TabIndex = 4;
            //
            // lblFiltroEstado
            //
            this.lblFiltroEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltroEstado.AutoSize = true;
            this.lblFiltroEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltroEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.lblFiltroEstado.Location = new System.Drawing.Point(1013, 37);
            this.lblFiltroEstado.Name = "lblFiltroEstado";
            this.lblFiltroEstado.Size = new System.Drawing.Size(119, 25);
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
            this.txtBuscar.Size = new System.Drawing.Size(974, 31);
            this.txtBuscar.TabIndex = 2;
            //
            // lblBuscar
            //
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.lblBuscar.Location = new System.Drawing.Point(22, 37);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(385, 25);
            this.lblBuscar.TabIndex = 1;
            this.lblBuscar.Text = "Buscar por título, reportado por o responsable:";
            //
            // lblTituloAdministracion
            //
            this.lblTituloAdministracion.AutoSize = true;
            this.lblTituloAdministracion.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloAdministracion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.lblTituloAdministracion.Location = new System.Drawing.Point(22, 8);
            this.lblTituloAdministracion.Name = "lblTituloAdministracion";
            this.lblTituloAdministracion.Size = new System.Drawing.Size(282, 28);
            this.lblTituloAdministracion.TabIndex = 0;
            this.lblTituloAdministracion.Text = "Administración de incidencias";
            //
            // pnlContenido
            //
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.pnlContenido.Controls.Add(this.dgvIncidencias);
            this.pnlContenido.Controls.Add(this.pnlAcciones);
            this.pnlContenido.Controls.Add(this.pnlEstado);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 218);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 0, 24, 24);
            this.pnlContenido.Size = new System.Drawing.Size(1240, 502);
            this.pnlContenido.TabIndex = 2;
            //
            // dgvIncidencias
            //
            this.dgvIncidencias.AllowUserToAddRows = false;
            this.dgvIncidencias.AllowUserToDeleteRows = false;
            this.dgvIncidencias.AllowUserToResizeRows = false;
            this.dgvIncidencias.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(243)))));
            this.dgvIncidencias.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvIncidencias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvIncidencias.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIncidencias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvIncidencias.ColumnHeadersHeight = 38;
            this.dgvIncidencias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdentificador,
            this.colTitulo,
            this.colDescripcion,
            this.colReportadoPor,
            this.colResponsable,
            this.colPrioridad,
            this.colEstado,
            this.colFechaCreacion});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(215)))), ((int)(((byte)(189)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIncidencias.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvIncidencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIncidencias.EnableHeadersVisualStyles = false;
            this.dgvIncidencias.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(224)))), ((int)(((byte)(214)))));
            this.dgvIncidencias.Location = new System.Drawing.Point(24, 0);
            this.dgvIncidencias.MultiSelect = false;
            this.dgvIncidencias.Name = "dgvIncidencias";
            this.dgvIncidencias.ReadOnly = true;
            this.dgvIncidencias.RowHeadersVisible = false;
            this.dgvIncidencias.RowHeadersWidth = 62;
            this.dgvIncidencias.RowTemplate.Height = 30;
            this.dgvIncidencias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIncidencias.Size = new System.Drawing.Size(1192, 358);
            this.dgvIncidencias.TabIndex = 0;
            //
            // colIdentificador
            //
            this.colIdentificador.HeaderText = "Identificador";
            this.colIdentificador.MinimumWidth = 8;
            this.colIdentificador.Name = "colIdentificador";
            this.colIdentificador.ReadOnly = true;
            this.colIdentificador.Width = 110;
            //
            // colTitulo
            //
            this.colTitulo.HeaderText = "Título";
            this.colTitulo.MinimumWidth = 8;
            this.colTitulo.Name = "colTitulo";
            this.colTitulo.ReadOnly = true;
            this.colTitulo.Width = 185;
            //
            // colDescripcion
            //
            this.colDescripcion.HeaderText = "Descripción";
            this.colDescripcion.MinimumWidth = 8;
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.ReadOnly = true;
            this.colDescripcion.Width = 265;
            //
            // colReportadoPor
            //
            this.colReportadoPor.HeaderText = "Reportado por";
            this.colReportadoPor.MinimumWidth = 8;
            this.colReportadoPor.Name = "colReportadoPor";
            this.colReportadoPor.ReadOnly = true;
            this.colReportadoPor.Width = 170;
            // 
            // colResponsable
            // 
            this.colResponsable.HeaderText = "Responsable";
            this.colResponsable.MinimumWidth = 8;
            this.colResponsable.Name = "colResponsable";
            this.colResponsable.ReadOnly = true;
            this.colResponsable.Width = 170;
            // 
            // colPrioridad
            // 
            this.colPrioridad.HeaderText = "Prioridad";
            this.colPrioridad.MinimumWidth = 8;
            this.colPrioridad.Name = "colPrioridad";
            this.colPrioridad.ReadOnly = true;
            this.colPrioridad.Width = 110;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 8;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            this.colEstado.Width = 120;
            // 
            // colFechaCreacion
            // 
            this.colFechaCreacion.HeaderText = "Fecha de creación";
            this.colFechaCreacion.MinimumWidth = 8;
            this.colFechaCreacion.Name = "colFechaCreacion";
            this.colFechaCreacion.ReadOnly = true;
            this.colFechaCreacion.Width = 170;
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.Controls.Add(this.btnSalir);
            this.pnlAcciones.Controls.Add(this.btnVistaPreviaReporte);
            this.pnlAcciones.Controls.Add(this.btnAdministrarEmpleados);
            this.pnlAcciones.Controls.Add(this.btnCambiarEstado);
            this.pnlAcciones.Controls.Add(this.btnModificarIncidencia);
            this.pnlAcciones.Controls.Add(this.btnNuevaIncidencia);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(24, 358);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Padding = new System.Windows.Forms.Padding(0, 16, 0, 10);
            this.pnlAcciones.Size = new System.Drawing.Size(1192, 68);
            this.pnlAcciones.TabIndex = 1;
            //
            // btnSalir
            //
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.Color.White;
            this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnSalir.FlatAppearance.BorderSize = 1;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.btnSalir.Location = new System.Drawing.Point(1065, 16);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(124, 36);
            this.btnSalir.TabIndex = 4;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            //
            // btnVistaPreviaReporte
            //
            this.btnVistaPreviaReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVistaPreviaReporte.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.btnVistaPreviaReporte.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnVistaPreviaReporte.FlatAppearance.BorderSize = 1;
            this.btnVistaPreviaReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVistaPreviaReporte.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVistaPreviaReporte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnVistaPreviaReporte.Location = new System.Drawing.Point(829, 16);
            this.btnVistaPreviaReporte.Name = "btnVistaPreviaReporte";
            this.btnVistaPreviaReporte.Size = new System.Drawing.Size(230, 36);
            this.btnVistaPreviaReporte.TabIndex = 3;
            this.btnVistaPreviaReporte.Text = "Vista previa de reporte";
            this.btnVistaPreviaReporte.UseVisualStyleBackColor = false;
            //
            // btnAdministrarEmpleados
            //
            this.btnAdministrarEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.btnAdministrarEmpleados.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnAdministrarEmpleados.FlatAppearance.BorderSize = 1;
            this.btnAdministrarEmpleados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdministrarEmpleados.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdministrarEmpleados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnAdministrarEmpleados.Location = new System.Drawing.Point(598, 16);
            this.btnAdministrarEmpleados.Name = "btnAdministrarEmpleados";
            this.btnAdministrarEmpleados.Size = new System.Drawing.Size(225, 36);
            this.btnAdministrarEmpleados.TabIndex = 5;
            this.btnAdministrarEmpleados.Text = "Administrar empleados";
            this.btnAdministrarEmpleados.UseVisualStyleBackColor = false;
            //
            // btnCambiarEstado
            //
            this.btnCambiarEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.btnCambiarEstado.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.btnCambiarEstado.FlatAppearance.BorderSize = 1;
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
            this.btnModificarIncidencia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnModificarIncidencia.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(143)))), ((int)(((byte)(82)))));
            this.btnModificarIncidencia.FlatAppearance.BorderSize = 1;
            this.btnModificarIncidencia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarIncidencia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarIncidencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.btnModificarIncidencia.Location = new System.Drawing.Point(206, 16);
            this.btnModificarIncidencia.Name = "btnModificarIncidencia";
            this.btnModificarIncidencia.Size = new System.Drawing.Size(190, 36);
            this.btnModificarIncidencia.TabIndex = 1;
            this.btnModificarIncidencia.Text = "Modificar incidencia";
            this.btnModificarIncidencia.UseVisualStyleBackColor = false;
            // 
            // btnNuevaIncidencia
            // 
            this.btnNuevaIncidencia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.btnNuevaIncidencia.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnNuevaIncidencia.FlatAppearance.BorderSize = 1;
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
            this.pnlEstado.Location = new System.Drawing.Point(24, 426);
            this.pnlEstado.Name = "pnlEstado";
            this.pnlEstado.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlEstado.Size = new System.Drawing.Size(1192, 52);
            this.pnlEstado.TabIndex = 2;
            // 
            // lblEstadoOperacion
            // 
            this.lblEstadoOperacion.AutoEllipsis = true;
            this.lblEstadoOperacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstadoOperacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoOperacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.lblEstadoOperacion.Location = new System.Drawing.Point(12, 8);
            this.lblEstadoOperacion.Name = "lblEstadoOperacion";
            this.lblEstadoOperacion.Size = new System.Drawing.Size(1168, 36);
            this.lblEstadoOperacion.TabIndex = 0;
            this.lblEstadoOperacion.Text = "Estado de operación: listo.";
            this.lblEstadoOperacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmIncidencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(1240, 720);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlAdministracion);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "frmIncidencias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "XAMY Technologies - Gestión de incidencias";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnCambiarEstado;
        private System.Windows.Forms.Button btnModificarIncidencia;
        private System.Windows.Forms.Button btnNuevaIncidencia;
        private System.Windows.Forms.Panel pnlEstado;
        private System.Windows.Forms.Label lblEstadoOperacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdentificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReportadoPor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResponsable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrioridad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaCreacion;
        private System.Windows.Forms.Button btnVistaPreviaReporte;
        private System.Windows.Forms.Button btnAdministrarEmpleados;
    }
}
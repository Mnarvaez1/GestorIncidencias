namespace GestorIncidencias
{
    partial class frmEmpleados
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.pnlLineaDorada = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnBuscarEmpleado = new System.Windows.Forms.Button();
            this.txtBuscarEmpleado = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.lblEstadoVacio = new System.Windows.Forms.Label();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.colIdentificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombreCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTecnico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstadoLaboral = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisponibilidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnActivarInactivar = new System.Windows.Forms.Button();
            this.btnModificarEmpleado = new System.Windows.Forms.Button();
            this.btnNuevoEmpleado = new System.Windows.Forms.Button();
            this.pnlEstado = new System.Windows.Forms.Panel();
            this.lblEstadoOperacion = new System.Windows.Forms.Label();
            this.pnlEncabezado.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.pnlAcciones.SuspendLayout();
            this.pnlEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.pnlEncabezado.Controls.Add(this.pnlLineaDorada);
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.lblMarca);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 14, 24, 12);
            this.pnlEncabezado.Size = new System.Drawing.Size(1240, 110);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // pnlLineaDorada
            // 
            this.pnlLineaDorada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.pnlLineaDorada.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLineaDorada.Location = new System.Drawing.Point(24, 96);
            this.pnlLineaDorada.Name = "pnlLineaDorada";
            this.pnlLineaDorada.Size = new System.Drawing.Size(1192, 2);
            this.pnlLineaDorada.TabIndex = 3;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(28, 66);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(364, 25);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Gestión de personal y técnicos de soporte";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(26, 30);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(480, 38);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "ADMINISTRACIÓN DE EMPLEADOS";
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.lblMarca.Location = new System.Drawing.Point(28, 8);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(301, 25);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "XAMY TECHNOLOGIES · SOPORTE TI";
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.pnlFiltros.Controls.Add(this.btnActualizar);
            this.pnlFiltros.Controls.Add(this.btnBuscarEmpleado);
            this.pnlFiltros.Controls.Add(this.txtBuscarEmpleado);
            this.pnlFiltros.Controls.Add(this.lblBuscar);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(0, 110);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Padding = new System.Windows.Forms.Padding(24, 14, 24, 10);
            this.pnlFiltros.Size = new System.Drawing.Size(1240, 92);
            this.pnlFiltros.TabIndex = 1;
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.White;
            this.btnActualizar.Enabled = false;
            this.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizar.Location = new System.Drawing.Point(1094, 36);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(122, 34);
            this.btnActualizar.TabIndex = 4;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            // 
            // btnBuscarEmpleado
            // 
            this.btnBuscarEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnBuscarEmpleado.Enabled = false;
            this.btnBuscarEmpleado.FlatAppearance.BorderSize = 0;
            this.btnBuscarEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarEmpleado.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarEmpleado.ForeColor = System.Drawing.Color.White;
            this.btnBuscarEmpleado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarEmpleado.Location = new System.Drawing.Point(968, 36);
            this.btnBuscarEmpleado.Name = "btnBuscarEmpleado";
            this.btnBuscarEmpleado.Size = new System.Drawing.Size(118, 34);
            this.btnBuscarEmpleado.TabIndex = 3;
            this.btnBuscarEmpleado.Text = "Buscar";
            this.btnBuscarEmpleado.UseVisualStyleBackColor = false;
            // 
            // txtBuscarEmpleado
            // 
            this.txtBuscarEmpleado.BackColor = System.Drawing.Color.White;
            this.txtBuscarEmpleado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscarEmpleado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarEmpleado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscarEmpleado.Location = new System.Drawing.Point(186, 36);
            this.txtBuscarEmpleado.Name = "txtBuscarEmpleado";
            this.txtBuscarEmpleado.Size = new System.Drawing.Size(768, 34);
            this.txtBuscarEmpleado.TabIndex = 2;
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.lblBuscar.Location = new System.Drawing.Point(28, 41);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(128, 25);
            this.lblBuscar.TabIndex = 1;
            this.lblBuscar.Text = "Buscar por nombre:";
            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.White;
            this.pnlContenido.Controls.Add(this.dgvEmpleados);
            this.pnlContenido.Controls.Add(this.lblEstadoVacio);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 202);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 12, 24, 10);
            this.pnlContenido.Size = new System.Drawing.Size(1240, 422);
            this.pnlContenido.TabIndex = 2;
            // 
            // lblEstadoVacio
            // 
            this.lblEstadoVacio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.lblEstadoVacio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEstadoVacio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstadoVacio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoVacio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lblEstadoVacio.Location = new System.Drawing.Point(24, 12);
            this.lblEstadoVacio.Name = "lblEstadoVacio";
            this.lblEstadoVacio.Size = new System.Drawing.Size(1192, 400);
            this.lblEstadoVacio.TabIndex = 1;
            this.lblEstadoVacio.Text = "No hay empleados registrados por el momento.";
            this.lblEstadoVacio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.AllowUserToAddRows = false;
            this.dgvEmpleados.AllowUserToDeleteRows = false;
            this.dgvEmpleados.BackgroundColor = System.Drawing.Color.White;
            this.dgvEmpleados.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvEmpleados.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmpleados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEmpleados.ColumnHeadersHeight = 40;
            this.dgvEmpleados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdentificador,
            this.colNombreCompleto,
            this.colTecnico,
            this.colEstadoLaboral,
            this.colDisponibilidad});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEmpleados.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmpleados.EnableHeadersVisualStyles = false;
            this.dgvEmpleados.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(223)))), ((int)(((byte)(216)))));
            this.dgvEmpleados.Location = new System.Drawing.Point(24, 12);
            this.dgvEmpleados.MultiSelect = false;
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.ReadOnly = true;
            this.dgvEmpleados.RowHeadersVisible = false;
            this.dgvEmpleados.RowHeadersWidth = 62;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dgvEmpleados.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvEmpleados.RowTemplate.Height = 34;
            this.dgvEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmpleados.Size = new System.Drawing.Size(1192, 400);
            this.dgvEmpleados.TabIndex = 0;
            // 
            // colIdentificador
            // 
            this.colIdentificador.DataPropertyName = "Identificador";
            this.colIdentificador.HeaderText = "Identificador";
            this.colIdentificador.MinimumWidth = 8;
            this.colIdentificador.Name = "colIdentificador";
            this.colIdentificador.ReadOnly = true;
            this.colIdentificador.Width = 140;
            // 
            // colNombreCompleto
            // 
            this.colNombreCompleto.DataPropertyName = "NombreCompleto";
            this.colNombreCompleto.HeaderText = "Nombre completo";
            this.colNombreCompleto.MinimumWidth = 8;
            this.colNombreCompleto.Name = "colNombreCompleto";
            this.colNombreCompleto.ReadOnly = true;
            this.colNombreCompleto.Width = 380;
            // 
            // colTecnico
            // 
            this.colTecnico.DataPropertyName = "EsTecnicoSoporte";
            this.colTecnico.HeaderText = "Técnico de soporte";
            this.colTecnico.MinimumWidth = 8;
            this.colTecnico.Name = "colTecnico";
            this.colTecnico.ReadOnly = true;
            this.colTecnico.Width = 190;
            // 
            // colEstadoLaboral
            // 
            this.colEstadoLaboral.DataPropertyName = "Activo";
            this.colEstadoLaboral.HeaderText = "Estado laboral";
            this.colEstadoLaboral.MinimumWidth = 8;
            this.colEstadoLaboral.Name = "colEstadoLaboral";
            this.colEstadoLaboral.ReadOnly = true;
            this.colEstadoLaboral.Width = 190;
            // 
            // colDisponibilidad
            // 
            this.colDisponibilidad.HeaderText = "Disponibilidad";
            this.colDisponibilidad.MinimumWidth = 8;
            this.colDisponibilidad.Name = "colDisponibilidad";
            this.colDisponibilidad.ReadOnly = true;
            this.colDisponibilidad.Width = 220;
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.pnlAcciones.Controls.Add(this.btnCerrar);
            this.pnlAcciones.Controls.Add(this.btnActivarInactivar);
            this.pnlAcciones.Controls.Add(this.btnModificarEmpleado);
            this.pnlAcciones.Controls.Add(this.btnNuevoEmpleado);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(0, 624);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Padding = new System.Windows.Forms.Padding(24, 10, 24, 10);
            this.pnlAcciones.Size = new System.Drawing.Size(1240, 70);
            this.pnlAcciones.TabIndex = 3;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Location = new System.Drawing.Point(1087, 14);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(129, 40);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            // 
            // btnActivarInactivar
            // 
            this.btnActivarInactivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnActivarInactivar.Enabled = false;
            this.btnActivarInactivar.FlatAppearance.BorderSize = 0;
            this.btnActivarInactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivarInactivar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivarInactivar.ForeColor = System.Drawing.Color.White;
            this.btnActivarInactivar.Location = new System.Drawing.Point(458, 14);
            this.btnActivarInactivar.Name = "btnActivarInactivar";
            this.btnActivarInactivar.Size = new System.Drawing.Size(196, 40);
            this.btnActivarInactivar.TabIndex = 2;
            this.btnActivarInactivar.Text = "Activar / Inactivar";
            this.btnActivarInactivar.UseVisualStyleBackColor = false;
            // 
            // btnModificarEmpleado
            // 
            this.btnModificarEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnModificarEmpleado.Enabled = false;
            this.btnModificarEmpleado.FlatAppearance.BorderSize = 0;
            this.btnModificarEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarEmpleado.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarEmpleado.ForeColor = System.Drawing.Color.White;
            this.btnModificarEmpleado.Location = new System.Drawing.Point(256, 14);
            this.btnModificarEmpleado.Name = "btnModificarEmpleado";
            this.btnModificarEmpleado.Size = new System.Drawing.Size(186, 40);
            this.btnModificarEmpleado.TabIndex = 1;
            this.btnModificarEmpleado.Text = "Modificar";
            this.btnModificarEmpleado.UseVisualStyleBackColor = false;
            // 
            // btnNuevoEmpleado
            // 
            this.btnNuevoEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.btnNuevoEmpleado.Enabled = false;
            this.btnNuevoEmpleado.FlatAppearance.BorderSize = 0;
            this.btnNuevoEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoEmpleado.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoEmpleado.ForeColor = System.Drawing.Color.White;
            this.btnNuevoEmpleado.Location = new System.Drawing.Point(24, 14);
            this.btnNuevoEmpleado.Name = "btnNuevoEmpleado";
            this.btnNuevoEmpleado.Size = new System.Drawing.Size(216, 40);
            this.btnNuevoEmpleado.TabIndex = 0;
            this.btnNuevoEmpleado.Text = "Nuevo empleado";
            this.btnNuevoEmpleado.UseVisualStyleBackColor = false;
            // 
            // pnlEstado
            // 
            this.pnlEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.pnlEstado.Controls.Add(this.lblEstadoOperacion);
            this.pnlEstado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEstado.Location = new System.Drawing.Point(0, 694);
            this.pnlEstado.Name = "pnlEstado";
            this.pnlEstado.Padding = new System.Windows.Forms.Padding(24, 8, 24, 8);
            this.pnlEstado.Size = new System.Drawing.Size(1240, 38);
            this.pnlEstado.TabIndex = 4;
            // 
            // lblEstadoOperacion
            // 
            this.lblEstadoOperacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstadoOperacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoOperacion.ForeColor = System.Drawing.Color.White;
            this.lblEstadoOperacion.Location = new System.Drawing.Point(24, 8);
            this.lblEstadoOperacion.Name = "lblEstadoOperacion";
            this.lblEstadoOperacion.Size = new System.Drawing.Size(1192, 22);
            this.lblEstadoOperacion.TabIndex = 0;
            this.lblEstadoOperacion.Text = "Estado de operación";
            this.lblEstadoOperacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1240, 732);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.pnlEstado);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1000, 680);
            this.Name = "frmEmpleados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XAMY TECHNOLOGIES - Administración de empleados";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.pnlContenido.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.pnlAcciones.ResumeLayout(false);
            this.pnlEstado.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlLineaDorada;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscarEmpleado;
        private System.Windows.Forms.Button btnBuscarEmpleado;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.Label lblEstadoVacio;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Button btnNuevoEmpleado;
        private System.Windows.Forms.Button btnModificarEmpleado;
        private System.Windows.Forms.Button btnActivarInactivar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Panel pnlEstado;
        private System.Windows.Forms.Label lblEstadoOperacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdentificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombreCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTecnico;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstadoLaboral;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisponibilidad;
    }
}

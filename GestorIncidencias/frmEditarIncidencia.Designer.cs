namespace GestorIncidencias
{
    partial class frmEditarIncidencia
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
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.pnlLineaDoradaEncabezado = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlFormulario = new System.Windows.Forms.Panel();
            this.lblAvisoResponsable = new System.Windows.Forms.Label();
            this.lblResponsableActual = new System.Windows.Forms.Label();
            this.cboResponsableTecnico = new System.Windows.Forms.ComboBox();
            this.cboPrioridad = new System.Windows.Forms.ComboBox();
            this.lblPrioridad = new System.Windows.Forms.Label();
            this.lblResponsable = new System.Windows.Forms.Label();
            this.txtReportadoPor = new System.Windows.Forms.TextBox();
            this.lblReportadoPor = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.lblTituloCampo = new System.Windows.Forms.Label();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.pnlEncabezado.SuspendLayout();
            this.pnlFormulario.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlEncabezado
            //
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.pnlEncabezado.Controls.Add(this.pnlLineaDoradaEncabezado);
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlEncabezado.Size = new System.Drawing.Size(860, 88);
            this.pnlEncabezado.TabIndex = 0;
            //
            // pnlLineaDoradaEncabezado
            //
            this.pnlLineaDoradaEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.pnlLineaDoradaEncabezado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLineaDoradaEncabezado.Location = new System.Drawing.Point(20, 74);
            this.pnlLineaDoradaEncabezado.Name = "pnlLineaDoradaEncabezado";
            this.pnlLineaDoradaEncabezado.Size = new System.Drawing.Size(820, 2);
            this.pnlLineaDoradaEncabezado.TabIndex = 2;
            //
            // lblSubtitulo
            //
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(24, 44);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(520, 25);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "XAMY TECHNOLOGIES · Sistema de Gestión de Incidencias";
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(22, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(225, 38);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Editar incidencia";
            //
            // pnlFormulario
            //
            this.pnlFormulario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.pnlFormulario.Controls.Add(this.lblAvisoResponsable);
            this.pnlFormulario.Controls.Add(this.lblResponsableActual);
            this.pnlFormulario.Controls.Add(this.cboResponsableTecnico);
            this.pnlFormulario.Controls.Add(this.cboPrioridad);
            this.pnlFormulario.Controls.Add(this.lblPrioridad);
            this.pnlFormulario.Controls.Add(this.lblResponsable);
            this.pnlFormulario.Controls.Add(this.txtReportadoPor);
            this.pnlFormulario.Controls.Add(this.lblReportadoPor);
            this.pnlFormulario.Controls.Add(this.txtDescripcion);
            this.pnlFormulario.Controls.Add(this.lblDescripcion);
            this.pnlFormulario.Controls.Add(this.txtTitulo);
            this.pnlFormulario.Controls.Add(this.lblTituloCampo);
            this.pnlFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormulario.Location = new System.Drawing.Point(0, 88);
            this.pnlFormulario.Name = "pnlFormulario";
            this.pnlFormulario.Padding = new System.Windows.Forms.Padding(24, 20, 24, 12);
            this.pnlFormulario.Size = new System.Drawing.Size(860, 476);
            this.pnlFormulario.TabIndex = 1;
            //
            // lblAvisoResponsable
            //
            this.lblAvisoResponsable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAvisoResponsable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lblAvisoResponsable.Location = new System.Drawing.Point(432, 405);
            this.lblAvisoResponsable.Name = "lblAvisoResponsable";
            this.lblAvisoResponsable.Size = new System.Drawing.Size(400, 66);
            this.lblAvisoResponsable.TabIndex = 12;
            //
            // lblResponsableActual
            //
            this.lblResponsableActual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResponsableActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.lblResponsableActual.Location = new System.Drawing.Point(432, 322);
            this.lblResponsableActual.Name = "lblResponsableActual";
            this.lblResponsableActual.Size = new System.Drawing.Size(400, 20);
            this.lblResponsableActual.TabIndex = 11;
            //
            // cboResponsableTecnico
            //
            this.cboResponsableTecnico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboResponsableTecnico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResponsableTecnico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboResponsableTecnico.FormattingEnabled = true;
            this.cboResponsableTecnico.Location = new System.Drawing.Point(436, 367);
            this.cboResponsableTecnico.Name = "cboResponsableTecnico";
            this.cboResponsableTecnico.Size = new System.Drawing.Size(396, 33);
            this.cboResponsableTecnico.TabIndex = 10;
            //
            // cboPrioridad
            //
            this.cboPrioridad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboPrioridad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrioridad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPrioridad.FormattingEnabled = true;
            this.cboPrioridad.Items.AddRange(new object[] {
            "Baja",
            "Media",
            "Alta"});
            this.cboPrioridad.Location = new System.Drawing.Point(28, 438);
            this.cboPrioridad.Name = "cboPrioridad";
            this.cboPrioridad.Size = new System.Drawing.Size(200, 33);
            this.cboPrioridad.TabIndex = 9;
            //
            // lblPrioridad
            //
            this.lblPrioridad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPrioridad.AutoSize = true;
            this.lblPrioridad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.lblPrioridad.Location = new System.Drawing.Point(24, 414);
            this.lblPrioridad.Name = "lblPrioridad";
            this.lblPrioridad.Size = new System.Drawing.Size(84, 25);
            this.lblPrioridad.TabIndex = 8;
            this.lblPrioridad.Text = "Prioridad";
            //
            // lblResponsable
            //
            this.lblResponsable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResponsable.AutoSize = true;
            this.lblResponsable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.lblResponsable.Location = new System.Drawing.Point(432, 343);
            this.lblResponsable.Name = "lblResponsable";
            this.lblResponsable.Size = new System.Drawing.Size(183, 25);
            this.lblResponsable.TabIndex = 6;
            this.lblResponsable.Text = "Responsable (técnico)";
            //
            // txtReportadoPor
            //
            this.txtReportadoPor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReportadoPor.BackColor = System.Drawing.Color.White;
            this.txtReportadoPor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReportadoPor.Location = new System.Drawing.Point(28, 367);
            this.txtReportadoPor.Name = "txtReportadoPor";
            this.txtReportadoPor.Size = new System.Drawing.Size(396, 31);
            this.txtReportadoPor.TabIndex = 5;
            //
            // lblReportadoPor
            //
            this.lblReportadoPor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblReportadoPor.AutoSize = true;
            this.lblReportadoPor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.lblReportadoPor.Location = new System.Drawing.Point(24, 343);
            this.lblReportadoPor.Name = "lblReportadoPor";
            this.lblReportadoPor.Size = new System.Drawing.Size(129, 25);
            this.lblReportadoPor.TabIndex = 4;
            this.lblReportadoPor.Text = "Reportado por";
            //
            // txtDescripcion
            //
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcion.BackColor = System.Drawing.Color.White;
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescripcion.Location = new System.Drawing.Point(28, 134);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(804, 184);
            this.txtDescripcion.TabIndex = 3;
            //
            // lblDescripcion
            //
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.lblDescripcion.Location = new System.Drawing.Point(24, 110);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(104, 25);
            this.lblDescripcion.TabIndex = 2;
            this.lblDescripcion.Text = "Descripción";
            //
            // txtTitulo
            //
            this.txtTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitulo.BackColor = System.Drawing.Color.White;
            this.txtTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitulo.Location = new System.Drawing.Point(28, 62);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(804, 31);
            this.txtTitulo.TabIndex = 1;
            //
            // lblTituloCampo
            //
            this.lblTituloCampo.AutoSize = true;
            this.lblTituloCampo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.lblTituloCampo.Location = new System.Drawing.Point(24, 38);
            this.lblTituloCampo.Name = "lblTituloCampo";
            this.lblTituloCampo.Size = new System.Drawing.Size(56, 25);
            this.lblTituloCampo.TabIndex = 0;
            this.lblTituloCampo.Text = "Título";
            //
            // pnlAcciones
            //
            this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.pnlAcciones.Controls.Add(this.btnCancelar);
            this.pnlAcciones.Controls.Add(this.btnGuardar);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(0, 564);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Padding = new System.Windows.Forms.Padding(24, 10, 24, 14);
            this.pnlAcciones.Size = new System.Drawing.Size(860, 76);
            this.pnlAcciones.TabIndex = 2;
            //
            // btnCancelar
            //
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.btnCancelar.FlatAppearance.BorderSize = 1;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(708, 18);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(124, 36);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            //
            // btnGuardar
            //
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(143)))), ((int)(((byte)(82)))));
            this.btnGuardar.FlatAppearance.BorderSize = 1;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.btnGuardar.Location = new System.Drawing.Point(568, 18);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(124, 36);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            //
            // frmEditarIncidencia
            //
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(860, 640);
            this.Controls.Add(this.pnlFormulario);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(760, 620);
            this.Name = "frmEditarIncidencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XAMY Technologies - Editar incidencia";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlFormulario.ResumeLayout(false);
            this.pnlFormulario.PerformLayout();
            this.pnlAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Panel pnlLineaDoradaEncabezado;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel pnlFormulario;
        private System.Windows.Forms.Label lblAvisoResponsable;
        private System.Windows.Forms.Label lblResponsableActual;
        private System.Windows.Forms.ComboBox cboResponsableTecnico;
        private System.Windows.Forms.ComboBox cboPrioridad;
        private System.Windows.Forms.Label lblPrioridad;
        private System.Windows.Forms.Label lblResponsable;
        private System.Windows.Forms.TextBox txtReportadoPor;
        private System.Windows.Forms.Label lblReportadoPor;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label lblTituloCampo;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
    }
}
namespace GestorIncidencias
{
    partial class frmEditarEmpleado
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
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.pnlLineaDorada = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.pnlFormulario = new System.Windows.Forms.Panel();
            this.lblMensajeValidacion = new System.Windows.Forms.Label();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.chkEsTecnicoSoporte = new System.Windows.Forms.CheckBox();
            this.txtNombreCompleto = new System.Windows.Forms.TextBox();
            this.lblNombreCompleto = new System.Windows.Forms.Label();
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
            this.pnlEncabezado.Controls.Add(this.pnlLineaDorada);
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.lblMarca);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(18, 10, 18, 10);
            this.pnlEncabezado.Size = new System.Drawing.Size(620, 98);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // pnlLineaDorada
            // 
            this.pnlLineaDorada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.pnlLineaDorada.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLineaDorada.Location = new System.Drawing.Point(18, 86);
            this.pnlLineaDorada.Name = "pnlLineaDorada";
            this.pnlLineaDorada.Size = new System.Drawing.Size(584, 2);
            this.pnlLineaDorada.TabIndex = 3;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(23, 57);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(214, 25);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Información del personal";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(21, 26);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(284, 36);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "REGISTRAR EMPLEADO";
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.lblMarca.Location = new System.Drawing.Point(23, 6);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(286, 23);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "XAMY TECHNOLOGIES · SOPORTE TI";
            // 
            // pnlFormulario
            // 
            this.pnlFormulario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.pnlFormulario.Controls.Add(this.lblMensajeValidacion);
            this.pnlFormulario.Controls.Add(this.chkActivo);
            this.pnlFormulario.Controls.Add(this.chkEsTecnicoSoporte);
            this.pnlFormulario.Controls.Add(this.txtNombreCompleto);
            this.pnlFormulario.Controls.Add(this.lblNombreCompleto);
            this.pnlFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormulario.Location = new System.Drawing.Point(0, 98);
            this.pnlFormulario.Name = "pnlFormulario";
            this.pnlFormulario.Padding = new System.Windows.Forms.Padding(22, 18, 22, 10);
            this.pnlFormulario.Size = new System.Drawing.Size(620, 256);
            this.pnlFormulario.TabIndex = 1;
            // 
            // lblMensajeValidacion
            // 
            this.lblMensajeValidacion.AutoSize = true;
            this.lblMensajeValidacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeValidacion.ForeColor = System.Drawing.Color.Firebrick;
            this.lblMensajeValidacion.Location = new System.Drawing.Point(25, 145);
            this.lblMensajeValidacion.Name = "lblMensajeValidacion";
            this.lblMensajeValidacion.Size = new System.Drawing.Size(0, 25);
            this.lblMensajeValidacion.TabIndex = 4;
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Checked = true;
            this.chkActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.chkActivo.Location = new System.Drawing.Point(30, 106);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(158, 29);
            this.chkActivo.TabIndex = 3;
            this.chkActivo.Text = "Estado: Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // chkEsTecnicoSoporte
            // 
            this.chkEsTecnicoSoporte.AutoSize = true;
            this.chkEsTecnicoSoporte.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEsTecnicoSoporte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.chkEsTecnicoSoporte.Location = new System.Drawing.Point(30, 71);
            this.chkEsTecnicoSoporte.Name = "chkEsTecnicoSoporte";
            this.chkEsTecnicoSoporte.Size = new System.Drawing.Size(206, 29);
            this.chkEsTecnicoSoporte.TabIndex = 2;
            this.chkEsTecnicoSoporte.Text = "Es técnico de soporte";
            this.chkEsTecnicoSoporte.UseVisualStyleBackColor = true;
            // 
            // txtNombreCompleto
            // 
            this.txtNombreCompleto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreCompleto.BackColor = System.Drawing.Color.White;
            this.txtNombreCompleto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombreCompleto.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreCompleto.Location = new System.Drawing.Point(206, 22);
            this.txtNombreCompleto.Name = "txtNombreCompleto";
            this.txtNombreCompleto.Size = new System.Drawing.Size(387, 34);
            this.txtNombreCompleto.TabIndex = 1;
            // 
            // lblNombreCompleto
            // 
            this.lblNombreCompleto.AutoSize = true;
            this.lblNombreCompleto.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreCompleto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.lblNombreCompleto.Location = new System.Drawing.Point(25, 27);
            this.lblNombreCompleto.Name = "lblNombreCompleto";
            this.lblNombreCompleto.Size = new System.Drawing.Size(160, 25);
            this.lblNombreCompleto.TabIndex = 0;
            this.lblNombreCompleto.Text = "Nombre completo:";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.White;
            this.pnlAcciones.Controls.Add(this.btnCancelar);
            this.pnlAcciones.Controls.Add(this.btnGuardar);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(0, 354);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Padding = new System.Windows.Forms.Padding(22, 10, 22, 10);
            this.pnlAcciones.Size = new System.Drawing.Size(620, 66);
            this.pnlAcciones.TabIndex = 2;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.btnCancelar.Location = new System.Drawing.Point(472, 13);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(121, 40);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(337, 13);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(129, 40);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // frmEditarEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(620, 420);
            this.Controls.Add(this.pnlFormulario);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(620, 420);
            this.Name = "frmEditarEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XAMY TECHNOLOGIES - Registrar empleado";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlFormulario.ResumeLayout(false);
            this.pnlFormulario.PerformLayout();
            this.pnlAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlLineaDorada;
        private System.Windows.Forms.Panel pnlFormulario;
        private System.Windows.Forms.Label lblNombreCompleto;
        private System.Windows.Forms.TextBox txtNombreCompleto;
        private System.Windows.Forms.CheckBox chkEsTecnicoSoporte;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Label lblMensajeValidacion;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}

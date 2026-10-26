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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblReporteOperativo = new System.Windows.Forms.Label();
            this.pnlIconoDocumento = new System.Windows.Forms.Panel();
            this.pnlLineaDorada = new System.Windows.Forms.Panel();
            this.lblFechaGeneracion = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.pnlIndicadores = new System.Windows.Forms.Panel();
            this.tlpIndicadores = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCardResueltas = new System.Windows.Forms.Panel();
            this.lblResueltasValor = new System.Windows.Forms.Label();
            this.lblResueltasTitulo = new System.Windows.Forms.Label();
            this.pnlCardEnProceso = new System.Windows.Forms.Panel();
            this.lblEnProcesoValor = new System.Windows.Forms.Label();
            this.lblEnProcesoTitulo = new System.Windows.Forms.Label();
            this.pnlCardPendientes = new System.Windows.Forms.Panel();
            this.lblPendientesValor = new System.Windows.Forms.Label();
            this.lblPendientesTitulo = new System.Windows.Forms.Label();
            this.pnlCardTotal = new System.Windows.Forms.Panel();
            this.lblTotalValor = new System.Windows.Forms.Label();
            this.lblTotalTitulo = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlDetalle = new System.Windows.Forms.Panel();
            this.flpIncidencias = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTabla = new System.Windows.Forms.Panel();
            this.lblEstadoVacio = new System.Windows.Forms.Label();
            this.dgvIncidenciasReporte = new System.Windows.Forms.DataGridView();
            this.colIdentificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReportadoPor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResponsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrioridad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaCreacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlDetalleLinea = new System.Windows.Forms.Panel();
            this.pnlDetalleCabecera = new System.Windows.Forms.Panel();
            this.btnExportarHtml = new System.Windows.Forms.Button();
            this.lnkVerDetalle = new System.Windows.Forms.LinkLabel();
            this.lblConteoRegistros = new System.Windows.Forms.Label();
            this.lblResumen = new System.Windows.Forms.Label();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.lblLeyenda = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.pnlEncabezado.SuspendLayout();
            this.pnlIndicadores.SuspendLayout();
            this.tlpIndicadores.SuspendLayout();
            this.pnlCardResueltas.SuspendLayout();
            this.pnlCardEnProceso.SuspendLayout();
            this.pnlCardPendientes.SuspendLayout();
            this.pnlCardTotal.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlDetalle.SuspendLayout();
            this.pnlTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidenciasReporte)).BeginInit();
            this.pnlDetalleCabecera.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.pnlEncabezado.Controls.Add(this.lblReporteOperativo);
            this.pnlEncabezado.Controls.Add(this.pnlIconoDocumento);
            this.pnlEncabezado.Controls.Add(this.pnlLineaDorada);
            this.pnlEncabezado.Controls.Add(this.lblFechaGeneracion);
            this.pnlEncabezado.Controls.Add(this.lblSubtitulo);
            this.pnlEncabezado.Controls.Add(this.lblTitulo);
            this.pnlEncabezado.Controls.Add(this.lblMarca);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(24, 10, 24, 10);
            this.pnlEncabezado.Size = new System.Drawing.Size(1050, 136);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // lblReporteOperativo
            // 
            this.lblReporteOperativo.AutoSize = true;
            this.lblReporteOperativo.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReporteOperativo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.lblReporteOperativo.Location = new System.Drawing.Point(27, 108);
            this.lblReporteOperativo.Name = "lblReporteOperativo";
            this.lblReporteOperativo.Size = new System.Drawing.Size(178, 23);
            this.lblReporteOperativo.TabIndex = 6;
            this.lblReporteOperativo.Text = "REPORTE OPERATIVO";
            // 
            // pnlIconoDocumento
            // 
            this.pnlIconoDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlIconoDocumento.Location = new System.Drawing.Point(976, 16);
            this.pnlIconoDocumento.Name = "pnlIconoDocumento";
            this.pnlIconoDocumento.Size = new System.Drawing.Size(46, 50);
            this.pnlIconoDocumento.TabIndex = 5;
            // 
            // pnlLineaDorada
            // 
            this.pnlLineaDorada.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLineaDorada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.pnlLineaDorada.Location = new System.Drawing.Point(0, 95);
            this.pnlLineaDorada.Name = "pnlLineaDorada";
            this.pnlLineaDorada.Size = new System.Drawing.Size(1050, 10);
            this.pnlLineaDorada.TabIndex = 4;
            // 
            // lblFechaGeneracion
            // 
            this.lblFechaGeneracion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFechaGeneracion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.lblFechaGeneracion.Location = new System.Drawing.Point(676, 108);
            this.lblFechaGeneracion.Name = "lblFechaGeneracion";
            this.lblFechaGeneracion.Size = new System.Drawing.Size(350, 24);
            this.lblFechaGeneracion.TabIndex = 3;
            this.lblFechaGeneracion.Text = "--/--/---- · --:--";
            this.lblFechaGeneracion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSubtitulo.Location = new System.Drawing.Point(27, 67);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(365, 25);
            this.lblSubtitulo.TabIndex = 2;
            this.lblSubtitulo.Text = "Seguimiento de incidencias empresariales";
            this.lblSubtitulo.Click += new System.EventHandler(this.lblSubtitulo_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(24, 26);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(369, 48);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "INFORME EJECUTIVO";
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.lblMarca.Location = new System.Drawing.Point(27, 9);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(380, 25);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "XAMY TECHNOLOGIES · SOPORTE TI";
            // 
            // pnlIndicadores
            // 
            this.pnlIndicadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.pnlIndicadores.Controls.Add(this.tlpIndicadores);
            this.pnlIndicadores.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlIndicadores.Location = new System.Drawing.Point(0, 136);
            this.pnlIndicadores.Name = "pnlIndicadores";
            this.pnlIndicadores.Padding = new System.Windows.Forms.Padding(24, 10, 24, 10);
            this.pnlIndicadores.Size = new System.Drawing.Size(1050, 98);
            this.pnlIndicadores.TabIndex = 1;
            // 
            // tlpIndicadores
            // 
            this.tlpIndicadores.ColumnCount = 4;
            this.tlpIndicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpIndicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpIndicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpIndicadores.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpIndicadores.Controls.Add(this.pnlCardResueltas, 3, 0);
            this.tlpIndicadores.Controls.Add(this.pnlCardEnProceso, 2, 0);
            this.tlpIndicadores.Controls.Add(this.pnlCardPendientes, 1, 0);
            this.tlpIndicadores.Controls.Add(this.pnlCardTotal, 0, 0);
            this.tlpIndicadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpIndicadores.Location = new System.Drawing.Point(24, 10);
            this.tlpIndicadores.Name = "tlpIndicadores";
            this.tlpIndicadores.RowCount = 1;
            this.tlpIndicadores.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpIndicadores.Size = new System.Drawing.Size(1002, 78);
            this.tlpIndicadores.TabIndex = 0;
            // 
            // pnlCardResueltas
            // 
            this.pnlCardResueltas.BackColor = System.Drawing.Color.White;
            this.pnlCardResueltas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardResueltas.Controls.Add(this.lblResueltasValor);
            this.pnlCardResueltas.Controls.Add(this.lblResueltasTitulo);
            this.pnlCardResueltas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardResueltas.Location = new System.Drawing.Point(753, 3);
            this.pnlCardResueltas.Name = "pnlCardResueltas";
            this.pnlCardResueltas.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.pnlCardResueltas.Size = new System.Drawing.Size(246, 72);
            this.pnlCardResueltas.TabIndex = 3;
            // 
            // lblResueltasValor
            // 
            this.lblResueltasValor.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResueltasValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.lblResueltasValor.Location = new System.Drawing.Point(10, 4);
            this.lblResueltasValor.Name = "lblResueltasValor";
            this.lblResueltasValor.Size = new System.Drawing.Size(224, 40);
            this.lblResueltasValor.TabIndex = 1;
            this.lblResueltasValor.Text = "0";
            this.lblResueltasValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResueltasTitulo
            // 
            this.lblResueltasTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResueltasTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lblResueltasTitulo.Location = new System.Drawing.Point(10, 47);
            this.lblResueltasTitulo.Name = "lblResueltasTitulo";
            this.lblResueltasTitulo.Size = new System.Drawing.Size(224, 20);
            this.lblResueltasTitulo.TabIndex = 0;
            this.lblResueltasTitulo.Text = "Resueltas";
            this.lblResueltasTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCardEnProceso
            // 
            this.pnlCardEnProceso.BackColor = System.Drawing.Color.White;
            this.pnlCardEnProceso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardEnProceso.Controls.Add(this.lblEnProcesoValor);
            this.pnlCardEnProceso.Controls.Add(this.lblEnProcesoTitulo);
            this.pnlCardEnProceso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardEnProceso.Location = new System.Drawing.Point(503, 3);
            this.pnlCardEnProceso.Name = "pnlCardEnProceso";
            this.pnlCardEnProceso.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.pnlCardEnProceso.Size = new System.Drawing.Size(244, 72);
            this.pnlCardEnProceso.TabIndex = 2;
            // 
            // lblEnProcesoValor
            // 
            this.lblEnProcesoValor.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnProcesoValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.lblEnProcesoValor.Location = new System.Drawing.Point(10, 4);
            this.lblEnProcesoValor.Name = "lblEnProcesoValor";
            this.lblEnProcesoValor.Size = new System.Drawing.Size(222, 40);
            this.lblEnProcesoValor.TabIndex = 1;
            this.lblEnProcesoValor.Text = "0";
            this.lblEnProcesoValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEnProcesoTitulo
            // 
            this.lblEnProcesoTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnProcesoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lblEnProcesoTitulo.Location = new System.Drawing.Point(10, 47);
            this.lblEnProcesoTitulo.Name = "lblEnProcesoTitulo";
            this.lblEnProcesoTitulo.Size = new System.Drawing.Size(222, 20);
            this.lblEnProcesoTitulo.TabIndex = 0;
            this.lblEnProcesoTitulo.Text = "En proceso";
            this.lblEnProcesoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCardPendientes
            // 
            this.pnlCardPendientes.BackColor = System.Drawing.Color.White;
            this.pnlCardPendientes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardPendientes.Controls.Add(this.lblPendientesValor);
            this.pnlCardPendientes.Controls.Add(this.lblPendientesTitulo);
            this.pnlCardPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardPendientes.Location = new System.Drawing.Point(253, 3);
            this.pnlCardPendientes.Name = "pnlCardPendientes";
            this.pnlCardPendientes.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlCardPendientes.Size = new System.Drawing.Size(244, 72);
            this.pnlCardPendientes.TabIndex = 1;
            // 
            // lblPendientesValor
            // 
            this.lblPendientesValor.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPendientesValor.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendientesValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.lblPendientesValor.Location = new System.Drawing.Point(12, 8);
            this.lblPendientesValor.Name = "lblPendientesValor";
            this.lblPendientesValor.Size = new System.Drawing.Size(218, 31);
            this.lblPendientesValor.TabIndex = 1;
            this.lblPendientesValor.Text = "0";
            this.lblPendientesValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPendientesValor.Click += new System.EventHandler(this.lblPendientesValor_Click);
            // 
            // lblPendientesTitulo
            // 
            this.lblPendientesTitulo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPendientesTitulo.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendientesTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lblPendientesTitulo.Location = new System.Drawing.Point(12, 39);
            this.lblPendientesTitulo.Name = "lblPendientesTitulo";
            this.lblPendientesTitulo.Size = new System.Drawing.Size(218, 23);
            this.lblPendientesTitulo.TabIndex = 0;
            this.lblPendientesTitulo.Text = "Pendientes";
            this.lblPendientesTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCardTotal
            // 
            this.pnlCardTotal.BackColor = System.Drawing.Color.White;
            this.pnlCardTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCardTotal.Controls.Add(this.lblTotalValor);
            this.pnlCardTotal.Controls.Add(this.lblTotalTitulo);
            this.pnlCardTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCardTotal.Location = new System.Drawing.Point(3, 3);
            this.pnlCardTotal.Name = "pnlCardTotal";
            this.pnlCardTotal.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.pnlCardTotal.Size = new System.Drawing.Size(244, 72);
            this.pnlCardTotal.TabIndex = 0;
            // 
            // lblTotalValor
            // 
            this.lblTotalValor.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.lblTotalValor.Location = new System.Drawing.Point(10, 4);
            this.lblTotalValor.Name = "lblTotalValor";
            this.lblTotalValor.Size = new System.Drawing.Size(222, 40);
            this.lblTotalValor.TabIndex = 1;
            this.lblTotalValor.Text = "0";
            this.lblTotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalTitulo
            // 
            this.lblTotalTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lblTotalTitulo.Location = new System.Drawing.Point(10, 47);
            this.lblTotalTitulo.Name = "lblTotalTitulo";
            this.lblTotalTitulo.Size = new System.Drawing.Size(222, 20);
            this.lblTotalTitulo.TabIndex = 0;
            this.lblTotalTitulo.Text = "Total";
            this.lblTotalTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.pnlContenido.Controls.Add(this.pnlDetalle);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 234);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(24, 6, 24, 10);
            this.pnlContenido.Size = new System.Drawing.Size(1050, 386);
            this.pnlContenido.TabIndex = 2;
            // 
            // pnlDetalle
            // 
            this.pnlDetalle.BackColor = System.Drawing.Color.White;
            this.pnlDetalle.Controls.Add(this.flpIncidencias);
            this.pnlDetalle.Controls.Add(this.pnlTabla);
            this.pnlDetalle.Controls.Add(this.pnlDetalleLinea);
            this.pnlDetalle.Controls.Add(this.pnlDetalleCabecera);
            this.pnlDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetalle.Location = new System.Drawing.Point(24, 6);
            this.pnlDetalle.Name = "pnlDetalle";
            this.pnlDetalle.Padding = new System.Windows.Forms.Padding(20, 14, 20, 14);
            this.pnlDetalle.Size = new System.Drawing.Size(1002, 370);
            this.pnlDetalle.TabIndex = 0;
            // 
            // flpIncidencias
            // 
            this.flpIncidencias.AutoScroll = true;
            this.flpIncidencias.BackColor = System.Drawing.Color.White;
            this.flpIncidencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpIncidencias.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpIncidencias.Location = new System.Drawing.Point(20, 59);
            this.flpIncidencias.Name = "flpIncidencias";
            this.flpIncidencias.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.flpIncidencias.Size = new System.Drawing.Size(962, 297);
            this.flpIncidencias.TabIndex = 3;
            this.flpIncidencias.WrapContents = false;
            // 
            // pnlTabla
            // 
            this.pnlTabla.BackColor = System.Drawing.Color.White;
            this.pnlTabla.Controls.Add(this.lblEstadoVacio);
            this.pnlTabla.Controls.Add(this.dgvIncidenciasReporte);
            this.pnlTabla.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTabla.Location = new System.Drawing.Point(20, 356);
            this.pnlTabla.Name = "pnlTabla";
            this.pnlTabla.Size = new System.Drawing.Size(962, 0);
            this.pnlTabla.TabIndex = 4;
            this.pnlTabla.Visible = false;
            // 
            // lblEstadoVacio
            // 
            this.lblEstadoVacio.BackColor = System.Drawing.Color.White;
            this.lblEstadoVacio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEstadoVacio.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoVacio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lblEstadoVacio.Location = new System.Drawing.Point(0, 0);
            this.lblEstadoVacio.Name = "lblEstadoVacio";
            this.lblEstadoVacio.Size = new System.Drawing.Size(962, 0);
            this.lblEstadoVacio.TabIndex = 1;
            this.lblEstadoVacio.Text = "No hay incidencias para mostrar con los filtros seleccionados.";
            this.lblEstadoVacio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEstadoVacio.Visible = false;
            // 
            // dgvIncidenciasReporte
            // 
            this.dgvIncidenciasReporte.AllowUserToAddRows = false;
            this.dgvIncidenciasReporte.AllowUserToDeleteRows = false;
            this.dgvIncidenciasReporte.AllowUserToResizeRows = false;
            this.dgvIncidenciasReporte.BackgroundColor = System.Drawing.Color.White;
            this.dgvIncidenciasReporte.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIncidenciasReporte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.dgvIncidenciasReporte.ColumnHeadersHeight = 36;
            this.dgvIncidenciasReporte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdentificador,
            this.colTitulo,
            this.colReportadoPor,
            this.colResponsable,
            this.colPrioridad,
            this.colEstado,
            this.colFechaCreacion});
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle32.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(215)))), ((int)(((byte)(189)))));
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIncidenciasReporte.DefaultCellStyle = dataGridViewCellStyle32;
            this.dgvIncidenciasReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIncidenciasReporte.EnableHeadersVisualStyles = false;
            this.dgvIncidenciasReporte.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(224)))), ((int)(((byte)(214)))));
            this.dgvIncidenciasReporte.Location = new System.Drawing.Point(0, 0);
            this.dgvIncidenciasReporte.MultiSelect = false;
            this.dgvIncidenciasReporte.Name = "dgvIncidenciasReporte";
            this.dgvIncidenciasReporte.ReadOnly = true;
            this.dgvIncidenciasReporte.RowHeadersVisible = false;
            this.dgvIncidenciasReporte.RowHeadersWidth = 62;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(243)))));
            this.dgvIncidenciasReporte.RowsDefaultCellStyle = dataGridViewCellStyle33;
            this.dgvIncidenciasReporte.RowTemplate.Height = 30;
            this.dgvIncidenciasReporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIncidenciasReporte.Size = new System.Drawing.Size(962, 0);
            this.dgvIncidenciasReporte.TabIndex = 0;
            // 
            // colIdentificador
            // 
            this.colIdentificador.HeaderText = "ID";
            this.colIdentificador.MinimumWidth = 8;
            this.colIdentificador.Name = "colIdentificador";
            this.colIdentificador.ReadOnly = true;
            this.colIdentificador.Width = 70;
            // 
            // colTitulo
            // 
            this.colTitulo.HeaderText = "Título";
            this.colTitulo.MinimumWidth = 8;
            this.colTitulo.Name = "colTitulo";
            this.colTitulo.ReadOnly = true;
            this.colTitulo.Width = 260;
            // 
            // colReportadoPor
            // 
            this.colReportadoPor.HeaderText = "Reportado por";
            this.colReportadoPor.MinimumWidth = 8;
            this.colReportadoPor.Name = "colReportadoPor";
            this.colReportadoPor.ReadOnly = true;
            this.colReportadoPor.Width = 190;
            // 
            // colResponsable
            // 
            this.colResponsable.HeaderText = "Responsable";
            this.colResponsable.MinimumWidth = 8;
            this.colResponsable.Name = "colResponsable";
            this.colResponsable.ReadOnly = true;
            this.colResponsable.Width = 190;
            // 
            // colPrioridad
            // 
            this.colPrioridad.HeaderText = "Prioridad";
            this.colPrioridad.MinimumWidth = 8;
            this.colPrioridad.Name = "colPrioridad";
            this.colPrioridad.ReadOnly = true;
            this.colPrioridad.Width = 120;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 8;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            this.colEstado.Width = 130;
            // 
            // colFechaCreacion
            // 
            this.colFechaCreacion.HeaderText = "Fecha";
            this.colFechaCreacion.MinimumWidth = 8;
            this.colFechaCreacion.Name = "colFechaCreacion";
            this.colFechaCreacion.ReadOnly = true;
            this.colFechaCreacion.Width = 170;
            // 
            // pnlDetalleLinea
            // 
            this.pnlDetalleLinea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.pnlDetalleLinea.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetalleLinea.Location = new System.Drawing.Point(20, 57);
            this.pnlDetalleLinea.Name = "pnlDetalleLinea";
            this.pnlDetalleLinea.Size = new System.Drawing.Size(962, 2);
            this.pnlDetalleLinea.TabIndex = 2;
            // 
            // pnlDetalleCabecera
            // 
            this.pnlDetalleCabecera.Controls.Add(this.btnExportarHtml);
            this.pnlDetalleCabecera.Controls.Add(this.lnkVerDetalle);
            this.pnlDetalleCabecera.Controls.Add(this.lblConteoRegistros);
            this.pnlDetalleCabecera.Controls.Add(this.lblResumen);
            this.pnlDetalleCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetalleCabecera.Location = new System.Drawing.Point(20, 14);
            this.pnlDetalleCabecera.Name = "pnlDetalleCabecera";
            this.pnlDetalleCabecera.Size = new System.Drawing.Size(962, 43);
            this.pnlDetalleCabecera.TabIndex = 1;
            // 
            // btnExportarHtml
            // 
            this.btnExportarHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportarHtml.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.btnExportarHtml.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnExportarHtml.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarHtml.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarHtml.ForeColor = System.Drawing.Color.White;
            this.btnExportarHtml.Location = new System.Drawing.Point(824, 4);
            this.btnExportarHtml.Name = "btnExportarHtml";
            this.btnExportarHtml.Size = new System.Drawing.Size(135, 36);
            this.btnExportarHtml.TabIndex = 3;
            this.btnExportarHtml.Text = "Exportar HTML";
            this.btnExportarHtml.UseVisualStyleBackColor = false;
            // 
            // lnkVerDetalle
            // 
            this.lnkVerDetalle.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.lnkVerDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkVerDetalle.AutoSize = true;
            this.lnkVerDetalle.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lnkVerDetalle.Location = new System.Drawing.Point(664, 9);
            this.lnkVerDetalle.Name = "lnkVerDetalle";
            this.lnkVerDetalle.Size = new System.Drawing.Size(155, 25);
            this.lnkVerDetalle.TabIndex = 2;
            this.lnkVerDetalle.TabStop = true;
            this.lnkVerDetalle.Text = "Ver detalle técnico";
            this.lnkVerDetalle.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            // 
            // lblConteoRegistros
            // 
            this.lblConteoRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConteoRegistros.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConteoRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.lblConteoRegistros.Location = new System.Drawing.Point(477, 9);
            this.lblConteoRegistros.Name = "lblConteoRegistros";
            this.lblConteoRegistros.Size = new System.Drawing.Size(181, 25);
            this.lblConteoRegistros.TabIndex = 1;
            this.lblConteoRegistros.Text = "00 registros";
            this.lblConteoRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblResumen
            // 
            this.lblResumen.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResumen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.lblResumen.Location = new System.Drawing.Point(0, 8);
            this.lblResumen.Name = "lblResumen";
            this.lblResumen.Size = new System.Drawing.Size(252, 29);
            this.lblResumen.TabIndex = 0;
            this.lblResumen.Text = "Detalle de incidencias";
            this.lblResumen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.pnlAcciones.Controls.Add(this.lblLeyenda);
            this.pnlAcciones.Controls.Add(this.btnCerrar);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAcciones.Location = new System.Drawing.Point(0, 620);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Padding = new System.Windows.Forms.Padding(20, 6, 20, 8);
            this.pnlAcciones.Size = new System.Drawing.Size(1050, 50);
            this.pnlAcciones.TabIndex = 3;
            // 
            // lblLeyenda
            // 
            this.lblLeyenda.AutoSize = true;
            this.lblLeyenda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(115)))), ((int)(((byte)(107)))));
            this.lblLeyenda.Location = new System.Drawing.Point(27, 0);
            this.lblLeyenda.Name = "lblLeyenda";
            this.lblLeyenda.Size = new System.Drawing.Size(356, 25);
            this.lblLeyenda.TabIndex = 2;
            this.lblLeyenda.Text = "Reporte generado por XAMY Technologies";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.White;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(161)))), ((int)(((byte)(92)))));
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.btnCerrar.Location = new System.Drawing.Point(830, -5);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(164, 34);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            // 
            // frmVistaPreviaReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(1050, 670);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.pnlIndicadores);
            this.Controls.Add(this.pnlEncabezado);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(900, 580);
            this.Name = "frmVistaPreviaReporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XAMY Technologies - Informe ejecutivo";
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlIndicadores.ResumeLayout(false);
            this.tlpIndicadores.ResumeLayout(false);
            this.pnlCardResueltas.ResumeLayout(false);
            this.pnlCardEnProceso.ResumeLayout(false);
            this.pnlCardPendientes.ResumeLayout(false);
            this.pnlCardTotal.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlDetalle.ResumeLayout(false);
            this.pnlTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIncidenciasReporte)).EndInit();
            this.pnlDetalleCabecera.ResumeLayout(false);
            this.pnlDetalleCabecera.PerformLayout();
            this.pnlAcciones.ResumeLayout(false);
            this.pnlAcciones.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblFechaGeneracion;
        private System.Windows.Forms.Panel pnlLineaDorada;
        private System.Windows.Forms.Label lblReporteOperativo;
        private System.Windows.Forms.Panel pnlIconoDocumento;
        private System.Windows.Forms.Panel pnlIndicadores;
        private System.Windows.Forms.TableLayoutPanel tlpIndicadores;
        private System.Windows.Forms.Panel pnlCardTotal;
        private System.Windows.Forms.Label lblTotalValor;
        private System.Windows.Forms.Label lblTotalTitulo;
        private System.Windows.Forms.Panel pnlCardPendientes;
        private System.Windows.Forms.Label lblPendientesValor;
        private System.Windows.Forms.Label lblPendientesTitulo;
        private System.Windows.Forms.Panel pnlCardEnProceso;
        private System.Windows.Forms.Label lblEnProcesoValor;
        private System.Windows.Forms.Label lblEnProcesoTitulo;
        private System.Windows.Forms.Panel pnlCardResueltas;
        private System.Windows.Forms.Label lblResueltasValor;
        private System.Windows.Forms.Label lblResueltasTitulo;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlDetalle;
        private System.Windows.Forms.FlowLayoutPanel flpIncidencias;
        private System.Windows.Forms.Label lblResumen;
        private System.Windows.Forms.Panel pnlDetalleLinea;
        private System.Windows.Forms.Panel pnlDetalleCabecera;
        private System.Windows.Forms.LinkLabel lnkVerDetalle;
        private System.Windows.Forms.Label lblConteoRegistros;
        private System.Windows.Forms.Panel pnlTabla;
        private System.Windows.Forms.DataGridView dgvIncidenciasReporte;
        private System.Windows.Forms.Label lblEstadoVacio;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Label lblLeyenda;
        private System.Windows.Forms.Button btnExportarHtml;
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

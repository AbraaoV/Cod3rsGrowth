namespace Cod3rsGrowth.Forms
{
    partial class FormListaDePedido
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
            components = new System.ComponentModel.Container();
            pedidoBindingSource = new BindingSource(components);
            clienteBindingSource = new BindingSource(components);
            buttonRemover = new Button();
            buttonEditar = new Button();
            buttonAdicionar = new Button();
            formaPagamentoDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            valorDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            numeroCartaoDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dataDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            clienteIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dataGridViewPedido = new DataGridView();
            dateTimePickerDataFiltro = new DateTimePicker();
            FiltroFormaPagamento = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            valorMinFiltro = new NumericUpDown();
            valorMaxFiltro = new NumericUpDown();
            buttonFiltrar = new Button();
            buttonLimparFiltro = new Button();
            ((System.ComponentModel.ISupportInitialize)pedidoBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)clienteBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPedido).BeginInit();
            ((System.ComponentModel.ISupportInitialize)valorMinFiltro).BeginInit();
            ((System.ComponentModel.ISupportInitialize)valorMaxFiltro).BeginInit();
            SuspendLayout();
            // 
            // pedidoBindingSource
            // 
            pedidoBindingSource.DataSource = typeof(Dominio.Pedido);
            // 
            // clienteBindingSource
            // 
            clienteBindingSource.DataSource = typeof(Dominio.Cliente);
            // 
            // buttonRemover
            // 
            buttonRemover.Location = new Point(867, 12);
            buttonRemover.Name = "buttonRemover";
            buttonRemover.Size = new Size(94, 29);
            buttonRemover.TabIndex = 1;
            buttonRemover.Text = "Remover";
            buttonRemover.UseVisualStyleBackColor = true;
            buttonRemover.Click += AoClicarNoBotaoRemover;
            // 
            // buttonEditar
            // 
            buttonEditar.Location = new Point(767, 12);
            buttonEditar.Name = "buttonEditar";
            buttonEditar.Size = new Size(94, 29);
            buttonEditar.TabIndex = 2;
            buttonEditar.Text = "Editar";
            buttonEditar.UseVisualStyleBackColor = true;
            buttonEditar.Click += AoClicarNoBotaoEditar;
            // 
            // buttonAdicionar
            // 
            buttonAdicionar.Location = new Point(667, 12);
            buttonAdicionar.Name = "buttonAdicionar";
            buttonAdicionar.Size = new Size(94, 29);
            buttonAdicionar.TabIndex = 3;
            buttonAdicionar.Text = "Adicionar";
            buttonAdicionar.UseVisualStyleBackColor = true;
            buttonAdicionar.Click += AoClicarNoBotaoAdicionar;
            // 
            // formaPagamentoDataGridViewTextBoxColumn
            // 
            formaPagamentoDataGridViewTextBoxColumn.DataPropertyName = "FormaPagamento";
            formaPagamentoDataGridViewTextBoxColumn.HeaderText = "Forma de Pagamento";
            formaPagamentoDataGridViewTextBoxColumn.MinimumWidth = 6;
            formaPagamentoDataGridViewTextBoxColumn.Name = "formaPagamentoDataGridViewTextBoxColumn";
            formaPagamentoDataGridViewTextBoxColumn.Width = 120;
            // 
            // valorDataGridViewTextBoxColumn
            // 
            valorDataGridViewTextBoxColumn.DataPropertyName = "Valor";
            valorDataGridViewTextBoxColumn.HeaderText = "Valor";
            valorDataGridViewTextBoxColumn.MinimumWidth = 6;
            valorDataGridViewTextBoxColumn.Name = "valorDataGridViewTextBoxColumn";
            valorDataGridViewTextBoxColumn.Width = 121;
            // 
            // numeroCartaoDataGridViewTextBoxColumn
            // 
            numeroCartaoDataGridViewTextBoxColumn.DataPropertyName = "NumeroCartao";
            numeroCartaoDataGridViewTextBoxColumn.HeaderText = "Numero do Cartao";
            numeroCartaoDataGridViewTextBoxColumn.MinimumWidth = 6;
            numeroCartaoDataGridViewTextBoxColumn.Name = "numeroCartaoDataGridViewTextBoxColumn";
            numeroCartaoDataGridViewTextBoxColumn.Width = 120;
            // 
            // dataDataGridViewTextBoxColumn
            // 
            dataDataGridViewTextBoxColumn.DataPropertyName = "Data";
            dataDataGridViewTextBoxColumn.HeaderText = "Data";
            dataDataGridViewTextBoxColumn.MinimumWidth = 6;
            dataDataGridViewTextBoxColumn.Name = "dataDataGridViewTextBoxColumn";
            dataDataGridViewTextBoxColumn.Width = 121;
            // 
            // clienteIdDataGridViewTextBoxColumn
            // 
            clienteIdDataGridViewTextBoxColumn.DataPropertyName = "ClienteId";
            clienteIdDataGridViewTextBoxColumn.HeaderText = "ClienteId";
            clienteIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            clienteIdDataGridViewTextBoxColumn.Name = "clienteIdDataGridViewTextBoxColumn";
            clienteIdDataGridViewTextBoxColumn.Width = 120;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.MinimumWidth = 6;
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.Width = 121;
            // 
            // dataGridViewPedido
            // 
            dataGridViewPedido.AutoGenerateColumns = false;
            dataGridViewPedido.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPedido.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, clienteIdDataGridViewTextBoxColumn, dataDataGridViewTextBoxColumn, numeroCartaoDataGridViewTextBoxColumn, valorDataGridViewTextBoxColumn, formaPagamentoDataGridViewTextBoxColumn });
            dataGridViewPedido.DataSource = pedidoBindingSource;
            dataGridViewPedido.Location = new Point(185, 59);
            dataGridViewPedido.Name = "dataGridViewPedido";
            dataGridViewPedido.RowHeadersWidth = 51;
            dataGridViewPedido.RowTemplate.Height = 29;
            dataGridViewPedido.Size = new Size(776, 444);
            dataGridViewPedido.TabIndex = 0;
            dataGridViewPedido.CellFormatting += formatacaoExibicaoListaPedido;
            // 
            // dateTimePickerDataFiltro
            // 
            dateTimePickerDataFiltro.CustomFormat = " ";
            dateTimePickerDataFiltro.Format = DateTimePickerFormat.Custom;
            dateTimePickerDataFiltro.Location = new Point(8, 82);
            dateTimePickerDataFiltro.Name = "dateTimePickerDataFiltro";
            dateTimePickerDataFiltro.Size = new Size(171, 27);
            dateTimePickerDataFiltro.TabIndex = 4;
            dateTimePickerDataFiltro.ValueChanged += AoFiltrarPelaData;
            dateTimePickerDataFiltro.KeyDown += AoApertarDeleteOuBack;
            // 
            // FiltroFormaPagamento
            // 
            FiltroFormaPagamento.FormattingEnabled = true;
            FiltroFormaPagamento.Items.AddRange(new object[] { "Cartão", "Pix", "Boleto", "Todas" });
            FiltroFormaPagamento.Location = new Point(8, 146);
            FiltroFormaPagamento.Name = "FiltroFormaPagamento";
            FiltroFormaPagamento.Size = new Size(171, 28);
            FiltroFormaPagamento.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(8, 18);
            label1.Name = "label1";
            label1.Size = new Size(84, 23);
            label1.TabIndex = 6;
            label1.Text = "Filtrar por";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 59);
            label2.Name = "label2";
            label2.Size = new Size(41, 20);
            label2.TabIndex = 7;
            label2.Text = "Data";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 123);
            label3.Name = "label3";
            label3.Size = new Size(153, 20);
            label3.TabIndex = 8;
            label3.Text = "Forma de pagamento";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 186);
            label4.Name = "label4";
            label4.Size = new Size(43, 20);
            label4.TabIndex = 9;
            label4.Text = "Valor";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 216);
            label5.Name = "label5";
            label5.Size = new Size(60, 20);
            label5.TabIndex = 10;
            label5.Text = "Mínimo";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(8, 269);
            label6.Name = "label6";
            label6.Size = new Size(63, 20);
            label6.TabIndex = 11;
            label6.Text = "Máximo";
            // 
            // valorMinFiltro
            // 
            valorMinFiltro.Location = new Point(8, 239);
            valorMinFiltro.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            valorMinFiltro.Name = "valorMinFiltro";
            valorMinFiltro.Size = new Size(171, 27);
            valorMinFiltro.TabIndex = 12;
            // 
            // valorMaxFiltro
            // 
            valorMaxFiltro.Location = new Point(8, 292);
            valorMaxFiltro.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            valorMaxFiltro.Name = "valorMaxFiltro";
            valorMaxFiltro.Size = new Size(171, 27);
            valorMaxFiltro.TabIndex = 13;
            // 
            // buttonFiltrar
            // 
            buttonFiltrar.Location = new Point(6, 325);
            buttonFiltrar.Name = "buttonFiltrar";
            buttonFiltrar.Size = new Size(94, 29);
            buttonFiltrar.TabIndex = 14;
            buttonFiltrar.Text = "Filtrar";
            buttonFiltrar.UseVisualStyleBackColor = true;
            buttonFiltrar.Click += AoApertarOBotaoFiltrar;
            // 
            // buttonLimparFiltro
            // 
            buttonLimparFiltro.Location = new Point(6, 360);
            buttonLimparFiltro.Name = "buttonLimparFiltro";
            buttonLimparFiltro.Size = new Size(94, 29);
            buttonLimparFiltro.TabIndex = 15;
            buttonLimparFiltro.Text = "Limpar";
            buttonLimparFiltro.UseVisualStyleBackColor = true;
            buttonLimparFiltro.Click += AoApertarOBotaoLimpar;
            // 
            // FormListaDePedido
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(973, 515);
            Controls.Add(buttonLimparFiltro);
            Controls.Add(buttonFiltrar);
            Controls.Add(valorMaxFiltro);
            Controls.Add(valorMinFiltro);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(FiltroFormaPagamento);
            Controls.Add(dateTimePickerDataFiltro);
            Controls.Add(buttonAdicionar);
            Controls.Add(buttonEditar);
            Controls.Add(buttonRemover);
            Controls.Add(dataGridViewPedido);
            Name = "FormListaDePedido";
            Text = "Pedidos";
            Load += FormListaDePedido_Load;
            ((System.ComponentModel.ISupportInitialize)pedidoBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)clienteBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPedido).EndInit();
            ((System.ComponentModel.ISupportInitialize)valorMinFiltro).EndInit();
            ((System.ComponentModel.ISupportInitialize)valorMaxFiltro).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private BindingSource pedidoBindingSource;
        private BindingSource clienteBindingSource;
        private Button buttonRemover;
        private Button buttonEditar;
        private Button buttonAdicionar;
        private DataGridViewTextBoxColumn formaPagamentoDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn valorDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn numeroCartaoDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn clienteIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridView dataGridViewPedido;
        private DateTimePicker dateTimePickerDataFiltro;
        private ComboBox FiltroFormaPagamento;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private NumericUpDown valorMinFiltro;
        private NumericUpDown valorMaxFiltro;
        private Button buttonFiltrar;
        private Button buttonLimparFiltro;
    }
}
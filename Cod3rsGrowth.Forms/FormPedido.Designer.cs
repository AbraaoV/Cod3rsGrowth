namespace Cod3rsGrowth.Forms
{
    partial class FormPedido
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
            dataGridViewPedido = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            clienteIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dataDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            numeroCartaoDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            valorDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            formaPagamentoDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            pedidoBindingSource = new BindingSource(components);
            clienteBindingSource = new BindingSource(components);
            buttonRemover = new Button();
            buttonEditar = new Button();
            buttonAdicionar = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPedido).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pedidoBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)clienteBindingSource).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewPedido
            // 
            dataGridViewPedido.AutoGenerateColumns = false;
            dataGridViewPedido.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPedido.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, clienteIdDataGridViewTextBoxColumn, dataDataGridViewTextBoxColumn, numeroCartaoDataGridViewTextBoxColumn, valorDataGridViewTextBoxColumn, formaPagamentoDataGridViewTextBoxColumn });
            dataGridViewPedido.DataSource = pedidoBindingSource;
            dataGridViewPedido.Location = new Point(12, 61);
            dataGridViewPedido.Name = "dataGridViewPedido";
            dataGridViewPedido.RowHeadersWidth = 51;
            dataGridViewPedido.RowTemplate.Height = 29;
            dataGridViewPedido.Size = new Size(776, 377);
            dataGridViewPedido.TabIndex = 0;
            dataGridViewPedido.CellContentClick += dataGridView1_CellContentClick;
            dataGridViewPedido.CellFormatting += dataGridViewPedido_CellFormatting;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.MinimumWidth = 6;
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.Width = 121;
            // 
            // clienteIdDataGridViewTextBoxColumn
            // 
            clienteIdDataGridViewTextBoxColumn.DataPropertyName = "ClienteId";
            clienteIdDataGridViewTextBoxColumn.HeaderText = "ClienteId";
            clienteIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            clienteIdDataGridViewTextBoxColumn.Name = "clienteIdDataGridViewTextBoxColumn";
            clienteIdDataGridViewTextBoxColumn.Width = 120;
            // 
            // dataDataGridViewTextBoxColumn
            // 
            dataDataGridViewTextBoxColumn.DataPropertyName = "Data";
            dataDataGridViewTextBoxColumn.HeaderText = "Data";
            dataDataGridViewTextBoxColumn.MinimumWidth = 6;
            dataDataGridViewTextBoxColumn.Name = "dataDataGridViewTextBoxColumn";
            dataDataGridViewTextBoxColumn.Width = 121;
            // 
            // numeroCartaoDataGridViewTextBoxColumn
            // 
            numeroCartaoDataGridViewTextBoxColumn.DataPropertyName = "NumeroCartao";
            numeroCartaoDataGridViewTextBoxColumn.HeaderText = "Numero do Cartao";
            numeroCartaoDataGridViewTextBoxColumn.MinimumWidth = 6;
            numeroCartaoDataGridViewTextBoxColumn.Name = "numeroCartaoDataGridViewTextBoxColumn";
            numeroCartaoDataGridViewTextBoxColumn.Width = 120;
            // 
            // valorDataGridViewTextBoxColumn
            // 
            valorDataGridViewTextBoxColumn.DataPropertyName = "Valor";
            valorDataGridViewTextBoxColumn.HeaderText = "Valor";
            valorDataGridViewTextBoxColumn.MinimumWidth = 6;
            valorDataGridViewTextBoxColumn.Name = "valorDataGridViewTextBoxColumn";
            valorDataGridViewTextBoxColumn.Width = 121;
            // 
            // formaPagamentoDataGridViewTextBoxColumn
            // 
            formaPagamentoDataGridViewTextBoxColumn.DataPropertyName = "FormaPagamento";
            formaPagamentoDataGridViewTextBoxColumn.HeaderText = "Forma de Pagamento";
            formaPagamentoDataGridViewTextBoxColumn.MinimumWidth = 6;
            formaPagamentoDataGridViewTextBoxColumn.Name = "formaPagamentoDataGridViewTextBoxColumn";
            formaPagamentoDataGridViewTextBoxColumn.Width = 120;
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
            buttonRemover.Location = new Point(694, 12);
            buttonRemover.Name = "buttonRemover";
            buttonRemover.Size = new Size(94, 29);
            buttonRemover.TabIndex = 1;
            buttonRemover.Text = "Remover";
            buttonRemover.UseVisualStyleBackColor = true;
            // 
            // buttonEditar
            // 
            buttonEditar.Location = new Point(594, 12);
            buttonEditar.Name = "buttonEditar";
            buttonEditar.Size = new Size(94, 29);
            buttonEditar.TabIndex = 2;
            buttonEditar.Text = "Editar";
            buttonEditar.UseVisualStyleBackColor = true;
            // 
            // buttonAdicionar
            // 
            buttonAdicionar.Location = new Point(494, 12);
            buttonAdicionar.Name = "buttonAdicionar";
            buttonAdicionar.Size = new Size(94, 29);
            buttonAdicionar.TabIndex = 3;
            buttonAdicionar.Text = "Adicionar";
            buttonAdicionar.UseVisualStyleBackColor = true;
            buttonAdicionar.Click += buttonAdicionar_Click;
            // 
            // FormPedido
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonAdicionar);
            Controls.Add(buttonEditar);
            Controls.Add(buttonRemover);
            Controls.Add(dataGridViewPedido);
            Name = "FormPedido";
            Text = "FormPedido";
            ((System.ComponentModel.ISupportInitialize)dataGridViewPedido).EndInit();
            ((System.ComponentModel.ISupportInitialize)pedidoBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)clienteBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewPedido;
        private BindingSource pedidoBindingSource;
        private BindingSource clienteBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn clienteIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn dataDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn numeroCartaoDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn valorDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn formaPagamentoDataGridViewTextBoxColumn;
        private Button buttonRemover;
        private Button buttonEditar;
        private Button buttonAdicionar;
    }
}
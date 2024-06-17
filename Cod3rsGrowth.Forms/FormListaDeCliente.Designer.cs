namespace Cod3rsGrowth.Forms
{
    partial class FormListaDeCliente
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            label1 = new Label();
            clienteBindingSource = new BindingSource(components);
            dataGridViewCliente = new DataGridView();
            nomeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            cpfDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            cnpjDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            tipoDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            clienteBindingSource1 = new BindingSource(components);
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            pedidosToolStripMenuItem = new ToolStripMenuItem();
            textBoxFiltroNome = new TextBox();
            comboBoxFiltroTipo = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)clienteBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCliente).BeginInit();
            ((System.ComponentModel.ISupportInitialize)clienteBindingSource1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(330, 174);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 0;
            // 
            // clienteBindingSource
            // 
            clienteBindingSource.DataSource = typeof(Dominio.Cliente);
            // 
            // dataGridViewCliente
            // 
            dataGridViewCliente.AutoGenerateColumns = false;
            dataGridViewCliente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCliente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCliente.Columns.AddRange(new DataGridViewColumn[] { nomeDataGridViewTextBoxColumn, idDataGridViewTextBoxColumn, cpfDataGridViewTextBoxColumn, cnpjDataGridViewTextBoxColumn, tipoDataGridViewTextBoxColumn });
            dataGridViewCliente.DataSource = clienteBindingSource1;
            dataGridViewCliente.Location = new Point(12, 60);
            dataGridViewCliente.Name = "dataGridViewCliente";
            dataGridViewCliente.RowHeadersWidth = 51;
            dataGridViewCliente.RowTemplate.Height = 29;
            dataGridViewCliente.Size = new Size(776, 376);
            dataGridViewCliente.TabIndex = 1;
            dataGridViewCliente.CellFormatting += formatacaoExibicaoListaCliente;
            dataGridViewCliente.CellMouseDown += AoClicarComOBotaoDireitoNaListaPedido;
            // 
            // nomeDataGridViewTextBoxColumn
            // 
            nomeDataGridViewTextBoxColumn.DataPropertyName = "Nome";
            nomeDataGridViewTextBoxColumn.HeaderText = "Nome";
            nomeDataGridViewTextBoxColumn.MinimumWidth = 6;
            nomeDataGridViewTextBoxColumn.Name = "nomeDataGridViewTextBoxColumn";
            nomeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.MinimumWidth = 6;
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cpfDataGridViewTextBoxColumn
            // 
            cpfDataGridViewTextBoxColumn.DataPropertyName = "Cpf";
            dataGridViewCellStyle2.Format = "###\\.###\\.###-##";
            cpfDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            cpfDataGridViewTextBoxColumn.HeaderText = "Cpf";
            cpfDataGridViewTextBoxColumn.MinimumWidth = 6;
            cpfDataGridViewTextBoxColumn.Name = "cpfDataGridViewTextBoxColumn";
            cpfDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cnpjDataGridViewTextBoxColumn
            // 
            cnpjDataGridViewTextBoxColumn.DataPropertyName = "Cnpj";
            cnpjDataGridViewTextBoxColumn.HeaderText = "Cnpj";
            cnpjDataGridViewTextBoxColumn.MinimumWidth = 6;
            cnpjDataGridViewTextBoxColumn.Name = "cnpjDataGridViewTextBoxColumn";
            cnpjDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tipoDataGridViewTextBoxColumn
            // 
            tipoDataGridViewTextBoxColumn.DataPropertyName = "Tipo";
            tipoDataGridViewTextBoxColumn.HeaderText = "Tipo";
            tipoDataGridViewTextBoxColumn.MinimumWidth = 6;
            tipoDataGridViewTextBoxColumn.Name = "tipoDataGridViewTextBoxColumn";
            tipoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // clienteBindingSource1
            // 
            clienteBindingSource1.DataSource = typeof(Dominio.Cliente);
            // 
            // button1
            // 
            button1.Location = new Point(694, 12);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "Remover";
            button1.UseVisualStyleBackColor = true;
            button1.Click += AoClicarNoBotaoRemover;
            // 
            // button2
            // 
            button2.Location = new Point(594, 12);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 0;
            button2.Text = "Editar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += AoClicarNoBotaoEditar;
            // 
            // button3
            // 
            button3.Location = new Point(494, 12);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 0;
            button3.Text = "Adicionar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += AoClicarNoBotaoAdicionar;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { pedidosToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(131, 28);
            // 
            // pedidosToolStripMenuItem
            // 
            pedidosToolStripMenuItem.Name = "pedidosToolStripMenuItem";
            pedidosToolStripMenuItem.Size = new Size(130, 24);
            pedidosToolStripMenuItem.Text = "Pedidos";
            // 
            // textBoxFiltroNome
            // 
            textBoxFiltroNome.Location = new Point(12, 14);
            textBoxFiltroNome.Name = "textBoxFiltroNome";
            textBoxFiltroNome.PlaceholderText = "Pesquisar por nome";
            textBoxFiltroNome.Size = new Size(259, 27);
            textBoxFiltroNome.TabIndex = 2;
            textBoxFiltroNome.TextChanged += AoFiltrarPorNome;
            // 
            // comboBoxFiltroTipo
            // 
            comboBoxFiltroTipo.FormattingEnabled = true;
            comboBoxFiltroTipo.Items.AddRange(new object[] { "Todos Clientes", "Pessoa Física", "Pessoa Jurídica" });
            comboBoxFiltroTipo.Location = new Point(286, 14);
            comboBoxFiltroTipo.Name = "comboBoxFiltroTipo";
            comboBoxFiltroTipo.Size = new Size(193, 28);
            comboBoxFiltroTipo.TabIndex = 3;
            comboBoxFiltroTipo.SelectedIndexChanged += AoFiltrarPelaComboBox;
            // 
            // FormListaDeCliente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 448);
            Controls.Add(comboBoxFiltroTipo);
            Controls.Add(textBoxFiltroNome);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridViewCliente);
            Controls.Add(label1);
            Name = "FormListaDeCliente";
            Text = "Clientes";
            Load += FormListaDeCliente_Load;
            ((System.ComponentModel.ISupportInitialize)clienteBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCliente).EndInit();
            ((System.ComponentModel.ISupportInitialize)clienteBindingSource1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private BindingSource clienteBindingSource;
        private DataGridView dataGridViewCliente;
        private Button button1;
        private Button button2;
        private Button button3;
        private BindingSource clienteBindingSource1;
        private DataGridViewTextBoxColumn nomeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn cpfDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn cnpjDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tipoDataGridViewTextBoxColumn;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem pedidosToolStripMenuItem;
        private TextBox textBoxFiltroNome;
        private ComboBox comboBoxFiltroTipo;
    }
}

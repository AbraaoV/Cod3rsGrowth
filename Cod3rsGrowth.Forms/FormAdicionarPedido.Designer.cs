namespace Cod3rsGrowth.Forms
{
    partial class FormAdicionarPedido
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
            labelData = new Label();
            labelCartao = new Label();
            label3 = new Label();
            labelValor = new Label();
            dateTimePickerPedido = new DateTimePicker();
            comboBoxFormaPagamento = new ComboBox();
            clienteBindingSource = new BindingSource(components);
            buttonAdicionar = new Button();
            buttonCancelar = new Button();
            numericUpDownValor = new NumericUpDown();
            maskedTextBoxCartao = new MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)clienteBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownValor).BeginInit();
            SuspendLayout();
            // 
            // labelData
            // 
            labelData.AutoSize = true;
            labelData.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            labelData.Location = new Point(12, 9);
            labelData.Name = "labelData";
            labelData.Size = new Size(46, 23);
            labelData.TabIndex = 0;
            labelData.Text = "Data";
            labelData.Click += label1_Click;
            // 
            // labelCartao
            // 
            labelCartao.AutoSize = true;
            labelCartao.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            labelCartao.Location = new Point(12, 65);
            labelCartao.Name = "labelCartao";
            labelCartao.Size = new Size(134, 23);
            labelCartao.TabIndex = 1;
            labelCartao.Text = "Numero Cartão ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 178);
            label3.Name = "label3";
            label3.Size = new Size(174, 23);
            label3.TabIndex = 2;
            label3.Text = "Forma de Pagamento";
            // 
            // labelValor
            // 
            labelValor.AutoSize = true;
            labelValor.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            labelValor.Location = new Point(12, 122);
            labelValor.Name = "labelValor";
            labelValor.Size = new Size(49, 23);
            labelValor.TabIndex = 3;
            labelValor.Text = "Valor";
            // 
            // dateTimePickerPedido
            // 
            dateTimePickerPedido.Checked = false;
            dateTimePickerPedido.Location = new Point(12, 35);
            dateTimePickerPedido.Name = "dateTimePickerPedido";
            dateTimePickerPedido.Size = new Size(360, 27);
            dateTimePickerPedido.TabIndex = 4;
            dateTimePickerPedido.ValueChanged += dateTimePickerPedido_ValueChanged;
            // 
            // comboBoxFormaPagamento
            // 
            comboBoxFormaPagamento.FormattingEnabled = true;
            comboBoxFormaPagamento.Items.AddRange(new object[] { "Cartão de Credito", "Pix", "Boleto" });
            comboBoxFormaPagamento.Location = new Point(12, 204);
            comboBoxFormaPagamento.Name = "comboBoxFormaPagamento";
            comboBoxFormaPagamento.Size = new Size(360, 28);
            comboBoxFormaPagamento.TabIndex = 7;
            comboBoxFormaPagamento.SelectedIndexChanged += comboBoxFormaPagamento_SelectedIndexChanged;
            // 
            // clienteBindingSource
            // 
            clienteBindingSource.DataSource = typeof(Dominio.Cliente);
            // 
            // buttonAdicionar
            // 
            buttonAdicionar.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            buttonAdicionar.Location = new Point(12, 344);
            buttonAdicionar.Name = "buttonAdicionar";
            buttonAdicionar.Size = new Size(94, 29);
            buttonAdicionar.TabIndex = 8;
            buttonAdicionar.Text = "Adicionar";
            buttonAdicionar.UseVisualStyleBackColor = true;
            buttonAdicionar.Click += buttonAdicionar_Click;
            // 
            // buttonCancelar
            // 
            buttonCancelar.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCancelar.Location = new Point(278, 344);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new Size(94, 29);
            buttonCancelar.TabIndex = 9;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.UseVisualStyleBackColor = true;
            // 
            // numericUpDownValor
            // 
            numericUpDownValor.DecimalPlaces = 2;
            numericUpDownValor.Location = new Point(12, 148);
            numericUpDownValor.Name = "numericUpDownValor";
            numericUpDownValor.Size = new Size(360, 27);
            numericUpDownValor.TabIndex = 10;
            // 
            // maskedTextBoxCartao
            // 
            maskedTextBoxCartao.Location = new Point(12, 91);
            maskedTextBoxCartao.Mask = "0000 0000 0000 0000";
            maskedTextBoxCartao.Name = "maskedTextBoxCartao";
            maskedTextBoxCartao.Size = new Size(360, 27);
            maskedTextBoxCartao.TabIndex = 11;
            maskedTextBoxCartao.MaskInputRejected += maskedTextBoxCartao_MaskInputRejected;
            // 
            // FormAdicionarPedido
            // 
            AcceptButton = buttonAdicionar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancelar;
            ClientSize = new Size(384, 412);
            Controls.Add(maskedTextBoxCartao);
            Controls.Add(numericUpDownValor);
            Controls.Add(buttonCancelar);
            Controls.Add(buttonAdicionar);
            Controls.Add(comboBoxFormaPagamento);
            Controls.Add(dateTimePickerPedido);
            Controls.Add(labelValor);
            Controls.Add(label3);
            Controls.Add(labelCartao);
            Controls.Add(labelData);
            Name = "FormAdicionarPedido";
            Text = "FormAdicionarPedido";
            Load += FormAdicionarPedido_Load;
            ((System.ComponentModel.ISupportInitialize)clienteBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownValor).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelData;
        private Label labelCartao;
        private Label label3;
        private Label labelValor;
        private DateTimePicker dateTimePickerPedido;
        private ComboBox comboBoxFormaPagamento;
        private BindingSource clienteBindingSource;
        private Button buttonAdicionar;
        private Button buttonCancelar;
        private NumericUpDown numericUpDownValor;
        private MaskedTextBox maskedTextBoxCartao;
    }
}
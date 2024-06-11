namespace Cod3rsGrowth.Forms
{
    partial class FormCadastroDeCliente
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
            labelNome = new Label();
            labelCnpj = new Label();
            labelCpf = new Label();
            labelPessoa = new Label();
            textBoxCpf = new TextBox();
            textBoxCnpj = new TextBox();
            checkBoxTipoFisica = new CheckBox();
            checkBoxTipoJuridica = new CheckBox();
            textBoxNome = new TextBox();
            buttonAdicionar = new Button();
            buttonCancelar = new Button();
            SuspendLayout();
            // 
            // labelNome
            // 
            labelNome.AutoSize = true;
            labelNome.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            labelNome.Location = new Point(12, 9);
            labelNome.Name = "labelNome";
            labelNome.Size = new Size(57, 23);
            labelNome.TabIndex = 0;
            labelNome.Text = "Nome";
            labelNome.Click += labelNome_Click;
            // 
            // labelCnpj
            // 
            labelCnpj.AutoSize = true;
            labelCnpj.Location = new Point(12, 128);
            labelCnpj.Name = "labelCnpj";
            labelCnpj.Size = new Size(45, 23);
            labelCnpj.TabIndex = 1;
            labelCnpj.Text = "Cnpj";
            labelCnpj.Click += labelCnpj_Click;
            // 
            // labelCpf
            // 
            labelCpf.AutoSize = true;
            labelCpf.Location = new Point(12, 69);
            labelCpf.Name = "labelCpf";
            labelCpf.Size = new Size(36, 23);
            labelCpf.TabIndex = 2;
            labelCpf.Text = "Cpf";
            labelCpf.Click += labelCpf_Click;
            // 
            // labelPessoa
            // 
            labelPessoa.AutoSize = true;
            labelPessoa.Location = new Point(10, 187);
            labelPessoa.Name = "labelPessoa";
            labelPessoa.Size = new Size(61, 23);
            labelPessoa.TabIndex = 3;
            labelPessoa.Text = "Pessoa";
            labelPessoa.Click += labelPessoa_Click;
            // 
            // textBoxCpf
            // 
            textBoxCpf.Location = new Point(12, 95);
            textBoxCpf.Name = "textBoxCpf";
            textBoxCpf.Size = new Size(360, 30);
            textBoxCpf.TabIndex = 5;
            textBoxCpf.TextChanged += textBoxCpf_TextChanged;
            // 
            // textBoxCnpj
            // 
            textBoxCnpj.Location = new Point(14, 154);
            textBoxCnpj.Name = "textBoxCnpj";
            textBoxCnpj.Size = new Size(360, 30);
            textBoxCnpj.TabIndex = 6;
            textBoxCnpj.TextChanged += textBoxCnpj_TextChanged;
            // 
            // checkBoxTipoFisica
            // 
            checkBoxTipoFisica.AutoSize = true;
            checkBoxTipoFisica.Location = new Point(14, 213);
            checkBoxTipoFisica.Name = "checkBoxTipoFisica";
            checkBoxTipoFisica.Size = new Size(72, 27);
            checkBoxTipoFisica.TabIndex = 7;
            checkBoxTipoFisica.Text = "Física";
            checkBoxTipoFisica.UseVisualStyleBackColor = true;
            checkBoxTipoFisica.CheckedChanged += checkBoxFisica_CheckedChanged;
            // 
            // checkBoxTipoJuridica
            // 
            checkBoxTipoJuridica.AutoSize = true;
            checkBoxTipoJuridica.Location = new Point(92, 213);
            checkBoxTipoJuridica.Name = "checkBoxTipoJuridica";
            checkBoxTipoJuridica.Size = new Size(89, 27);
            checkBoxTipoJuridica.TabIndex = 8;
            checkBoxTipoJuridica.Text = "Jurídica";
            checkBoxTipoJuridica.UseVisualStyleBackColor = true;
            checkBoxTipoJuridica.CheckedChanged += checkBoxJuridica_CheckedChanged;
            // 
            // textBoxNome
            // 
            textBoxNome.Location = new Point(10, 36);
            textBoxNome.Name = "textBoxNome";
            textBoxNome.Size = new Size(358, 30);
            textBoxNome.TabIndex = 9;
            textBoxNome.TextChanged += textBoxNome_TextChanged;
            // 
            // buttonAdicionar
            // 
            buttonAdicionar.Location = new Point(10, 346);
            buttonAdicionar.Name = "buttonAdicionar";
            buttonAdicionar.Size = new Size(104, 36);
            buttonAdicionar.TabIndex = 10;
            buttonAdicionar.Text = "Adicionar";
            buttonAdicionar.UseVisualStyleBackColor = true;
            buttonAdicionar.Click += button1_Click;
            // 
            // buttonCancelar
            // 
            buttonCancelar.Location = new Point(257, 346);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new Size(115, 36);
            buttonCancelar.TabIndex = 11;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.UseVisualStyleBackColor = true;
            // 
            // FormCadastroDeCliente
            // 
            AcceptButton = buttonAdicionar;
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancelar;
            ClientSize = new Size(384, 412);
            Controls.Add(buttonCancelar);
            Controls.Add(buttonAdicionar);
            Controls.Add(textBoxNome);
            Controls.Add(checkBoxTipoJuridica);
            Controls.Add(checkBoxTipoFisica);
            Controls.Add(textBoxCnpj);
            Controls.Add(textBoxCpf);
            Controls.Add(labelPessoa);
            Controls.Add(labelCpf);
            Controls.Add(labelCnpj);
            Controls.Add(labelNome);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "FormCadastroDeCliente";
            Text = "Cadastro de Cliente";
            Load += Cadastro_De_Cliente_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelNome;
        private Label labelCnpj;
        private Label labelCpf;
        private Label labelPessoa;
        private TextBox textBoxCpf;
        private TextBox textBoxCnpj;
        private CheckBox checkBoxTipoFisica;
        private CheckBox checkBoxTipoJuridica;
        private TextBox textBoxNome;
        private Button buttonAdicionar;
        private Button buttonCancelar;
    }
}
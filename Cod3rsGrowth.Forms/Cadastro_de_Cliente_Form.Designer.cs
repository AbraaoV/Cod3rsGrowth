namespace Cod3rsGrowth.Forms
{
    partial class Cadastro_de_Cliente_Form
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBoxCpf = new TextBox();
            textBoxCnpj = new TextBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            textBoxNome = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(14, 10);
            label1.Name = "label1";
            label1.Size = new Size(57, 23);
            label1.TabIndex = 0;
            label1.Text = "Nome";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 128);
            label2.Name = "label2";
            label2.Size = new Size(45, 23);
            label2.TabIndex = 1;
            label2.Text = "Cnpj";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 69);
            label3.Name = "label3";
            label3.Size = new Size(36, 23);
            label3.TabIndex = 2;
            label3.Text = "Cpf";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 187);
            label4.Name = "label4";
            label4.Size = new Size(61, 23);
            label4.TabIndex = 3;
            label4.Text = "Pessoa";
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
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(14, 213);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(72, 27);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Física";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(92, 213);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(89, 27);
            checkBox2.TabIndex = 8;
            checkBox2.Text = "Jurídica";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // textBoxNome
            // 
            textBoxNome.Location = new Point(10, 36);
            textBoxNome.Name = "textBoxNome";
            textBoxNome.Size = new Size(358, 30);
            textBoxNome.TabIndex = 9;
            textBoxNome.TextChanged += textBoxNome_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(10, 346);
            button1.Name = "button1";
            button1.Size = new Size(104, 36);
            button1.TabIndex = 10;
            button1.Text = "Adicionar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(257, 346);
            button2.Name = "button2";
            button2.Size = new Size(115, 36);
            button2.TabIndex = 11;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = true;
            // 
            // Cadastro_de_Cliente_Form
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 412);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBoxNome);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(textBoxCnpj);
            Controls.Add(textBoxCpf);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "Cadastro_de_Cliente_Form";
            Text = "Cadastro de Cliente";
            Load += Cadastro_De_Cliente_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBoxCpf;
        private TextBox textBoxCnpj;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private TextBox textBoxNome;
        private Button button1;
        private Button button2;
    }
}
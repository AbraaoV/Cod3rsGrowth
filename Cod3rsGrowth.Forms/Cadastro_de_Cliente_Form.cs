using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cod3rsGrowth.Forms
{
    public partial class Cadastro_de_Cliente_Form : Form
    {
        private readonly ServicoCliente _servicoCliente;
        public Cadastro_de_Cliente_Form(ServicoCliente servicoCliente)
        {
            _servicoCliente = servicoCliente;
            InitializeComponent();
        }

        private void Cadastro_De_Cliente_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var clienteAdicionado = new Cliente()
            {
                Nome = textBoxNome.Text,
                Id = null,
                Cpf = textBoxCpf.Text,
                Cnpj = textBoxCnpj.Text,
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            _servicoCliente.Adicionar(clienteAdicionado);
        }

        private void textBoxNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxCpf_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxCnpj_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

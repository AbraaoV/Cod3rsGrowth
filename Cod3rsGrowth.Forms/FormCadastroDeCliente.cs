using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;

namespace Cod3rsGrowth.Forms
{
    public partial class FormCadastroDeCliente : Form
    {
        private readonly ServicoCliente _servicoCliente;
        public FormCadastroDeCliente(ServicoCliente servicoCliente)
        {
            _servicoCliente = servicoCliente;
            InitializeComponent();
        }

        private void Cadastro_De_Cliente_Load(object sender, EventArgs e)
        {

        }

        private void labelNome_Click(object sender, EventArgs e)
        {

        }

        private void labelCnpj_Click(object sender, EventArgs e)
        {

        }

        private void labelPessoa_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxFisica_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void checkBoxJuridica_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void AoClicarNoBotaoDeAdicionar(object sender, EventArgs e)
        {
            try
            {
                Cliente.TipoDeCliente tipo = Constantes.ENUM_INDEFINIDO;
                if (checkBoxTipoFisica.Checked)
                {
                    tipo = Cliente.TipoDeCliente.Fisica;
                }
                else if (checkBoxTipoJuridica.Checked)
                {
                    tipo = Cliente.TipoDeCliente.Juridica;
                }

                var clienteAdicionado = new Cliente()
                {
                    Nome = textBoxNome.Text,
                    Cpf = maskedTextBoxCpf.Text.Replace(".", string.Empty).Replace("-", string.Empty),
                    Cnpj = maskedTextBoxCnpj.Text.Replace(".", string.Empty).Replace("/", string.Empty).Replace("-", string.Empty),
                    Tipo = tipo,
                };

                _servicoCliente.Adicionar(clienteAdicionado);
                DialogResult = DialogResult.OK;
            }
            catch (ValidationException ex)
            {
                string mensagemErro = "";

                foreach (var erro in ex.Errors)
                {
                    mensagemErro += erro.ErrorMessage + "\n";
                }
                MessageBox.Show(mensagemErro);
            }


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

        private void labelCpf_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBoxCpf_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBoxCnpj_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}

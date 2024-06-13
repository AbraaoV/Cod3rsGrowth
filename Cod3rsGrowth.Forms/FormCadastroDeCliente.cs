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
        private void checkBoxFisica_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTipoFisica.Checked)
            {
                checkBoxTipoJuridica.Visible = false;
            }
            else if (checkBoxTipoFisica.Checked == false)
            {
                checkBoxTipoJuridica.Visible = true;
            }

        }
        private void checkBoxJuridica_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTipoJuridica.Checked)
            {
                checkBoxTipoFisica.Visible = false;
            }
            if (checkBoxTipoJuridica.Checked == false)
            {
                checkBoxTipoFisica.Visible = true;
            }
        }
        private void AoClicarNoBotaoDeAdicionar(object sender, EventArgs e)
        {
            try
            {
                Cliente.TipoDeCliente tipo = Constantes.ENUM_INDEFINIDO;
                if (checkBoxTipoFisica.Checked)
                {
                    tipo = Cliente.TipoDeCliente.Fisica;
                    checkBoxTipoJuridica.Visible = false;
                }
                else if (checkBoxTipoJuridica.Checked)
                {
                    tipo = Cliente.TipoDeCliente.Juridica;
                    checkBoxTipoFisica.Visible = false;
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
    }
}

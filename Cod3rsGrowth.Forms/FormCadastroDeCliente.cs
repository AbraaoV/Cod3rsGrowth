using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;

namespace Cod3rsGrowth.Forms
{
    public partial class FormCadastroDeCliente : Form
    {
        private readonly ServicoCliente _servicoCliente;
        private Cliente.TipoDeCliente _tipoCliente;
        public FormCadastroDeCliente(ServicoCliente servicoCliente)
        {
            _servicoCliente = servicoCliente;
            InitializeComponent();
        }
        private void AoChecarACaixaFisica(object sender, EventArgs e)
        {
            DeixarCheckBoxDesmarcadaInvisivel();

        }
        private void AoChecarACaixaJuridica(object sender, EventArgs e)
        {
            DeixarCheckBoxDesmarcadaInvisivel();
        }
        private void AoClicarNoBotaoDeAdicionar(object sender, EventArgs e)
        {
            try
            {
                ObterTipoDeCliente();

                var clienteAdicionado = new Cliente()
                {
                    Nome = textBoxNome.Text,
                    Cpf = maskedTextBoxCpf.Text.Replace(".", string.Empty).Replace("-", string.Empty),
                    Cnpj = maskedTextBoxCnpj.Text.Replace(".", string.Empty).Replace("/", string.Empty).Replace("-", string.Empty),
                    Tipo = _tipoCliente,
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
        private void ObterTipoDeCliente()
        {
            _tipoCliente = Constantes.ENUM_INDEFINIDO;
            if (checkBoxTipoFisica.Checked)
            {
                _tipoCliente = Cliente.TipoDeCliente.Fisica;
                checkBoxTipoJuridica.Visible = false;
            }
            else if (checkBoxTipoJuridica.Checked)
            {
                _tipoCliente = Cliente.TipoDeCliente.Juridica;
                checkBoxTipoFisica.Visible = false;
            }
        }
        private void DeixarCheckBoxDesmarcadaInvisivel()
        {
            if (checkBoxTipoFisica.Checked)
            {
                checkBoxTipoJuridica.Visible = false;
            }
            else if (checkBoxTipoFisica.Checked == false)
            {
                checkBoxTipoJuridica.Visible = true;
            }
            if (checkBoxTipoJuridica.Checked)
            {
                checkBoxTipoFisica.Visible = false;
            }
            else if (checkBoxTipoJuridica.Checked == false)
            {
                checkBoxTipoFisica.Visible = true;
            }
        }
    }
}

using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;

namespace Cod3rsGrowth.Forms
{
    public partial class FormEditarCliente : Form
    {
        private readonly ServicoCliente _servicoCliente;
        private readonly int _clienteId;
        public FormEditarCliente(ServicoCliente servicoCliente, int clienteId)
        {
            _servicoCliente = servicoCliente;
            _clienteId = clienteId;
            InitializeComponent();
        }
        private void AoChecarACaixaFisica(object sender, EventArgs e)
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
        private void AoChecarACaixaJuridica(object sender, EventArgs e)
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
                }
                else if (checkBoxTipoJuridica.Checked)
                {
                    tipo = Cliente.TipoDeCliente.Juridica;
                }

                var clienteAtualizado = new Cliente()
                {
                    Nome = textBoxNome.Text,
                    Cpf = maskedTextBoxCpf.Text.Replace(".", string.Empty).Replace("-", string.Empty),
                    Cnpj = maskedTextBoxCnpj.Text.Replace(".", string.Empty).Replace("/", string.Empty).Replace("-", string.Empty),
                    Tipo = tipo,
                };

                _servicoCliente.Atualizar(_clienteId, clienteAtualizado);
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
        private void FormEditarCliente_Load(object sender, EventArgs e)
        {
            Cliente cliente = _servicoCliente.ObterPorId(_clienteId);
            textBoxNome.Text = cliente.Nome;
            maskedTextBoxCpf.Text = cliente.Cpf;
            maskedTextBoxCnpj.Text = cliente.Cnpj;
            if (cliente.Tipo == Cliente.TipoDeCliente.Fisica)
            {
                checkBoxTipoFisica.Checked = true;
            }
            else if (cliente.Tipo == Cliente.TipoDeCliente.Juridica)
            {
                checkBoxTipoJuridica.Checked = true;
            }
        }
    }
}

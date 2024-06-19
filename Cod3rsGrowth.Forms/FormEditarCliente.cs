using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;

namespace Cod3rsGrowth.Forms
{
    public partial class FormEditarCliente : Form
    {
        private readonly ServicoCliente _servicoCliente;
        private readonly int _clienteId;
        private Cliente.TipoDeCliente _tipoCliente;
        private Cliente _cliente;
        public FormEditarCliente(ServicoCliente servicoCliente, int clienteId)
        {
            _servicoCliente = servicoCliente;
            _clienteId = clienteId;
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

                var clienteAtualizado = new Cliente()
                {
                    Nome = textBoxNome.Text,
                    Cpf = maskedTextBoxCpf.Text.Replace(".", string.Empty).Replace("-", string.Empty),
                    Cnpj = maskedTextBoxCnpj.Text.Replace(".", string.Empty).Replace("/", string.Empty).Replace("-", string.Empty),
                    Tipo = _tipoCliente,
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
            _cliente = _servicoCliente.ObterPorId(_clienteId);
            textBoxNome.Text = _cliente.Nome;
            maskedTextBoxCpf.Text = _cliente.Cpf;
            maskedTextBoxCnpj.Text = _cliente.Cnpj;
            ObterCheckBoxMarcada();
            
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
        private void ObterCheckBoxMarcada()
        {
            if (_cliente.Tipo == Cliente.TipoDeCliente.Fisica)
            {
                checkBoxTipoFisica.Checked = true;
            }
            else if (_cliente.Tipo == Cliente.TipoDeCliente.Juridica)
            {
                checkBoxTipoJuridica.Checked = true;
            }
        }
    }
}

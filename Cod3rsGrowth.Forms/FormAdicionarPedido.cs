using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;

namespace Cod3rsGrowth.Forms
{
    public partial class FormAdicionarPedido : Form
    {
        private readonly ServicoPedido _servicoPedido;
        private readonly int _clienteId;
        public FormAdicionarPedido(ServicoPedido servicoPedido, int clienteId)
        {
            _servicoPedido = servicoPedido;
            _clienteId = clienteId;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxFormaPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AoClicarNoBotaoAdicionar(object sender, EventArgs e)
        {
            try
            {
                Pedido.Pagamentos pagamento = Constantes.ENUM_INDEFINIDO;
                if (comboBoxFormaPagamento.SelectedIndex == Constantes.INDEX_CARTAO)
                {
                    pagamento = Pedido.Pagamentos.Cartao;
                }
                else if (comboBoxFormaPagamento.SelectedIndex == Constantes.INDEX_PIX)
                {
                    pagamento = Pedido.Pagamentos.Pix;
                }
                else if (comboBoxFormaPagamento.SelectedIndex == Constantes.INDEX_BOLETO)
                {
                    pagamento = Pedido.Pagamentos.Boleto;
                }
                var pedidoAdicionado = new Pedido()
                {
                    ClienteId = _clienteId,
                    Data = dateTimePickerPedido.Value,
                    NumeroCartao = maskedTextBoxCartao.Text,
                    Valor = numericUpDownValor.Value,
                    FormaPagamento = pagamento,
                };

                _servicoPedido.Adicionar(pedidoAdicionado);
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

        private void dateTimePickerPedido_ValueChanged(object sender, EventArgs e)
        {

        }

        private void FormAdicionarPedido_Load(object sender, EventArgs e)
        {

        }

        private void maskedTextBoxCartao_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void numericUpDownValor_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

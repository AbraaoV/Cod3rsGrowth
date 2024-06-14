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
        private void AoClicarNoBotaoAdicionar(object sender, EventArgs e)
        {
            try
            {
                Pedido.Pagamentos pagamento = Constantes.ENUM_INDEFINIDO;
                if (comboBoxFormaPagamento.SelectedIndex == Constantes.INDICE_CARTAO)
                {
                    pagamento = Pedido.Pagamentos.Cartao;
                }
                else if (comboBoxFormaPagamento.SelectedIndex == Constantes.INDICE_PIX)
                {
                    pagamento = Pedido.Pagamentos.Pix;
                }
                else if (comboBoxFormaPagamento.SelectedIndex == Constantes.INDICE_BOLETO)
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
    }
}

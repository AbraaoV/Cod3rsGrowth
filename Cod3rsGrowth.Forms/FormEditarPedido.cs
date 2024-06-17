using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;

namespace Cod3rsGrowth.Forms
{
    public partial class FormEditarPedido : Form
    {
        private readonly ServicoPedido _servicoPedido;
        private readonly int _clienteId;
        private readonly int _pedidoId;
        public FormEditarPedido(ServicoPedido servicoPedido, int clienteId, int pedidoId)
        {
            _servicoPedido = servicoPedido;
            _clienteId = clienteId;
            _pedidoId = pedidoId;
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
                var pedidoAtualizado = new Pedido()
                {
                    ClienteId = _clienteId,
                    Data = dateTimePickerPedido.Value,
                    NumeroCartao = maskedTextBoxCartao.Text,
                    Valor = numericUpDownValor.Value,
                    FormaPagamento = pagamento,
                };

                _servicoPedido.Atualizar(_pedidoId, pedidoAtualizado);
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
        private void FormEditarPedido_Load(object sender, EventArgs e)
        {
            Pedido pedido = new Pedido();
            pedido = _servicoPedido.ObterPorId(_pedidoId);
            dateTimePickerPedido.Value = pedido.Data;
            maskedTextBoxCartao.Text = pedido.NumeroCartao;
            numericUpDownValor.Value = pedido.Valor;
            if (pedido.FormaPagamento == Pedido.Pagamentos.Cartao)
            {
                comboBoxFormaPagamento.SelectedIndex = Constantes.INDICE_CARTAO;
            }
            else if (pedido.FormaPagamento == Pedido.Pagamentos.Pix)
            {
                comboBoxFormaPagamento.SelectedIndex = Constantes.INDICE_PIX;
            }
            else if (pedido.FormaPagamento == Pedido.Pagamentos.Boleto)
            {
                comboBoxFormaPagamento.SelectedIndex = Constantes.INDICE_BOLETO;
            }
        }
    }
}

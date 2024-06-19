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
        private Pedido.Pagamentos _formaPagamento;
        private Pedido _pedido;
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
                ObterFormaDePagamentoSelecionado();
                var pedidoAtualizado = new Pedido()
                {
                    ClienteId = _clienteId,
                    Data = dateTimePickerPedido.Value,
                    NumeroCartao = maskedTextBoxCartao.Text,
                    Valor = numericUpDownValor.Value,
                    FormaPagamento = _formaPagamento,
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
            _pedido = _servicoPedido.ObterPorId(_pedidoId);
            dateTimePickerPedido.Value = _pedido.Data;
            maskedTextBoxCartao.Text = _pedido.NumeroCartao;
            numericUpDownValor.Value = _pedido.Valor;
            ComboBoxIndiceSelecionado();
        }
        private void ObterFormaDePagamentoSelecionado()
        {
            _formaPagamento = Constantes.ENUM_INDEFINIDO;
            if (comboBoxFormaPagamento.SelectedIndex == Constantes.INDICE_CARTAO)
            {
                _formaPagamento = Pedido.Pagamentos.Cartao;
            }
            else if (comboBoxFormaPagamento.SelectedIndex == Constantes.INDICE_PIX)
            {
                _formaPagamento = Pedido.Pagamentos.Pix;
            }
            else if (comboBoxFormaPagamento.SelectedIndex == Constantes.INDICE_BOLETO)
            {
                _formaPagamento = Pedido.Pagamentos.Boleto;
            }
        }
        private void ComboBoxIndiceSelecionado()
        {
            if (_pedido.FormaPagamento == Pedido.Pagamentos.Cartao)
            {
                comboBoxFormaPagamento.SelectedIndex = Constantes.INDICE_CARTAO;
            }
            else if (_pedido.FormaPagamento == Pedido.Pagamentos.Pix)
            {
                comboBoxFormaPagamento.SelectedIndex = Constantes.INDICE_PIX;
            }
            else if (_pedido.FormaPagamento == Pedido.Pagamentos.Boleto)
            {
                comboBoxFormaPagamento.SelectedIndex = Constantes.INDICE_BOLETO;
            }
        }
    }
}

using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;
using Microsoft.Data.SqlClient;
using static Cod3rsGrowth.Dominio.Cliente;

namespace Cod3rsGrowth.Forms
{
    public partial class FormListaDePedido : Form
    {
        private readonly ServicoPedido _servicoPedido;
        private readonly int _clienteId;
        private DateTime _dataPedido;
        private Pedido.Pagamentos? _pagamentoSelecionado;
        public FormListaDePedido(ServicoPedido servicoPedido, int clienteId)
        {
            _servicoPedido = servicoPedido;
            _clienteId = clienteId;

            InitializeComponent();
            dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(new FiltroPedido { ClienteId = _clienteId }); ;
        }

        private void AoClicarNoBotaoAdicionar(object sender, EventArgs e)
        {
            using (FormAdicionarPedido novoPedido = new FormAdicionarPedido(_servicoPedido, _clienteId) { })
            {
                if (novoPedido.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(new FiltroPedido { ClienteId = _clienteId });
                }
            }
        }

        private void formatacaoExibicaoListaPedido(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridViewPedido.Columns[Constantes.COLUNA_VALOR_TABELA_PEDIDO].DefaultCellStyle.Format = Constantes.DUAS_CASAS_APOS_VIRGULA;
            if (e.ColumnIndex == Constantes.INDICE_COLUNA_CARTAO)
            {
                if (e.Value is string && e.Value != string.Empty)
                {
                    string valor = (string)e.Value;
                    e.Value = valor.Substring(0, 4) + " " + valor.Substring(4, 4) + " " + valor.Substring(8, 4) + " " + valor.Substring(12, 4);
                    e.FormattingApplied = true;
                }
            }
        }

        private void AoClicarNoBotaoRemover(object sender, EventArgs e)
        {
            if (dataGridViewPedido.SelectedRows.Count > Constantes.INDICE_PRIMEIRA_LINHA)
            {
                if (MessageBox.Show(Constantes.MENSAGEM_CONFIRMACAO_REMOCAO_PEDIDO, Constantes.MENSAGEM_CONFIRMACAO, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataGridViewRow linhaSelecionada = dataGridViewPedido.SelectedRows[Constantes.INDICE_PRIMEIRA_LINHA];
                    int pedidoId = (int)linhaSelecionada.Cells[Constantes.COLUNA_ID].Value;
                    try
                    {
                        _servicoPedido.Deletar(pedidoId);
                        dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(new FiltroPedido { ClienteId = _clienteId });
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
            else
            {
                MessageBox.Show(Constantes.MENSAGEM_ERRO_AO_SELECIONAR_NENHUM_PEDIDO, Constantes.AVISO, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void AoClicarNoBotaoEditar(object sender, EventArgs e)
        {
            if (dataGridViewPedido.SelectedRows.Count > Constantes.INDICE_PRIMEIRA_LINHA)
            {
                DataGridViewRow linhaSelecionada = dataGridViewPedido.SelectedRows[Constantes.INDICE_PRIMEIRA_LINHA];
                int pedidoId = (int)linhaSelecionada.Cells[Constantes.COLUNA_ID].Value;

                using (FormEditarPedido novoPedido = new FormEditarPedido(_servicoPedido, _clienteId, pedidoId) { })
                {
                    if (novoPedido.ShowDialog() == DialogResult.OK)
                    {
                        dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(new FiltroPedido { ClienteId = _clienteId });
                    }
                }
            }
            else
            {
                MessageBox.Show(Constantes.MENSAGEM_ERRO_AO_SELECIONAR_NENHUM_PEDIDO, Constantes.AVISO, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AoFiltrarPelaData(object sender, EventArgs e)
        {
            dateTimePickerDataFiltro.CustomFormat = Constantes.FORMATACAO_DATA;
            
        }

        private void FormListaDePedido_Load(object sender, EventArgs e)
        {
            FiltroFormaPagamento.SelectedIndex = Constantes.INDICE_TODOS_PAGAMENTOS;
        }

        private void AoApertarOBotaoFiltrar(object sender, EventArgs e)
        {
            ObterValorParaFiltroPagamento();
            ObterFiltroDaData();

            dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(new FiltroPedido { FormaPagamento = _pagamentoSelecionado, ClienteId = _clienteId, DataPedido = _dataPedido, ValorMin = valorMinFiltro.Value, ValorMax = valorMaxFiltro.Value });

        }

        private void AoApertarDeleteOuBack(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                dateTimePickerDataFiltro.CustomFormat = " ";
                dateTimePickerDataFiltro.Value = DateTime.MinValue;
            }
        }

        private void AoApertarOBotaoLimpar(object sender, EventArgs e)
        {
            dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(new FiltroPedido { ClienteId  = _clienteId});
            dateTimePickerDataFiltro.CustomFormat = " ";
            FiltroFormaPagamento.SelectedIndex = Constantes.INDICE_TODOS_PAGAMENTOS;
            valorMaxFiltro.Value = Constantes.VALOR_INICIAL;
            valorMinFiltro.Value = Constantes.VALOR_INICIAL;
        }
        private void ObterValorParaFiltroPagamento()
        {
            _pagamentoSelecionado = new Pedido.Pagamentos();
            
            if (FiltroFormaPagamento.SelectedIndex == Constantes.INDICE_CARTAO)
            {
                _pagamentoSelecionado = Pedido.Pagamentos.Cartao;
            }
            else if (FiltroFormaPagamento.SelectedIndex == Constantes.INDICE_PIX)
            {
                _pagamentoSelecionado = Pedido.Pagamentos.Pix;
            }
            else if (FiltroFormaPagamento.SelectedIndex == Constantes.INDICE_BOLETO)
            {
                _pagamentoSelecionado = Pedido.Pagamentos.Boleto;
            }
            else
            {
                _pagamentoSelecionado = null;
            }
            
        }
        private void ObterFiltroDaData()
        {
            _dataPedido = dateTimePickerDataFiltro.Value.Date;
            if (dateTimePickerDataFiltro.CustomFormat == " ")
            {
                _dataPedido = default;
            }
        }
    }
}

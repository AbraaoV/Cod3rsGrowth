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
        public FormListaDePedido(ServicoPedido servicoPedido, int clienteId)
        {
            _servicoPedido = servicoPedido;
            _clienteId = clienteId;

            InitializeComponent();
            dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(null, clienteId, default, null, null);
        }

        private void AoClicarNoBotaoAdicionar(object sender, EventArgs e)
        {
            using (FormAdicionarPedido novoPedido = new FormAdicionarPedido(_servicoPedido, _clienteId) { })
            {
                if (novoPedido.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(null, _clienteId, default, null, null);
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
                        dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(null, _clienteId, default, null, null);
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
                MessageBox.Show(Constantes.MENSAGEM_ERRO_AO_REMOVER_NENHUM_PEDIDO, Constantes.AVISO, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(null, _clienteId, default, null, null);
                    }
                }
            }
        }

        private void AoFiltrarPelaData(object sender, EventArgs e)
        {
            dateTimePickerDataFiltro.CustomFormat = "dd-MM-yyyy";
        }

        private void FormListaDePedido_Load(object sender, EventArgs e)
        {
            FiltroFormaPagamento.SelectedIndex = Constantes.INDICE_TODOS_PAGAMENTOS;
        }

        private void AoApertarOBotaoFiltrar(object sender, EventArgs e)
        {
            Dominio.Pedido.Pagamentos? pagamentoSelecionado = new Dominio.Pedido.Pagamentos();
            if (FiltroFormaPagamento.SelectedIndex == Constantes.INDICE_CARTAO)
            {
                pagamentoSelecionado = Dominio.Pedido.Pagamentos.Cartao;
            }
            else if (FiltroFormaPagamento.SelectedIndex == Constantes.INDICE_PIX)
            {
                pagamentoSelecionado = Dominio.Pedido.Pagamentos.Pix;
            }
            else if (FiltroFormaPagamento.SelectedIndex == Constantes.INDICE_BOLETO)
            {
                pagamentoSelecionado = Dominio.Pedido.Pagamentos.Boleto;
            }
            else
            {
                pagamentoSelecionado = null;
            }

            dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(pagamentoSelecionado, _clienteId, default, valorMinFiltro.Value, valorMaxFiltro.Value);

        }

        private void AoApertarDeleteOuBack(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                dateTimePickerDataFiltro.CustomFormat = " ";
            }
        }

        private void AoApertarOBotaoLimpar(object sender, EventArgs e)
        {
            dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(null, _clienteId, default, null, null);
        }
    }
}

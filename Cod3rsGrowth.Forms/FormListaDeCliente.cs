using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;
using LinqToDB.Data;
using LinqToDB;
using System.Data;
using System.Configuration;
using Cod3rsGrowth.Servico.Servicos;
using System.Windows.Forms;

namespace Cod3rsGrowth.Forms
{
    public partial class FormListaDeCliente : Form
    {
        private readonly ServicoCliente _servicoCliente;
        private readonly ServicoPedido _servicoPedido;
        private int _clienteId;
        public FormListaDeCliente(ServicoCliente servicoCliente, ServicoPedido servicoPedido)
        {
            _servicoCliente = servicoCliente;
            _servicoPedido = servicoPedido;

            InitializeComponent();

            dataGridViewCliente.DataSource = _servicoCliente.ObterTodos(null);
        }

        private void AoClicarNoBotaoAdicionar(object sender, EventArgs e)
        {
            using (FormCadastroDeCliente novoCliente = new FormCadastroDeCliente(_servicoCliente) { })
            {
                if (novoCliente.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewCliente.DataSource = _servicoCliente.ObterTodos(null);
                }
            }

        }
        private void formatacaoExibicaoListaCliente(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == Constantes.INDICE_COLUNA_CPF)
            {
                if (e.Value is string && e.Value != string.Empty)
                {
                    string valor = (string)e.Value;
                    e.Value = valor.Substring(0, 3) + "." + valor.Substring(3, 3) + "." + valor.Substring(6, 3) + "-" + valor.Substring(9, 2).ToUpper();
                    e.FormattingApplied = true;
                }
            }

            if (e.ColumnIndex == Constantes.INDICE_COLUNA_CNPJ)
            {
                if (e.Value is string && e.Value != string.Empty)
                {
                    string valor = (string)e.Value;
                    e.Value = valor.Substring(0, 2) + "." + valor.Substring(2, 3) + "." + valor.Substring(5, 3) + "/" + valor.Substring(8, 4) + "-" + valor.Substring(12, 2).ToString();
                    e.FormattingApplied = true;
                }
            }
        }
        public void AbrirPedidos(int clienteId)
        {
            FormListaDePedido formPedido = new FormListaDePedido(_servicoPedido, clienteId);
            formPedido.Show();
        }
        private void AoClicarComOBotaoDireitoNaListaPedido(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= Constantes.INDICE_PRIMEIRA_LINHA)
                {
                    DataGridViewRow linha = this.dataGridViewCliente.Rows[e.RowIndex];
                    int clienteId = (int)linha.Cells[Constantes.COLUNA_ID_TABELA_CLIENTE].Value;


                    ContextMenuStrip contextMenu = new ContextMenuStrip();
                    ToolStripMenuItem menuItem = new ToolStripMenuItem(Constantes.OPCAO_DO_TOOL_STRIP_MENU);
                    menuItem.Click += (s, args) => AbrirPedidos(clienteId);
                    contextMenu.Items.Add(menuItem);

                    dataGridViewCliente.ContextMenuStrip = contextMenu;
                }
            }
        }
    }
}

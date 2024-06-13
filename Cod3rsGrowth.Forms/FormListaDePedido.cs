using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(null, clienteId);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs idDataGridViewTextBoxColumn)
        {

        }

        private void AoClicarNoBotaoAdicionar(object sender, EventArgs e)
        {
            using (FormAdicionarPedido novoPedido = new FormAdicionarPedido(_servicoPedido, _clienteId) { })
            {
                if (novoPedido.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewPedido.DataSource = _servicoPedido.ObterTodos(null, _clienteId);
                }
            }
        }

        private void dataGridViewPedido_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridViewPedido.Columns[Constantes.COLUNA_VALOR_TABELA_PEDIDO].DefaultCellStyle.Format = Constantes.DUAS_CASAS_APOS_VIRGULA;
        }
    }
}

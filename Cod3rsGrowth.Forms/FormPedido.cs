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

    public partial class FormPedido : Form
    {
        private readonly ServicoPedido _servicoPedido;
        private readonly int _clienteId;
        public FormPedido(ServicoPedido servicoPedido, int clienteId)
        {
            _servicoPedido = servicoPedido;
            _clienteId = clienteId;

            InitializeComponent();
            dataGridView1.DataSource = _servicoPedido.ObterTodos(null, clienteId);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs idDataGridViewTextBoxColumn)
        {

        }

        private void buttonAdicionar_Click(object sender, EventArgs e)
        {
            using (FormAdicionarPedido novoPedido = new FormAdicionarPedido(_servicoPedido, _clienteId) { })
            {
                if (novoPedido.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.DataSource = _servicoPedido.ObterTodos(null, _clienteId);
                }
            }
        }
    }
}

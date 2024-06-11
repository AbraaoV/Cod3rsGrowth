using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;
using LinqToDB.Data;
using LinqToDB;
using System.Data;
using System.Configuration;
using Cod3rsGrowth.Servico.Servicos;

namespace Cod3rsGrowth.Forms
{
    public partial class FormLista : Form
    {
        private readonly ServicoCliente _servicoCliente;
        private readonly ServicoPedido _servicoPedido;
        public FormLista(ServicoCliente servicoCliente, ServicoPedido servicoPedido)
        {
            _servicoCliente = servicoCliente;
            _servicoPedido = servicoPedido;

            InitializeComponent();
            dataGridView1.DataSource = _servicoCliente.ObterTodos();
        }

        private void buttonAdicionar_Click(object sender, EventArgs e)
        {
            using (FormCadastroDeCliente novoCliente = new FormCadastroDeCliente(_servicoCliente) { })
            {
                if(novoCliente.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.DataSource = _servicoCliente.ObterTodos();
                }
            }

        }

        private void FormLista_Load(object sender, EventArgs e)
        {
  
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = this.dataGridView1.Rows[e.RowIndex];
                int clienteId = (int)linha.Cells["idDataGridViewTextBoxColumn"].Value;

                FormPedido formPedido = new FormPedido(_servicoPedido, clienteId);
                formPedido.Show();
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.Value is string && e.Value != "")
                {
                    string valor = (string)e.Value;
                    e.Value = valor.Substring(0, 3) + "." + valor.Substring(3, 3) + "." + valor.Substring(6, 3) + "-" + valor.Substring(9, 2).ToUpper();
                    e.FormattingApplied = true;
                }
            }

            if (e.ColumnIndex == 3)
            {
                if (e.Value is string && e.Value != "")
                {
                    string valor = (string)e.Value;
                    e.Value = valor.Substring(0, 2) + "." + valor.Substring(2, 3) + "." + valor.Substring(5, 3) + "/" + valor.Substring(8, 4) + "-" + valor.Substring(12, 2).ToString();
                    e.FormattingApplied = true;
                }
            }
        }
    }
}

using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;
using LinqToDB.Data;
using LinqToDB;
using System.Data;
using System.Configuration;

namespace Cod3rsGrowth.Forms
{
    public partial class FormLista : Form
    {
        private readonly IClienteRepositorio _clienteRepostiorio;
        public FormLista(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepostiorio = clienteRepositorio;

            InitializeComponent();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void FormLista_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _clienteRepostiorio.ObterTodos();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

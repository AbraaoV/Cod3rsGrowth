using Cod3rsGrowth.Dominio;
using System.Data;

namespace Cod3rsGrowth.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Cliente> clientes = new List<Cliente>();
            clientes.Add(new Cliente()
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            });


            List<Pedido> pedidos = new List<Pedido>();
            pedidos.Add(new Pedido()
            {
                Id = 2,
                ClienteId = 100,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 150.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao
            });

            dataGridView2.DataSource = pedidos;
            dataGridView1.DataSource = clientes;
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

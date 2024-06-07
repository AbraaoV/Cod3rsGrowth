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
        public FormLista(ServicoCliente servicoCliente)
        {
            _servicoCliente = servicoCliente;

            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void FormLista_Load(object sender, EventArgs e)
        {
            //var Clientes = _servicoCliente.ObterTodos();
            //foreach (ITable<Cliente> cliente in Clientes)
            //{
            //    cliente.Cpf = formatarCpf(cliente.Cpf);
            //}

            dataGridView1.DataSource = _servicoCliente.ObterTodos();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {


        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if(e.Value is string)
                {
                    string valor = (string)e.Value;
                    e.Value = valor.Substring(0, 3) + "." + valor.Substring(3, 3) + "." + valor.Substring(6, 3) + "-" + valor.Substring(9, 2).ToUpper();
                    e.FormattingApplied = true;
                }
            }

            if (e.ColumnIndex == 3)
            {
                if (e.Value is string)
                {
                    string valor = (string)e.Value;
                    e.Value = valor.Substring(0, 2) + "." + valor.Substring(2, 3) + "." + valor.Substring(5, 3) + "/" + valor.Substring(8, 4) + "-" + valor.Substring(12, 2).ToUpper();
                    e.FormattingApplied = true;
                }
            }
        }

        public string formatarCpf(String Cpf)
        {
            string formatarCpf = Cpf;
            return formatarCpf.Substring(0, 3) + "." + formatarCpf.Substring(3, 3) + "." + formatarCpf.Substring(6, 3) + "-" + formatarCpf.Substring(9, 2).ToUpper();
        }

    }
}

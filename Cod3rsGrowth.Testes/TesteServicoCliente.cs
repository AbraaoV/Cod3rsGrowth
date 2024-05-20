using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using Microsoft.Extensions.DependencyInjection;
namespace Cod3rsGrowth.Testes
{
    public class TesteServicoCliente : TesteBase
    {
        private readonly IServicoCliente _servicosCliente;

        public TesteServicoCliente()
        {
            _servicosCliente = ServiceProvider.GetService<IServicoCliente>();
        }

        [Fact]
        public void Ao_obter_todos_deve_retornar_uma_lista_diferente_de_vazio()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "123.456.789-10",
                Cnpj = "",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            var cliente2 = new Cliente
            {
                Nome = "Empresa Teste",
                Id = 200,
                Cpf = "",
                Cnpj = "12.345.678/0001-90",
                Tipo = Cliente.TipoDeCliente.Juridica
            };
            TabelaCliente.Instance.Add(cliente1);
            TabelaCliente.Instance.Add(cliente2);

            var clientes = _servicosCliente.ObterTodos();

            Assert.NotEmpty(clientes);
        }
    }
}

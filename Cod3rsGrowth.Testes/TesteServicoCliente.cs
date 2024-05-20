using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Dominio.Servicos;
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
        public void TesteObterTodos_NaoEstaVazio()
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

            var obterTodos = _servicosCliente.ObterTodos();

            Assert.NotEmpty(obterTodos);
        }

        [Fact]
        public void TesteObterTodos_Clientes_cliente1()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "123.456.789-10",
                Cnpj = "",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var obterTodos = _servicosCliente.ObterTodos();

            Assert.Equal("Teste", cliente1.Nome);
            Assert.Equal(100, cliente1.Id);
            Assert.Equal("123.456.789-10", cliente1.Cpf);
            Assert.Equal("", cliente1.Cnpj);
            Assert.Equal(Cliente.TipoDeCliente.Fisica, cliente1.Tipo);
        }

        [Fact]
        public void TesteObterTodos_Clientes_cliente2()
        {
            var cliente2 = new Cliente
            {
                Nome = "Empresa Teste",
                Id = 200,
                Cpf = "",
                Cnpj = "12.345.678/0001-90",
                Tipo = Cliente.TipoDeCliente.Juridica
            };
            TabelaCliente.Instance.Add(cliente2);

            var obterTodos = _servicosCliente.ObterTodos();

            Assert.Equal("Empresa Teste", cliente2.Nome);
            Assert.Equal(200, cliente2.Id);
            Assert.Equal("", cliente2.Cpf);
            Assert.Equal("12.345.678/0001-90", cliente2.Cnpj);
            Assert.Equal(Cliente.TipoDeCliente.Juridica, cliente2.Tipo);
        }
    }
}

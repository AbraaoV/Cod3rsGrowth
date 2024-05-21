using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.TestHelper;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
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
        [Fact]
        public void Ao_obter_por_id_deve_retornar_cliente()
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

            var clientes = _servicosCliente.ObterPorId(id: cliente1.Id = 100);

            Assert.Equal("Teste", cliente1.Nome);
            Assert.Equal(100, cliente1.Id);
            Assert.Equal("123.456.789-10", cliente1.Cpf);
            Assert.Equal("", cliente1.Cnpj);
            Assert.Equal(Cliente.TipoDeCliente.Fisica, cliente1.Tipo);
        }

        [Fact]
        public void Ao_obter_por_id_deve_retornar_cliente_nullo()
        {

            var cliente = _servicosCliente.ObterPorId(id: 400);

            Assert.Null(cliente);
        }

        [Fact]
        public void Ao_adicionar_cliente_deve_retornar_nome_valido()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            _servicosCliente.Adicionar(cliente1);

            Assert.
        }
    }
}

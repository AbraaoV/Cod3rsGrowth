using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.TestHelper;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using ValidationException = FluentValidation.ValidationException;
using FluentValidation.Results;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Cod3rsGrowth.Testes
{
    public class TesteServicoCliente : TesteBase
    {
        private readonly IServicoCliente _servicosCliente;
        private readonly IValidator<Cliente> _validarCliente;
        public TesteServicoCliente()
        {
            _servicosCliente = ServiceProvider.GetService<IServicoCliente>();
            _validarCliente = ServiceProvider.GetService<IValidator<Cliente>>();
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
        public void Ao_adicionar_nome_nulo_ou_vazio_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Adicionar(cliente1));
            Assert.Equal("O nome � um campo obrigat�rio.", mensagemErro.Errors.Single().ErrorMessage);
 
        }

        [Fact]
        public void Ao_adicionar_nome_de_mais_50_caracteres_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Adicionar(cliente1));
            Assert.Equal("O nome n�o pode ter mais de 50 caracteres", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_adicionar_cpf_que_nao_tenha_11_digitos_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "1112345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Adicionar(cliente1));
            Assert.Equal("CPF inv�lido", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_adicionar_cnpj_que_nao_tenha_14_digitos_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Empresa Teste",
                Id = 200,
                Cnpj = "11112345678000190",
                Tipo = Cliente.TipoDeCliente.Juridica
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Adicionar(cliente1));
            Assert.Equal("CNPJ inv�lido", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_adicionar_um_cliente_sem_tipo_definido_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Empresa Teste",
                Id = 200,
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Adicionar(cliente1));
            Assert.Equal("O Tipo � um campo obrigat�rio", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_adicionar_cliente_do_tipo_fisica_com_cnpj_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cnpj = "12345678000190",
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Adicionar(cliente1));
            Assert.Equal("Para pessoa f�sica, n�o informe Cnpj.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_adicionar_cliente_do_tipo_juridica_com_cpf_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cnpj = "12345678000190",
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Juridica
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Adicionar(cliente1));
            Assert.Equal("Para pessoa j�ridica, n�o informe Cpf.", mensagemErro.Errors.Single().ErrorMessage);
        }
        [Fact]
        public void Ao_adicionar_cliente_do_tipo_fisica_com_cpf_vazio_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "",
                Tipo = Cliente.TipoDeCliente.Fisica
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Adicionar(cliente1));
            Assert.Equal("Para pessoa f�sica, o Cpf � obrigat�rio.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_adicionar_cliente_que_atende_todas_as_regras_deve_ser_adicionado_normalmente()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            _servicosCliente.Adicionar(cliente1);

            Assert.Equal("Teste", cliente1.Nome);
            Assert.Equal(100, cliente1.Id);
            Assert.Equal("12345678910", cliente1.Cpf);
            Assert.Equal(Cliente.TipoDeCliente.Fisica, cliente1.Tipo);
        }

    }
}

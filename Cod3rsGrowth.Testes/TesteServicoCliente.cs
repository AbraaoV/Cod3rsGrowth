using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.TestHelper;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using ValidationException = FluentValidation.ValidationException;
using FluentValidation.Results;
using Cod3rsGrowth.Infra;
using LinqToDB.Data;
using System.Configuration;
using LinqToDB;
namespace Cod3rsGrowth.Testes
{
    public class TesteServicoCliente : TesteBase
    {
        private readonly ServicoCliente _servicosCliente;
        private readonly DataConnection _dataConnection;
        public TesteServicoCliente()
        {
            _servicosCliente = ServiceProvider.GetService<ServicoCliente>();
            TabelaCliente.Instance.Clear();
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
            TabelaCliente.Instance.Remove(cliente1);
            TabelaCliente.Instance.Remove(cliente2);
        }
        [Fact]
        public void Ao_obter_por_id_deve_retornar_cliente()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cnpj = "",
                Cpf = "123.456.789-10",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var cliente = _servicosCliente.ObterPorId(id: cliente1.Id);

            Assert.Contains(TabelaCliente.Instance, cliente1 => cliente1.Nome == cliente.Nome);
            Assert.Contains(TabelaCliente.Instance, cliente1 => cliente1.Id == cliente.Id);
            Assert.Contains(TabelaCliente.Instance, cliente1 => cliente1.Cpf == cliente.Cpf);
            Assert.Contains(TabelaCliente.Instance, cliente1 => cliente1.Cnpj  == cliente.Cnpj);
            Assert.Contains(TabelaCliente.Instance, cliente1 => cliente1.Tipo == cliente.Tipo);
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
            Assert.Equal("O nome é um campo obrigatório.", mensagemErro.Errors.Single().ErrorMessage);
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
            Assert.Equal("O nome não pode ter mais de 50 caracteres", mensagemErro.Errors.Single().ErrorMessage);
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
            Assert.Equal("CPF inválido", mensagemErro.Errors.Single().ErrorMessage);
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
            Assert.Equal("CNPJ inválido", mensagemErro.Errors.Single().ErrorMessage);
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
            Assert.Equal("O Tipo é um campo obrigatório", mensagemErro.Errors.Single().ErrorMessage);
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
            Assert.Equal("Para pessoa física, não informe Cnpj.", mensagemErro.Errors.Single().ErrorMessage);
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
            Assert.Equal("Para pessoa júridica, não informe Cpf.", mensagemErro.Errors.Single().ErrorMessage);
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
            Assert.Equal("Para pessoa física, o Cpf é obrigatório.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_adicionar_cliente_do_tipo_juridica_com_cnpj_vazio_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cnpj = "",
                Tipo = Cliente.TipoDeCliente.Juridica
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Adicionar(cliente1));
            Assert.Equal("Para pessoa júridica, o Cnpj é obrigatório.", mensagemErro.Errors.Single().ErrorMessage);
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

            Assert.Contains(TabelaCliente.Instance, cliente1 => cliente1 == cliente1);
        }

        [Fact]
        public void Ao_adicionar_cliente_que_atende_todas_as_regras_deve_ser_adicionado_normalmente_com_os_valores_iguais()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            _servicosCliente.Adicionar(cliente1);

            Assert.Contains(TabelaCliente.Instance, cliente1 => cliente1.Nome == cliente1.Nome);
            Assert.Contains(TabelaCliente.Instance, cliente1 => cliente1.Id == cliente1.Id);
            Assert.Contains(TabelaCliente.Instance, cliente1 => cliente1.Cpf == cliente1.Cpf);
            Assert.Contains(TabelaCliente.Instance, cliente1 => cliente1.Tipo == cliente1.Tipo);
        }

        [Fact]
        public void Ao_tentar_atualizar_um_cliente_de_um_id_inexistente_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var clienteAtualizado = new Cliente
            {
                Nome = "João",
                Id = 102,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Atualizar(102, clienteAtualizado));
            Assert.Equal("Esse Id não existe.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_um_cliente_de_um_id_existente_deve_ser_atualizado_normalmente()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var clienteAtualizado = new Cliente
            {
                Nome = "João",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            _servicosCliente.Atualizar(100, clienteAtualizado);

            Assert.Equal("João", cliente1.Nome);
            Assert.Equal(100, cliente1.Id);
            Assert.Equal("12345678910", cliente1.Cpf);
            Assert.Equal(Cliente.TipoDeCliente.Fisica, cliente1.Tipo);
        }

        [Fact]
        public void Ao_atualizar_nome_nulo_ou_vazio_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var clienteAtualizado = new Cliente
            {
                Nome = null,
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Atualizar(100, clienteAtualizado));
            Assert.Equal("O nome é um campo obrigatório.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_nome_de_mais_50_caracteres_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);
            var clienteAtualizado = new Cliente
            {
                Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Atualizar(100, clienteAtualizado));
            Assert.Equal("O nome não pode ter mais de 50 caracteres", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_um_cpf_que_nao_tenha_11_digitos_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var clienteAtualizado = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "1112345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Adicionar(clienteAtualizado));
            Assert.Equal("CPF inválido", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_um_cnpj_que_nao_tenha_14_digitos_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var clienteAtualizado = new Cliente
            {
                Nome = "Empresa Teste",
                Id = 100,
                Cnpj = "11112345678000190",
                Tipo = Cliente.TipoDeCliente.Juridica
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Atualizar(100, clienteAtualizado));
            Assert.Equal("CNPJ inválido", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_um_cliente_sem_tipo_definido_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var clienteAtualizado = new Cliente
            {
                Nome = "Empresa Teste",
                Id = 100,
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Atualizar(100, clienteAtualizado));
            Assert.Equal("O Tipo é um campo obrigatório", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_cliente_do_tipo_fisica_com_cnpj_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var clienteAtualizado = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cnpj = "12345678000190",
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Atualizar(100, clienteAtualizado));
            Assert.Equal("Para pessoa física, não informe Cnpj.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_cliente_do_tipo_juridica_com_cpf_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var clienteAtualizado = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cnpj = "12345678000190",
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Juridica
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Atualizar(100, clienteAtualizado));
            Assert.Equal("Para pessoa júridica, não informe Cpf.", mensagemErro.Errors.Single().ErrorMessage);
        }
        
        [Fact]
        public void Ao_atualizar_cliente_do_tipo_fisica_com_cpf_vazio_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var clienteAtualizado = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Tipo = Cliente.TipoDeCliente.Fisica
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Atualizar(100, clienteAtualizado));
            Assert.Equal("Para pessoa física, o Cpf é obrigatório.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_cliente_do_tipo_juridica_com_cnpj_vazio_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var clienteAtualizado = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cnpj = "",
                Tipo = Cliente.TipoDeCliente.Juridica
            };


            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Atualizar(100, clienteAtualizado));
            Assert.Equal("Para pessoa júridica, o Cnpj é obrigatório.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_deleter_um_cliente_com_id_inexistente_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicosCliente.Deletar(102));
            Assert.Equal("Esse Id não existe.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_deletar_um_cliente_com_id_existente_nao_deve_retornar_erro()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Tipo = Cliente.TipoDeCliente.Fisica
            };
            TabelaCliente.Instance.Add(cliente1);

            _servicosCliente.Deletar(100);

            Assert.DoesNotContain(TabelaCliente.Instance, cliente1 => cliente1 == cliente1);
        }
    }
}

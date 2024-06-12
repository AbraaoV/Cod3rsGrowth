using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Testes
{
    public class TesteServicoPedido : TesteBase
    {
        private readonly ServicoPedido _servicoPedido;
        public TesteServicoPedido()
        {
            _servicoPedido = ServiceProvider.GetService<ServicoPedido>();
            TabelaPedido.Instance.Clear();
        }

        [Fact]
        public void Ao_obter_todos_deve_retornar_uma_lista_diferente_de_vazio()
        {
            var pedido1 = new Pedido
            {
                Id = 1,
                ClienteId = 100,
                Data = DateTime.Now,
                NumeroCartao = "0000 1111 2222 3333",
                Valor = 200.50m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            var pedido2 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = DateTime.Now,
                NumeroCartao = "",
                Valor = 540.50m,
                FormaPagamento = Pedido.Pagamentos.Pix,
            };
            TabelaPedido.Instance.Add(pedido1);
            TabelaPedido.Instance.Add(pedido2);

            var pedidos = _servicoPedido.ObterTodos(null, null);

            Assert.NotEmpty(pedidos);
            TabelaPedido.Instance.Remove(pedido1);
            TabelaPedido.Instance.Remove(pedido2);
        }

        [Fact]
        public void Ao_obter_por_id_deve_retornar_pedido()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "",
                Valor = 540.50m,
                FormaPagamento = Pedido.Pagamentos.Pix,
            };
            TabelaPedido.Instance.Add(pedido1);

            var pedido = _servicoPedido.ObterPorId(pedido1.Id = 2);

            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1 == pedido);
        }
        [Fact]
        public void Ao_obter_por_id_deve_retornar_pedido_com_os_valores_iguais()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "",
                Valor = 540.50m,
                FormaPagamento = Pedido.Pagamentos.Pix,
            };
            TabelaPedido.Instance.Add(pedido1);

            var pedido = _servicoPedido.ObterPorId(pedido1.Id = 2);


            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.Id == pedido.Id);
            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.ClienteId == pedido.ClienteId);
            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.Data == pedido.Data);
            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.NumeroCartao == pedido.NumeroCartao);
            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.Valor == pedido.Valor);
            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.FormaPagamento == pedido.FormaPagamento);
        }

        [Fact]
        public void Ao_obter_por_id_deve_retornar_pedido_nullo()
        {

            var pedidos = _servicoPedido.ObterPorId(id : 1);

            Assert.Null(pedidos);
        }

        [Fact]
        public void Ao_adicionar_pedido_com_campo_data_vazio_deve_retornar_erro()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(),
                NumeroCartao = "0000111122223333",
                Valor = 540.50m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicoPedido.Adicionar(pedido1));
            Assert.Equal("O campo Data é obrigatório.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_adicionar_pedido_com_campo_NumeroCartao_com_tamanho_diferente_de_14_deve_retornar_erro()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "000011112222333312",
                Valor = 540.50m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicoPedido.Adicionar(pedido1));
            Assert.Equal("Cartão invalido.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_adicionar_pedido_com_campo_Valor_menor_que_1_deve_retornar_erro()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = -3,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicoPedido.Adicionar(pedido1));
            Assert.Equal("O valor do pedido deve ser maior que zero.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_adicionar_pedido_com_campo_enum_vazio_deve_retornar_erro()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 2,
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicoPedido.Adicionar(pedido1));
            Assert.Equal("O campo FormaPagamento é obrigatório.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_adicionar_cliente_que_atende_todas_as_regras_deve_ser_adicionado_normalmente()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 150.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            _servicoPedido.Adicionar(pedido1);

            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1 == pedido1);
        }

        [Fact]
        public void Ao_adicionar_cliente_que_atende_todas_as_regras_deve_ser_adicionado_normalmente_com_os_valores_iguais()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 150.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            _servicoPedido.Adicionar(pedido1);

            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.Id == pedido1.Id);
            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.ClienteId == pedido1.ClienteId);
            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.Data == pedido1.Data);
            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.NumeroCartao == pedido1.NumeroCartao);
            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.Valor == pedido1.Valor);
            Assert.Contains(TabelaPedido.Instance, pedido1 => pedido1.FormaPagamento == pedido1.FormaPagamento);
        }

        [Fact]
        public void Ao_tentar_atualizar_um_pedido_de_um_id_inexistente_deve_retornar_erro()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 150.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };
            TabelaPedido.Instance.Add(pedido1);

            var pedidoAtualizado = new Pedido
            {
                Id = 3,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 53.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicoPedido.Atualizar(3, pedidoAtualizado));
            Assert.Equal("Esse Id não existe.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_um_pedido_com_um_id_existente_deve_ser_atualizado_normalmente()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 150.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };
            TabelaPedido.Instance.Add(pedido1);

            var pedidoAtualizado = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 53.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            _servicoPedido.Atualizar(2, pedidoAtualizado);

            Assert.Equal(2, pedido1.Id);
            Assert.Equal(200, pedido1.ClienteId);
            Assert.Equal("0000111122223333", pedido1.NumeroCartao);
            Assert.Equal(53.45m, pedido1.Valor);
            Assert.Equal(Pedido.Pagamentos.Cartao, pedido1.FormaPagamento);
        }

        [Fact]
        public void Ao_atualizar_pedido_com_campo_data_vazio_deve_retornar_erro()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 150.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };
            TabelaPedido.Instance.Add(pedido1);

            var pedidoAtualizado = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(),
                NumeroCartao = "0000111122223333",
                Valor = 540.50m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicoPedido.Atualizar(2, pedidoAtualizado));
            Assert.Equal("O campo Data é obrigatório.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_pedido_com_campo_NumeroCartao_com_tamanho_diferente_de_14_deve_retornar_erro()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 150.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            TabelaPedido.Instance.Add(pedido1);
            var pedidoAtualizado = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "000011112222333312",
                Valor = 540.50m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicoPedido.Atualizar(2, pedidoAtualizado));
            Assert.Equal("Cartão invalido.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_pedido_com_campo_Valor_menor_que_1_deve_retornar_erro()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 150.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };
            TabelaPedido.Instance.Add(pedido1);

            var pedidoAtualizado = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = -3,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicoPedido.Atualizar(2, pedidoAtualizado));
            Assert.Equal("O valor do pedido deve ser maior que zero.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_atualizar_pedido_com_campo_enum_vazio_deve_retornar_erro()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 150.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };
            TabelaPedido.Instance.Add(pedido1);

            var pedidoAtualizado = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 2,
            };

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicoPedido.Atualizar(2, pedidoAtualizado));
            Assert.Equal("O campo FormaPagamento é obrigatório.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_deleter_um_pedido_com_id_inexistente_deve_retornar_erro()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 150.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };
            TabelaPedido.Instance.Add(pedido1);

            var mensagemErro = Assert.Throws<ValidationException>(() => _servicoPedido.Deletar(3));
            Assert.Equal("Esse Id não existe.", mensagemErro.Errors.Single().ErrorMessage);
        }

        [Fact]
        public void Ao_deletar_um_pedido_com_id_existente_nao_deve_retornar_erro()
        {
            var pedido1 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000111122223333",
                Valor = 150.45m,
                FormaPagamento = Pedido.Pagamentos.Cartao
            };
            TabelaPedido.Instance.Add(pedido1);

            _servicoPedido.Deletar(2);

            Assert.DoesNotContain(TabelaPedido.Instance, pedido1 => pedido1 == pedido1);
        }
    }
}

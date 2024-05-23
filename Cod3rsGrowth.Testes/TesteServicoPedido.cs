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
        private readonly IServicoPedido _servicoPedido;
        public TesteServicoPedido()
        {
            _servicoPedido = ServiceProvider.GetService<IServicoPedido>();
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

            var pedidos = _servicoPedido.ObterTodos();

            Assert.NotEmpty(pedidos);
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

            var pedidos = _servicoPedido.ObterPorId(pedido1.Id = 2);

            Assert.Equal(2, pedido1.Id);
            Assert.Equal(200, pedido1.ClienteId);
            Assert.Equal(new DateTime(2024, 05, 15), pedido1.Data);
            Assert.Equal("", pedido1.NumeroCartao);
            Assert.Equal(540.50m, pedido1.Valor);
            Assert.Equal(Pedido.Pagamentos.Pix, pedido1.FormaPagamento);
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

            Assert.Equal(2, pedido1.Id);
            Assert.Equal(200, pedido1.ClienteId);
            Assert.Equal("0000111122223333", pedido1.NumeroCartao);
            Assert.Equal(150.45m, pedido1.Valor);
            Assert.Equal(Pedido.Pagamentos.Cartao, pedido1.FormaPagamento);
        }
    }
}

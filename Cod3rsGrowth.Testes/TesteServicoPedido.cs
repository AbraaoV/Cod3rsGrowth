using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
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
        public void TesteObterTodos_NaoEstaVazio()
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

            var obterTodos = _servicoPedido.ObterTodos();

            Assert.NotEmpty(obterTodos);
        }
        [Fact]
        public void TesteObterTodos_Pedido_pedido1()
        {
            var pedido1 = new Pedido
            {
                Id = 1,
                ClienteId = 100,
                Data = new DateTime(2024, 05, 15),
                NumeroCartao = "0000 1111 2222 3333",
                Valor = 200.50m,
                FormaPagamento = Pedido.Pagamentos.Cartao,
            };
            TabelaPedido.Instance.Add(pedido1);

            var obterTodos = _servicoPedido.ObterTodos();

            Assert.Equal(1, pedido1.Id);
            Assert.Equal(100, pedido1.ClienteId);
            Assert.Equal(new DateTime(2024, 05, 15), pedido1.Data);
            Assert.Equal("0000 1111 2222 3333", pedido1.NumeroCartao);
            Assert.Equal(200.50m, pedido1.Valor);
            Assert.Equal(Pedido.Pagamentos.Cartao, pedido1.FormaPagamento);
        }
        [Fact]
        public void TesteObterTodos_Pedido_pedido2()
        {
            var pedido2 = new Pedido
            {
                Id = 2,
                ClienteId = 200,
                Data = new DateTime(2024, 05, 20),
                NumeroCartao = "",
                Valor = 540.50m,
                FormaPagamento = Pedido.Pagamentos.Pix,
            };
            TabelaPedido.Instance.Add(pedido2);

            var obterTodos = _servicoPedido.ObterTodos();

            Assert.Equal(2, pedido2.Id);
            Assert.Equal(200, pedido2.ClienteId);
            Assert.Equal(new DateTime(2024, 05, 20), pedido2.Data);
            Assert.Equal("", pedido2.NumeroCartao);
            Assert.Equal(540.50m, pedido2.Valor);
            Assert.Equal(Pedido.Pagamentos.Pix, pedido2.FormaPagamento);
        }

    }
}

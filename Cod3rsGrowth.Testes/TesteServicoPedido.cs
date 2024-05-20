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
    }
}

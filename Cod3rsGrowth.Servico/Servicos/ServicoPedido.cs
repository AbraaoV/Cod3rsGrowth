using Cod3rsGrowth.Servico.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Infra;
using Cod3rsGrowth.Dominio;

namespace Cod3rsGrowth.Servico.Servicos
{
    public class ServicoPedido : IServicoPedido
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        public ServicoPedido(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
        }
        public List<Pedido> ObterTodos()
        {
            var pedidos = _pedidoRepositorio.ObterTodos();
            return pedidos;
        }
        public Pedido ObterPorId(int id)
        {
            var pedidos = _pedidoRepositorio.ObterPorId(id);
            return pedidos;
        }

    }
}

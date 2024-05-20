using Cod3rsGrowth.Dominio.Servicos;
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
        public ServicoPedido(IPedidoRepositorio PedidoRepositorio)
        {
            _pedidoRepositorio = PedidoRepositorio;
        }
        public List<Pedido> ObterTodos()
        {
            var clientes = _pedidoRepositorio.ObterTodos();
            return clientes;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;

namespace Cod3rsGrowth.Testes
{
    public class PedidoRepositorioMock : IPedidoRepositorio
    {
        public List<Pedido> ObterTodos()
        {
            return TabelaPedido.Instance;
        }
        public Pedido ObterPorId(int id)
        {
            Pedido pedido = new Pedido();
            return pedido;
        }
        public void Atualizar(int id, Pedido pedido)
        {

        }
        public void Deletar (int id)
        {

        }
        public int Adicionar(Pedido pedido)
        {
            int adicionar = new int();
            return adicionar;
        }


    }
}

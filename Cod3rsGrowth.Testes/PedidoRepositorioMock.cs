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
            return TabelaPedido.Instance.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Atualizar(int id, Pedido pedido)
        {
            Pedido procurarPedido = TabelaPedido.Instance.FirstOrDefault(c => c.Id == id);
            procurarPedido.Id = pedido.Id;
            procurarPedido.ClienteId = pedido.ClienteId;
            procurarPedido.Data = pedido.Data;
            procurarPedido.NumeroCartao = pedido.NumeroCartao;
            procurarPedido.Valor = pedido.Valor;
            procurarPedido.FormaPagamento = pedido.FormaPagamento;
        }
        public void Deletar (int id)
        {
            Pedido pedidoParaRemover = TabelaPedido.Instance.FirstOrDefault(c => c.Id == id);
            TabelaPedido.Instance.Remove(pedidoParaRemover);
        }
        public void Adicionar(Pedido pedido)
        {
            TabelaPedido.Instance.Add(pedido);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;
using static Cod3rsGrowth.Dominio.Pedido;

namespace Cod3rsGrowth.Dominio
{
    public interface IPedidoRepositorio
    {
        List<Pedido> ObterTodos(Pagamentos? FormaPagamento = null);
        Pedido ObterPorId(int id);
        void Atualizar(int id, Pedido pedido);
        void Deletar(int id);
        void Adicionar(Pedido pedido);
    }
}

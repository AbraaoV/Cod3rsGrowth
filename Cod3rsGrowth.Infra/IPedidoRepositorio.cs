using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;

namespace Cod3rsGrowth.Infra
{
    public interface IPedidoRepositorio
    {
        List<Pedido> ObterTodos();
        Pedido ObterPorId(int id);
        void Atualizar(int id, Pedido pedido);
        void Deletar(int id);
        int Adicionar(Pedido pedido);
    }
}

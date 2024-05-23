using Cod3rsGrowth.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Servico.Servicos
{
    public interface IServicoPedido
    {
        List<Pedido> ObterTodos();
        Pedido ObterPorId(int id);
        void Adicionar(Pedido pedido);
        void Atualizar(int id, Pedido pedido);
    }
}

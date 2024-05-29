using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Dominio
{
    public interface IUnitOfWork
    {
        IClienteRepositorio Cliente { get; }
        IPedidoRepositorio Pedido { get; }
        void Salvar();
    }
}

using Cod3rsGrowth.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Testes
{
    public sealed class TabelaPedido
    {
        private static readonly List<Pedido> instance = new List<Pedido>();
 

        public static List<Pedido> Instance { get { return instance; } }
    }
}

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
        private static readonly TabelaPedido instance = new TabelaPedido();
        public List<Pedido> ListaCliente = new List<Pedido>();


        static TabelaPedido() { }

        private TabelaPedido() { }

        public static TabelaPedido Instance { get { return instance; } }
    }
}

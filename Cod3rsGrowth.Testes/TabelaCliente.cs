using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;

namespace Cod3rsGrowth.Testes
{
    public sealed class TabelaPedido
    {
        private static readonly TabelaPedido instance = new TabelaCliente();
        public List<Cliente> ListaCliente = new List<Cliente>();


        static TabelaPedido() { }

        private TabelaPedido() { }

        public static TabelaPedido Instance { get { return instance; } }
    }
    
}
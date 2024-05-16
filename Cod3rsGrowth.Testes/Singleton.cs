using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;

namespace Cod3rsGrowth.Testes
{
    public sealed class Singleton
    {
        public List<Cliente> ListaCliente = new List<Cliente>();
        public List<Pedido> PedidoCliente = new List<Pedido>();
        private static readonly Singleton instance = new Singleton();


        static Singleton() { }

        private Singleton() { }

        public static Singleton Instance { get { return instance; } }
    }
    
}
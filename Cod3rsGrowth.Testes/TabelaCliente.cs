using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;

namespace Cod3rsGrowth.Testes
{
    public sealed class TabelaCliente
    {
        private static readonly List<Cliente> instance = new List<Cliente>();


        public static List<Cliente> Instance { get { return instance; } }


    }
    
}
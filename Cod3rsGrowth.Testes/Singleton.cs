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
        private static readonly Singleton instance = new Singleton();
        public List<Singleton> ListaSingleton = new List<Singleton>();

        static Singleton() { }

        private Singleton() { }

        public static Singleton Instance { get { return instance; } }
    }
    
}
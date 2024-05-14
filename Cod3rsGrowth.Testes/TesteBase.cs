using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Testes
{
    public class TesteBase : IDisposable
    {
        protected ServiceProvider ServiceProvider;
        MeuServico = new MeuServico();
        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

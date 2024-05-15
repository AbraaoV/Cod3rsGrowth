using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Testes
{
    public static class ModuloDeInjecaoTeste
    {
        public static ServiceProvider BindServices(IServiceCollection services)
        {
           return services.BuildServiceProvider();
        }
    }
}

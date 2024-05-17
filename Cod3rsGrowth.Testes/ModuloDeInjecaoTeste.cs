using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Dominio.Servicos;

namespace Cod3rsGrowth.Testes
{
    public static class ModuloDeInjecaoTeste
    {
        public static ServiceProvider BindServices(IServiceCollection services)
        {
            services.AddScoped<IServicosCliente, ServicosCliente>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorioMock>();
            services.AddScoped<IPedidoRepositorio, PedidoRepositorioMock>();
            return services.BuildServiceProvider();
        }
    }
}

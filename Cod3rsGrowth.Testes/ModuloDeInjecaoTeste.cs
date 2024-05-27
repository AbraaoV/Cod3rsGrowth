using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;
using Cod3rsGrowth.Servico;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;

namespace Cod3rsGrowth.Testes
{
    public static class ModuloDeInjecaoTeste
    {
        public static ServiceProvider BindServices(IServiceCollection services)
        {
            services.AddScoped<IValidator<Cliente>, ValidacaoCliente>();
            services.AddScoped<IValidator<Pedido>, ValidacaoPedido>();
            services.AddScoped<IServicoCliente, ServicoCliente>();
            services.AddScoped<IServicoPedido, ServicoPedido>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorioMock>();
            services.AddScoped<IPedidoRepositorio, PedidoRepositorioMock>();
            return services.BuildServiceProvider();
        }
    }
}

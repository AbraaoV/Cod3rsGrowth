using Microsoft.Extensions.DependencyInjection;
using Cod3rsGrowth.Dominio;
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
            services.AddScoped<ServicoCliente>();
            services.AddScoped<ServicoPedido>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorioMock>();
            services.AddScoped<IPedidoRepositorio, PedidoRepositorioMock>();
            return services.BuildServiceProvider();
        }
    }
}

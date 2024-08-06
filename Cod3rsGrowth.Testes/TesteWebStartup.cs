using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Cod3rsGrowth.Testes
{
    public class TesteWebStartup
    {
        public void ConfigurarServicos(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddEndpointsApiExplorer();
            services.AddDirectoryBrowser();
            services.AddSwaggerGen();
            services.AddScoped<IClienteRepositorio, ClienteRepositorioMock>();
            services.AddScoped<IPedidoRepositorio, PedidoRepositorioMock>();
            services.AddScoped<ServicoCliente>();
            services.AddScoped<ServicoPedido>();
            services.AddScoped<IValidator<Cliente>, ValidacaoCliente>();
            services.AddScoped<IValidator<Pedido>, ValidacaoPedido>();
        }

        public void Configurar(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseFileServer(new FileServerOptions
            {
                EnableDirectoryBrowsing = true
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });
            app.UseProblemDetailsExceptionHandler(app.ApplicationServices.GetRequiredService<ILoggerFactory>());
            app.UseAuthorization();
        }
    }
}

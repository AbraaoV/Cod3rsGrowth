using Cod3rsGrowth.Dominio.Migracoes;
using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;
using Cod3rsGrowth.Servico.Servicos;
using FluentMigrator.Runner;
using FluentValidation;
using System.Text.Json.Serialization;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Cod3rsGrowth.Web
{   
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[ConstantesDosRepositorios.CONNECTION_STRING];

            services.AddFluentMigratorCore().ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(result)
                .ScanIn(typeof(AtualizarTabela).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            services.AddMvc().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddEndpointsApiExplorer();
            services.AddDirectoryBrowser();
            services.AddSwaggerGen();
            services.AddScoped<ServicoCliente>();
            services.AddScoped<ServicoPedido>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
            services.AddScoped<IValidator<Cliente>, ValidacaoCliente>();
            services.AddScoped<IValidator<Pedido>, ValidacaoPedido>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseFileServer(new FileServerOptions()
            {
                EnableDirectoryBrowsing = true
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                ServeUnknownFileTypes = true
            });

            app.UseProblemDetailsExceptionHandler(app.ApplicationServices.GetRequiredService<ILoggerFactory>());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    
}

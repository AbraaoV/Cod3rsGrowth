using System.Configuration;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Cod3rsGrowth.Infra;
using Cod3rsGrowth.Dominio;
using Microsoft.Extensions.Hosting;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;
using Cod3rsGrowth.Dominio.Migracoes;

namespace Cod3rsGrowth.Forms
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //To customize application configuration such as set high DPI settings or default font,
            //see https://aka.ms/applicationconfiguration.
            using (var serviceProvider = CreateServices())
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
            ApplicationConfiguration.Initialize();
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<FormListaDeCliente>());
        }
        private static ServiceProvider CreateServices()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[ConstantesDosRepositorios.CONNECTION_STRING];


            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(result)
                    .ScanIn(typeof(AtualizarTabela).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }


        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }


        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<ServicoCliente>();
                    services.AddTransient<ServicoPedido>();
                    services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
                    services.AddTransient<IPedidoRepositorio, PedidoRepositorio>();
                    services.AddScoped<IValidator<Cliente>, ValidacaoCliente>();
                    services.AddScoped<IValidator<Pedido>, ValidacaoPedido>();
                    services.AddTransient<FormListaDeCliente>();
                });
        }

    }
}

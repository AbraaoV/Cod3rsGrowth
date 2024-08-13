using Cod3rsGrowth.Dominio.Migracoes;
using Cod3rsGrowth.Infra;
using FluentMigrator.Runner;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Cod3rsGrowth.Web
{
    public static class Migracoes
    {
        public static void rodarMigracao(WebApplicationBuilder builder)
        {
            builder.Services.AddFluentMigratorCore().ConfigureRunner(rb => rb
            .AddSqlServer()
            .WithGlobalConnectionString(ConfigurationManager.AppSettings[ConnectionString.connectionString])
            .ScanIn(typeof(AtualizarTabela).Assembly).For.Migrations()
            ).AddLogging(lb => lb.AddFluentMigratorConsole());

            var serviceProvider = builder.Services.BuildServiceProvider(false);
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp(20240606135200);
        }

        public static void rodarMigracaoDosTestes(WebApplicationBuilder builder)
        {
            builder.Services.AddFluentMigratorCore().ConfigureRunner(rb => rb
            .AddSqlServer()
            .WithGlobalConnectionString(ConfigurationManager.AppSettings[ConnectionString.connectionString])
            .ScanIn(typeof(_20240813141300_SeedClientes).Assembly).For.Migrations()
            ).AddLogging(lb => lb.AddFluentMigratorConsole());

            var serviceProvider = builder.Services.BuildServiceProvider(false);
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateDown(ConstantesApi.MIGRACAO_TABELA);
            runner.MigrateUp();

        }

    }
}

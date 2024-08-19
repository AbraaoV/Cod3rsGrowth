using Cod3rsGrowth.Dominio.Migracoes;
using Cod3rsGrowth.Dominio.MigracoesBancoDeTeste;
using Cod3rsGrowth.Infra;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using System.Reflection;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Cod3rsGrowth.Web
{
    public static class Migracoes
    {
        public static void Executar(WebApplicationBuilder builder, Assembly migracao, string? profile = null, string? profile2 = null)
        {
            builder.Services.AddFluentMigratorCore().ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(ConfigurationManager.AppSettings[ConnectionString.connectionString])
                .ScanIn(migracao).For.Migrations()
            ).AddLogging(lb => lb.AddFluentMigratorConsole());

            if (profile != null)
            {
                builder.Services.Configure<RunnerOptions>(cfg =>
                {
                    cfg.Profile = profile;
                });
            }

            var serviceProvider = builder.Services.BuildServiceProvider(false);
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}

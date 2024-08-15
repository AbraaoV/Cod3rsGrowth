using Cod3rsGrowth.Dominio.Migracoes;
using Cod3rsGrowth.Dominio.MigracoesBancoDeTeste;
using Cod3rsGrowth.Infra;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
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
            .ScanIn(typeof(_20240606135200_Editar_Coluna_Cpf_Cpnj_e_Cartao).Assembly).For.Migrations()
            ).AddLogging(lb => lb.AddFluentMigratorConsole());

            var serviceProvider = builder.Services.BuildServiceProvider(false);
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        public static void rodarMigracaoDosTestes(WebApplicationBuilder builder)
        {
            builder.Services.AddFluentMigratorCore().ConfigureRunner(rb => rb
            .AddSqlServer()
            .WithGlobalConnectionString(ConfigurationManager.AppSettings[ConnectionString.connectionString])
            .ScanIn(typeof(_20240606135200_Editar_Coluna_Cpf_Cpnj_e_Cartao).Assembly).For.Migrations()
            ).AddLogging(lb => lb.AddFluentMigratorConsole()).Configure<RunnerOptions>(cfg =>
            {
                cfg.Profile = ConstantesMigracao.PERFIL_POPULAR_BANCO_DE_TESTES;
            });

            var serviceProvider = builder.Services.BuildServiceProvider(false);
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}

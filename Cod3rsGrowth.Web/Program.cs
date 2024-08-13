using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Dominio.Migracoes;
using Cod3rsGrowth.Infra;
using Cod3rsGrowth.Servico.Servicos;
using Cod3rsGrowth.Web;
using FluentMigrator.Runner;
using FluentValidation;
using System.Text.Json.Serialization;
using ConfigurationManager = System.Configuration.ConfigurationManager;


var builder = WebApplication.CreateBuilder(args);


if (args.FirstOrDefault() == ConstantesApi.VALOR_DO_COMMAND_LINE_ARGS_PERFIL_DE_TESTE)
{
    ConnectionString.connectionString = ConstantesDosRepositorios.CONNECTION_STRING_TESTE;
    Migracoes.rodarMigracaoDosTestes(builder);
}
else
{
    Migracoes.rodarMigracao(builder);
}

builder.Services.AddMvc().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDirectoryBrowser();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ServicoCliente>();
builder.Services.AddScoped<ServicoPedido>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
builder.Services.AddScoped<IValidator<Cliente>, ValidacaoCliente>();
builder.Services.AddScoped<IValidator<Pedido>, ValidacaoPedido>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
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

app.UseProblemDetailsExceptionHandler(app.Services.GetRequiredService<ILoggerFactory>());

app.UseAuthorization();

app.MapControllers();

app.Run();
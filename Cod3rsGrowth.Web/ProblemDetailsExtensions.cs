using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using FluentValidation;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Cod3rsGrowth.Web;

public static class ProblemDetailsExtensions
{
    public static void UseProblemDetailsExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandlerFeature != null)
                {
                    var exception = exceptionHandlerFeature.Error;
                    var problemDetails = new ProblemDetails
                    {
                        Instance = context.Request.HttpContext.Request.Path
                    };

                    problemDetails.Title = ConstantesDaController.TITULO;
                    problemDetails.Detail = ConstantesDaController.DETALHE;
                    problemDetails.Type = ConstantesDaController.TIPO;
                    problemDetails.Status = StatusCodes.Status400BadRequest;

                    if (exception is ValidationException validationException)
                    {
                        problemDetails.Extensions[ConstantesDaController.NOME_EXTENCAO] = validationException.Errors.Select(x => x.ErrorMessage).ToList();
                    }
                    else if (exception is SqlException sqlException)
                    {
                        if(sqlException.Message.StartsWith(ConstantesDaController.COMECO_MENSAGEM_ERRO_SQL))
                        {
                            problemDetails.Extensions[ConstantesDaController.NOME_EXTENCAO] = ConstantesDaController.MENSAGEM_ERRO_AO_DELETAR_CLIENTE_COM_PEDIDO;
                        }
                    }
                    else
                    {
                        var logger = loggerFactory.CreateLogger(ConstantesDaController.NOME_LOG);
                        logger.LogError($"Unexpected error: {exceptionHandlerFeature.Error}");
                        problemDetails.Title = exception.Message;
                        problemDetails.Status = StatusCodes.Status500InternalServerError;
                        problemDetails.Detail = exception.Demystify().ToString();
                    }

                    context.Response.StatusCode = problemDetails.Status.Value;
                    context.Response.ContentType = ConstantesDaController.TIPO_CONTEUDO;
                    var json = JsonConvert.SerializeObject(problemDetails,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    await context.Response.WriteAsync(json);
                }
            });
        });
    }
}
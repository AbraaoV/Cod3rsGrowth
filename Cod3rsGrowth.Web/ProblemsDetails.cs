//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System.Diagnostics;

//public static class ProblemDetailsExtensions
//{
//    public static void UseProblemDetailsExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
//    {
//        app.UseExceptionHandler(builder =>
//        {
//            builder.Run(async context =>
//            {
//                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
//                if (exceptionHandlerFeature != null)
//                {
//                    var exception = exceptionHandlerFeature.Error;
//                    var problemDetails = new ProblemDetails
//                    {
//                        Instance = context.Request.HttpContext.Request.Path
//                    };
//                    if (exception is BadHttpRequestException badHttpRequestException)
//                    {
//                        problemDetails.Title = "The request is invalid";
//                        problemDetails.Status = StatusCodes.Status400BadRequest;
//                        problemDetails.Detail = badHttpRequestException.Message;
//                    }
//                    else if (exception is FluentValidation.ValidationException validationExpetion)
//                    {
//                        problemDetails.Title = "Aqui";
//                        problemDetails.Status = StatusCodes.Status404NotFound;
//                        problemDetails.Detail = validationExpetion.Message;
//                        problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
//                    }
//                    else
//                    {
//                        var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
//                        logger.LogError($"Unexpected error: {exceptionHandlerFeature.Error}");
//                        problemDetails.Title = exception.Message;
//                        problemDetails.Status = StatusCodes.Status500InternalServerError;
//                        problemDetails.Detail = exception.Demystify().ToString();
//                    }
//                    context.Response.StatusCode = problemDetails.Status.Value;
//                    context.Response.ContentType = "application/problem+json";
//                    var json = JsonConvert.SerializeObject(problemDetails);
//                    await context.Response.WriteAsync(json);
//                }
//            });
//        });
//    }
//}

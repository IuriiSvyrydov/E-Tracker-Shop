
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace E_Tracker.Presentation.Middlewares
{
    public static class ExceptionHandlerConfigure
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication application,ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature!=null)
                    {
                        //log
                        logger.LogError(contextFeature.Error.Message);
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StatusCodes = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Title = "Error"
                        }));
                    }

                });
            });
        }
    }
}
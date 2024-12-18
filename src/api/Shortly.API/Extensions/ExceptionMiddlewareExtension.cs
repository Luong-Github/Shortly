using Microsoft.AspNetCore.Diagnostics;
using Shortly.API.Middlewares;
using Shortly.Application.Models.Errors;
using Shortly.Contract.Dependencies.Services;
using System.Net;

namespace Shortly.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(
                appError =>
                {
                    appError.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var contextFeatures = context.Features.Get<IExceptionHandlerFeature>();

                        if(contextFeatures != null)
                        {
                            logger.LogError($"Something went wrong, {contextFeatures.Error.ToString()}");

                            await context.Response.WriteAsync(new ErrorDetail
                            {
                                Message = contextFeatures.Error.ToString(),
                                StatusCode = context.Response.StatusCode
                            }.ToString());
                        }
                    });
                });
        }

        public static void CustomConfigureExceptionHandler(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

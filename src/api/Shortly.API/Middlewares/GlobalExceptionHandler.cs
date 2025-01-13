using Microsoft.AspNetCore.Diagnostics;
using Shortly.Application.Models.Errors;
using Shortly.Contract.Dependencies.Services;
using System.Net;

namespace Shortly.API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILoggerManager _logger;
        
        public GlobalExceptionHandler(ILoggerManager logger) {  _logger = logger; }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) 
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            _logger.LogError($"Something went wrong, {exception}");
            
            var message = exception switch
            {
                AccessViolationException => "Access violation exception",
                _ => "Something went wrong from global exception handler"
            };

            await httpContext.Response.WriteAsync(new ErrorDetail
            {
                Message = message,
                StatusCode = httpContext.Response.StatusCode
            }.ToString(), cancellationToken).ConfigureAwait(false);

            return true;



        }
    }
}

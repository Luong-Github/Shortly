using Shortly.Application.Models.Errors;
using Shortly.Contract.Dependencies.Services;
using System.Net;

namespace Shortly.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestDelegate _previous;
        private readonly ILoggerManager _logger;

        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, RequestDelegate previous, ILoggerManager logger, IHostEnvironment env)
        {
            _next = next;
            _previous = previous;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong, {ex.ToString()}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var message = 
                _env.IsDevelopment() ? ex.Message :
                ex switch
            {
                AccessViolationException => "Access violation exception",
                _ => "Internal server error from custom middleware"
            };

            await context.Response.WriteAsync(new ErrorDetail 
            { 
                Message = message,
                StatusCode = context.Response.StatusCode
            }.ToString());
        }
    }
}

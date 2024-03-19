using System.Net;
using System.Text.Json;

namespace PresentationLayer.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }

            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            _logger.LogError("Внутреняя ошибка сервера. Подробности:" +
                   " {ExceptionName} \n {exception}", exception, exception.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Description = "Error in methods. Please contact with support"
            }.ToString());

        }

        /// <summary>
        /// Класс содержащий подробности об исключениях
        /// </summary>
        public class ErrorDetails
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public string Description { get; set; }
            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        }
    }
}

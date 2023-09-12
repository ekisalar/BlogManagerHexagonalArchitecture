using System.Net;
using System.Text.Json;

namespace BlogManager.Adapter.Api.Utilities
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate                 _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;


        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next   = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error.StackTrace, $"Error Occured on {context.Request.Method}:{context.Request.Path.Value}");

                var response = context.Response;
                string message;
                response.ContentType = "application/json";
                switch (error)
                {
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        message             = error.InnerException != null ? error.InnerException.Message : error.Message;
                        break;
                }


                var result = JsonSerializer.Serialize(message);

                await response.WriteAsync(result);
            }
        }
    }
}
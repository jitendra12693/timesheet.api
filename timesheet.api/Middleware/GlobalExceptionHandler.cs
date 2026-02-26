using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace timesheet.api.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                _logger.LogInformation($"Request details: {context.Request.Method}/{context.Request.Path}");
                await _next(context);
                stopwatch.Stop();
                _logger.LogInformation($"Request process time: {stopwatch.ElapsedMilliseconds}");
            }
            catch (System.Exception ex)
            {
                context.Response.ContentType = "application/json";
                var errorObj = new object();
                switch (ex)
                {
                    case KeyNotFoundException key:
                        errorObj = new {StatusCode=404,Message=ex.Message};
                        break;
                    default:
                        errorObj = new {StatusCode=500,Message="Some internal error occured." };
                        break;

                }
                _logger.LogError($"Exception occured : {ex.StackTrace}");
                await context.Response.WriteAsJsonAsync(JsonSerializer.Serialize(errorObj));
            }

        }
    }
}

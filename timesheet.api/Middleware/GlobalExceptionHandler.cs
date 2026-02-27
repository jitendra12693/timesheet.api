using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using timesheet.business.Dtos;

namespace timesheet.api.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next,
            ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Correlation / Trace ID
            var traceId = context.TraceIdentifier;

            try
            {
                _logger.LogInformation(
                    "Incoming Request {Method} {Path} TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    traceId);

                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, traceId);
            }
            finally
            {
                stopwatch.Stop();

                _logger.LogInformation(
                    "Request Completed in {ElapsedMilliseconds} ms TraceId: {TraceId}",
                    stopwatch.ElapsedMilliseconds,
                    traceId);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception ex,
            string traceId)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            object? errors = null;
            string message;

            switch (ex)
            {
                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = ex.Message;
                    break;

                case ArgumentException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = ex.Message;
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            var response = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = message,
                TraceId = traceId,
                Errors = errors
            };

            context.Response.StatusCode = statusCode;

            _logger.LogError(ex,
                "Exception Occurred TraceId: {TraceId} StatusCode: {StatusCode}",
                traceId,
                statusCode);

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

using FluentValidation;
using Softqu.Domain.Shared.Exceptions;
using System.Net;

namespace Softqu.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            object response = new { error = "An unexpected error occurred." };

            if (exception is ValidationException validationException)
            {
                code = HttpStatusCode.BadRequest;
                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                response = new
                {
                    title = "Validation Failed",
                    status = (int)code,
                    errors = errors
                };
            }
            else if (exception is DomainException domainException)
            {
                code = HttpStatusCode.BadRequest;
                response = new
                {
                    title = "Domain Logic Violation",
                    status = (int)code,
                    detail = domainException.Message
                };
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Softqu.Application.Shared.Results;

namespace Softqu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")] // Enforces JSON for all endpoints
    // --- GLOBAL RESPONSE TYPES ---
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public abstract class ApiClientController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

        /// <summary>
        /// Centralized Result Handler.
        /// Maps Application Results (OneOf) to HTTP Status Codes.
        /// </summary>
        protected IActionResult HandleResult(AppResult result)
        {
            return result.Match(
                // 200 OK
                success => Ok(new { message = "Operation successful." }),

                // 400 Bad Request (Validation)
                validationFailed =>
                {
                    var problemDetails = new ValidationProblemDetails(
                        validationFailed.Errors.ToDictionary(e => e.PropertyName, e => new[] { e.ErrorMessage })
                    )
                    {
                        Type = "ValidationFailure",
                        Title = "Validation Error",
                        Status = StatusCodes.Status400BadRequest
                    };
                    return BadRequest(problemDetails);
                },

                // 404 Not Found
                NotFound => StatusCode(StatusCodes.Status404NotFound, new
                {
                    type = "NotFound",
                    title = "Resource Not Found",
                    detail = "Requested resource was not found."
                }),

                // 403 Forbidden
                forbidden => StatusCode(StatusCodes.Status403Forbidden, new
                {
                    type = "Forbidden",
                    title = "Access Denied",
                    detail = "You do not have permission to access or modify this resource."
                }),

                // 400 Bad Request (Domain Logic)
                domainError => BadRequest(new
                {
                    type = "DomainError",
                    title = "Business Rule Violation",
                    detail = domainError.Message
                })
            );
        }

        protected IActionResult HandleResult<T>(AppResult<T> result)
        {
            return result.Match(
                // 200 OK
                data => Ok(data),

                // 400 Bad Request (Validation)
                validationFailed =>
                {
                    var problemDetails = new ValidationProblemDetails(
                        validationFailed.Errors.ToDictionary(e => e.PropertyName, e => new[] { e.ErrorMessage })
                    )
                    {
                        Type = "ValidationFailure",
                        Title = "Validation Error",
                        Status = StatusCodes.Status400BadRequest
                    };
                    return BadRequest(problemDetails);
                },

                // 404 Not Found
                NotFound => StatusCode(StatusCodes.Status404NotFound, new
                {
                    type = "NotFound",
                    title = "Resource Not Found",
                    detail = "Requested resource was not found."
                }),

                // 403 Forbidden
                forbidden => StatusCode(StatusCodes.Status403Forbidden, new
                {
                    type = "Forbidden",
                    title = "Access Denied",
                    detail = "You do not have permission to access or modify this resource."
                }),

                // 400 Bad Request (Domain Logic)
                domainError => BadRequest(new
                {
                    type = "DomainError",
                    title = "Business Rule Violation",
                    detail = domainError.Message
                })
            );
        }
    }
}

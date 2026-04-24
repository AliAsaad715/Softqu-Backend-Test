using FluentValidation.Results;
using Softqu.Domain.Errors;

namespace Softqu.Application.Extensions
{
    public static class FluentValidationExtensions
    {
        public static ValidationError ToDomainError(this ValidationResult validationResult)
        {
            // Convert FluentValidation failures to a Dictionary<string, string[]>
            var errors = validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );

            return new ValidationError("Validation.Failed", "One or more validation errors occurred.", errors);
        }
    }
}

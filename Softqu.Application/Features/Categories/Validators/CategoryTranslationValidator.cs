using FluentValidation;
using Softqu.Application.Features.Categories.DTOs;

namespace Softqu.Application.Features.Categories.Validators
{
    public class CategoryTranslationValidator : AbstractValidator<CategoryTranslationDto>
    {
        public CategoryTranslationValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty()
                .WithMessage("Title is required.");
            RuleFor(t => t.LanguageCode)
                .NotEmpty()
                .WithMessage("Language code is required.")
                .Length(2)
                .WithMessage("Language code must be 2 characters long.");
        }
    }
}

using FluentValidation;
using Softqu.Application.Features.Categories.Commands;

namespace Softqu.Application.Features.Categories.Validators
{
    public class AddOrUpdateCategoryTranslationValidator : AbstractValidator<AddOrUpdateCategoryTranslationCommand>
    {
        public AddOrUpdateCategoryTranslationValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category ID is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");
        }
    }
}

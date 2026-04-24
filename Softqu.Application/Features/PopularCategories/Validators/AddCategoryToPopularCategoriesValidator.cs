using FluentValidation;
using Softqu.Application.Features.PopularCategories.Commands;

namespace Softqu.Application.Features.PopularCategories.Validators
{
    public class AddCategoryToPopularCategoriesValidator : AbstractValidator<AddCategoryToPopularCategoriesCommand>
    {
        public AddCategoryToPopularCategoriesValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.");

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("SortOrder cannot be negative.");
        }
    }
}

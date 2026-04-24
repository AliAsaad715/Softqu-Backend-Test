using FluentValidation;
using Softqu.Application.Features.PopularCategories.Commands;
using Softqu.Application.Features.PopularCategories.DTOs;

namespace Softqu.Application.Features.PopularCategories.Validators
{
    public class ChangePopularCategoryOrderValidator : AbstractValidator<ChangePopularCategoryOrderCommand>
    {
        public ChangePopularCategoryOrderValidator()
        {
            RuleForEach(x => x.Items)
                .SetValidator(new NewItemOrderDtoValidator());

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Items list cannot be empty.");

            RuleFor(x => x.Items)
            .Must(HaveUniqueSortOrders)
            .WithMessage("Duplicate SortOrder values are not allowed. Each item must have a unique order.");
        }

        private bool HaveUniqueSortOrders(IEnumerable<ChangePopularCategoryOrderDto> items)
        {
            if (items == null) return true;

            var orderCount = items.Select(x => x.SortOrder).Count();
            var distinctOrderCount = items.Select(x => x.SortOrder).Distinct().Count();

            return orderCount == distinctOrderCount;
        }
    } 

    public class NewItemOrderDtoValidator : AbstractValidator<ChangePopularCategoryOrderDto>
    {
        public NewItemOrderDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("SortOrder cannot be negative.");
        }
    }
}

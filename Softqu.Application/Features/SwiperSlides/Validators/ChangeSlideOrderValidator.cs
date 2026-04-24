using FluentValidation;
using Softqu.Application.Features.SwiperSlides.Commands;
using Softqu.Application.Features.SwiperSlides.DTOs;

namespace Softqu.Application.Features.SwiperSlides.Validators
{
    public class ChangeSlideOrderValidator :AbstractValidator<ChangeSlideOrderCommand>
    {
        public ChangeSlideOrderValidator()
        {
            RuleForEach(x => x.Items)
                .SetValidator(new NewItemOrderDtoValidator());

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Items list cannot be empty.");

            RuleFor(x => x.Items)
            .Must(HaveUniqueSortOrders)
            .WithMessage("Duplicate SortOrder values are not allowed. Each item must have a unique order.");
        }

        private bool HaveUniqueSortOrders(IEnumerable<ChangeSlideOrderDto> items)
        {
            if (items == null) return true;

            var orderCount = items.Select(x => x.SortOrder).Count();
            var distinctOrderCount = items.Select(x => x.SortOrder).Distinct().Count();

            return orderCount == distinctOrderCount;
        }
    }

    public class NewItemOrderDtoValidator : AbstractValidator<ChangeSlideOrderDto>
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